using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhino.Hub
{
    public static class PluginUtilities
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        internal static readonly string TabName = "Hub";

        internal static readonly string CategoryAAInputs = "1. Lights";
    }
}