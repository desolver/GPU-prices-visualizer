using System;

namespace GPU_Prices_Parser.Data.Gpu
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

        public override string ToString() => Gpu.FullName;
    }
}