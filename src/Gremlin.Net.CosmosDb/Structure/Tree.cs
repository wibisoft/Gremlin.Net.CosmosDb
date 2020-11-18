using Gremlin.Net.CosmosDb.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A parsed traversal result in tree representation. E.g. "g.V().hasLabel('person').outE('purchased').inV().tree()"
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// The root vertices of the tree traversal.
        /// </summary>
        public TreeVertexNode[] RootVertexNodes { get; set; }

        /// <summary>
        /// Convert the parsed tree into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="treeConnectors">Custom connectors to use for attaching edges and vertices.</param>
        /// <returns></returns>
        public T[] ToObject<T>(IEnumerable<ITreeConnector> treeConnectors = null)
            where T : IVertex
        {
            TreeParser parser = new TreeParser(treeConnectors ?? Enumerable.Empty<ITreeConnector>());

            IEnumerable<T> vertices = RootVertexNodes.Select(n => parser.GetVertex(n, typeof(T))).Cast<T>();

            return vertices.ToArray();
        }

        private class TreeParser
        {
            private readonly List<IEdgeConnector> _edgeConnectors;
            private readonly List<IVertexConnector> _vertexConnectors;

            public TreeParser(IEnumerable<ITreeConnector> connectors)
            {
                if (!connectors.Any(c => c is AutoConnector))
                {
                    connectors = connectors.Append(new AutoConnector());
                }

                _vertexConnectors = connectors.OfType<IVertexConnector>().ToList();
                _edgeConnectors = connectors.OfType<IEdgeConnector>().ToList();
            }

            public IEdge GetEdge(TreeEdgeNode edgeNode, Type edgeType, bool directionIsOut)
            {
                IEdge edge = (IEdge)edgeNode.Edge.ToObject(edgeType);

                Type nextVertexType = directionIsOut ? GetEdgeInType(edgeType) : GetEdgeOutType(edgeType);

                IVertex vertex = GetVertex(edgeNode.VertexNode, nextVertexType);

                foreach (IVertexConnector connector in _vertexConnectors)
                {
                    if (connector.ConnectVertex(edge, vertex)) continue;
                }

                return edge;
            }

            public IVertex GetVertex(TreeVertexNode vertexNode, Type vertexType)
            {
                IVertex vertexObject = (IVertex)vertexNode.Vertex.ToObject(vertexType);
                string vertexLabel = vertexType.GetLabelName();

                PropertyInfo[] edgeProps = GetEdgeProps(vertexType);

                foreach (PropertyInfo edgeProp in edgeProps)
                {
                    string label = edgeProp.GetLabelName();

                    List<TreeEdgeNode> edgeNodes = vertexNode.EdgeNodes.Where(e => e.Edge.Label == label).ToList();
                    Type inV = GetEdgeInType(edgeProp.PropertyType);
                    string inVLabel = inV.GetLabelName();
                    Type outV = GetEdgeOutType(edgeProp.PropertyType);
                    string outVLabel = outV.GetLabelName();

                    foreach (TreeEdgeNode node in edgeNodes)
                    {
                        bool isOut = node.Edge.OutVLabel == outVLabel;

                        IEdge edge = GetEdge(node, edgeProp.PropertyType, isOut);

                        foreach (IEdgeConnector connector in _edgeConnectors)
                        {
                            if (connector.ConnectEdge(vertexObject, edge, edgeProp)) continue;
                        }
                    }
                }

                return vertexObject;
            }

            private static Type GetEdgeInType(Type e) => e.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IHasInV<>)).GetGenericArguments()[0];

            private static Type GetEdgeOutType(Type e) => e.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IHasOutV<>)).GetGenericArguments()[0];

            private static PropertyInfo[] GetEdgeProps(Type v) => v.GetProperties().Where(p => typeof(IEdge).IsAssignableFrom(p.PropertyType)).ToArray();
        }
    }
}