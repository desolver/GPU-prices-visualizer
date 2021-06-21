using System;
using GPU_Prices_Parser.Data;

namespace GPU_Prices_Parser.Graph
{
    internal class GpuNote
    {
        public Gpu Gpu { get; }
        public DateTime DateStamp { get; }
        
        public GpuNote(Gpu gpu, DateTime dateStamp)
        {
            Gpu = gpu;
            DateStamp = dateStamp;
        }
    }
}