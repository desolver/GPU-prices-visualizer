using System.Collections.Generic;
using GPU_Prices_Parser.Data.Gpu;

namespace GPU_Prices_Parser
{
    internal static class GpuInfoHolder
    {
        private static readonly Dictionary<string, List<GpuNote>> Info = new Dictionary<string, List<GpuNote>>();

        public static bool Exists(string name) => Info.ContainsKey(name);

        public static void Add(string name, GpuNote note)
        {
            if (Exists(name))
            {
                var index = Info[name].IndexOf(note);
                if (index < 0) Info[name].Add(note);
                else Info[name][index] = note;
            }
            else Info.Add(name, new List<GpuNote>{note});
        }
    }
}