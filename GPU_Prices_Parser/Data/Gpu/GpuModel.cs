using System;
using System.Collections.Generic;
using System.Linq;
using GPU_Prices_Parser.Extensions;

namespace GPU_Prices_Parser.Data.Gpu
{
    internal enum GpuModel
    {
        Rtx_2060,
        Rtx_3060,
        Rtx_3060_ti,
        Rtx_3070,
        Rtx_3070_ti,
        Rtx_3080,
        Rtx_3090
    }

    internal static class GpuModelHelper
    {
        private static readonly Dictionary<GpuModel, string> Representations = new Dictionary<GpuModel, string>
        {
            {GpuModel.Rtx_2060, "RTX 2060"},
            {GpuModel.Rtx_3060, "RTX 3060"},
            {GpuModel.Rtx_3060_ti, "RTX 3060 ti"},
            {GpuModel.Rtx_3070, "RTX 3070"},
            {GpuModel.Rtx_3070_ti, "RTX 3070 ti"},
            {GpuModel.Rtx_3080, "RTX 3080"},
            {GpuModel.Rtx_3090, "RTX 3090"}
        };

        public static string GetRepresentation(GpuModel model) => Representations[model];

        public static GpuModel Parse(string value)
        {
            var newValue = value.Capitalize().Replace(' ', '_');
            if (Enum.TryParse(newValue, out GpuModel result))
                return result;

            throw new ArgumentException($"String {value} can't parse to GpuModel type");
        }

        public static GpuModel[] GetModelList() => Representations.Select(pair => pair.Key).ToArray();
    }
}