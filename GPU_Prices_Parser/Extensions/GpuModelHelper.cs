using System;
using GPU_Prices_Parser.Data;

namespace GPU_Prices_Parser.Extensions
{
    internal static class GpuModelHelper
    {
        public static GpuModel Parse(string value)
        {
            var newValue = value.Capitalize().Replace(' ', '_');
            if (Enum.TryParse(newValue, out GpuModel result))
                return result;
            
            throw new ArgumentException($"String {value} can't parse to GpuModel type");
        }
    }
}