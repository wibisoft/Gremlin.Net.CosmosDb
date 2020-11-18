using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.CosmosDb.Serialization
{
    public class QuerySerializerTests
    {
        private IGraphTraversalSource g = new GraphTraversalSource();

        [Fact]
        public void GetSingleVertex()
        {
            Guid id = Guid.NewGuid();
            GraphTraversal<Vertex, Vertex> traversal = g.V(id);
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V(""{id}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #region HasId
        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithIEnumerableOfObjectsWithOneItem()
        {
            List<object> list = new List<object> { Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(list.First(), list.Skip(1));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithArrayOfGuidsWithOneItem()
        {
            List<Guid> list = new List<Guid> { Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeHasIdWithArrayOfObjectsWithOneItem()
        {
            List<object> list = new List<object> { Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithIEnumerableOfObjectsWithMultipleItems()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(list.First(), list.Skip(1));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"",""{list[1]}"",""{list[2]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithArrayOfGuidsWithMultipleItems()
        {
            List<Guid> list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"",""{list[1]}"",""{list[2]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeHasIdWithArrayOfObjectsWithMultipleItems()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"",""{list[1]}"",""{list[2]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion HasId

        #region predicates accepting a single object
        [Fact]
        public void SerializePredicateEqualToNumber()
        {
            int value = 5;
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("count", P.Eq(value));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""count"",eq({value}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateEqualToNumberAsObject()
        {
            object value = 5;
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("count", P.Eq(value));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""count"",eq({value}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateEqualToString()
        {
            string value = "John";
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("name", P.Eq(value));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""name"",eq(""{value}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateEqualToStringAsObject()
        {
            object value = "John";
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("name", P.Eq(value));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""name"",eq(""{value}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion predicates accepting a single object

        #region predicates accepting exactly two numbers
        [Fact]
        public void SerializePredicateBetweenWithListOfObjects()
        {
            List<object> list = new List<object> { 5, 10 };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("count", P.Between(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""count"",between({list[0]},{list[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateBetweenWithListOfInts()
        {
            List<int> list = new List<int> { 5, 10 };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("count", P.Between(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""count"",between({list[0]},{list[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateBetweenWithArrayOfInts()
        {
            int[] array = new[] { 5, 10 };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("count", P.Between(array));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""count"",between({array[0]},{array[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateBetweenWithTwoDistinctInts()
        {
            int[] array = new[] { 5, 10 };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("count", P.Between(array[0], array[1]));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""count"",between({array[0]},{array[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion predicates accepting exactly two numbers

        #region predicates acceptiong a list of objects

        [Fact]
        public void SerializePredicateWithinWithListOfSingleObject()
        {
            List<object> list = new List<object> { Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfObjects()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}"",""{list[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfSingleGuid()
        {
            List<Guid> list = new List<Guid> { Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfGuids()
        {
            List<Guid> list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}"",""{list[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfSingleGuid()
        {
            Guid[] array = new[] { Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfGuids()
        {
            Guid[] array = new[] { Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfSingleObject()
        {
            IEnumerable<object> list = new List<object> { Guid.NewGuid() }.Select(item => item);
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfObjects()
        {
            IEnumerable<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid() }.Select(item => item);
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}"",""{list.Skip(1).First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfSingleGuid()
        {
            IEnumerable<Guid> list = new List<Guid> { Guid.NewGuid() }.Select(item => item);
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfGuids()
        {
            IEnumerable<Guid> list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }.Select(item => item);
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}"",""{list.Skip(1).First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithTwoDistinctGuids()
        {
            Guid[] array = new[] { Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array[0], array[1]));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfSingleString()
        {
            List<string> array = new List<string> { "abc" };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfStrings()
        {
            List<string> list = new List<string> { "abc", "123" };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(list));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}"",""{list[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfSingleString()
        {
            string[] array = new[] { "abc" };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfStrings()
        {
            string[] array = new[] { "abc", "123" };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithTwoDistinctStrings()
        {
            string[] array = new[] { "abc", "123" };
            GraphTraversal<Vertex, Vertex> traversal = g.V().HasId(P.Within(array[0], array[1]));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion predicates acceptiong a list of objects

        #region anonymous traversals

        [Fact]
        public void SerializeAnonymousVeeAsStepArgument()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("p1", __.V("someId").Values<string>("p1"));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""p1"",__.V(""someId"").values(""p1""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousVeeAsPredicateArgument()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Has("p1", P.Neq(__.V("someId").Values<string>("p1")));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().has(""p1"",neq(__.V(""someId"").values(""p1"")))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousNotAsStepArgument()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().And(__.Has("prop1"), __.Not(__.Has("prop2")));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().and(has(""prop1""),__.not(has(""prop2"")))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousNotAsPredicateArgument()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V().Where(P.Neq(__.Not(__.Has("prop2")))); // contrived example
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V().where(neq(__.not(has(""prop2""))))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousInAsStepArgument()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, object> traversal = g.V("someId").Optional<object>(__.In("edgeLabel").In("edgeLabel2"));
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V(""someId"").optional(__.in(""edgeLabel"").in(""edgeLabel2""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousInAsPredicateArgument()
        {
            List<object> list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            GraphTraversal<Vertex, Vertex> traversal = g.V("someId").Where(P.Eq(__.In("edgeLabel").In("edgeLabel2"))); // contrived example
            string actualGremlinQuery = traversal.ToGremlinQuery();
            string expectedGremlinQuery = $@"g.V(""someId"").where(eq(__.in(""edgeLabel"").in(""edgeLabel2"")))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }
        #endregion anonymous traversals
    }
}