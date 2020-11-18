using System;
using System.Collections;
using System.Linq;

namespace Gremlin.Net.CosmosDb
{
    internal static class TypeHelper
    {
        internal static bool IsScalar(Type t) => t == typeof(string) || !(typeof(IEnumerable).IsAssignableFrom(t) || t.IsArray);

        internal static Type UnderlyingType(Type t) => IsScalar(t) ? t : (t.IsArray ? t.GetElementType() : t.GenericTypeArguments.FirstOrDefault());
    }
}