using System.Collections.Generic;
using System.Linq;

namespace GPU_Prices_Parser.Data.Gpu
{
    internal enum GpuModel
    {
        Rtx_2060,
        Rtx_3060,
        //Rtx_3060_ti,
        Rtx_3070,
        //Rtx_3070_ti,
        Rtx_3080,
        Rtx_3090
    }

    internal static class GpuModelHelper
    {
        private static readonly Dictionary<GpuModel, string> Representations;
        private static readonly Dictionary<string, GpuModel> Models;

        static GpuModelHelper()
        {
            Representations = new Dictionary<GpuModel, string>
            {
                {GpuModel.Rtx_2060, "RTX 2060"},
                {GpuModel.Rtx_3060, "RTX 3060"},
                //{GpuModel.Rtx_3060_ti, "RTX 3060 ti"},
                {GpuModel.Rtx_3070, "RTX 3070"},
                //{GpuModel.Rtx_3070_ti, "RTX 3070 ti"},
                {GpuModel.Rtx_3080, "RTX 3080"},
                {GpuModel.Rtx_3090, "RTX 3090"}
            };
            
            Models = Representations
                .Select(pair => new KeyValuePair<string, GpuModel>(pair.Value, pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        
        public static string GetRepresentation(GpuModel model) => Representations[model];
        
        public static GpuModel GetModel(string representation) => Models[representation];
        
        public static GpuModel[] GetModelList() => Representations.Select(pair => pair.Key).ToArray();
    }
}