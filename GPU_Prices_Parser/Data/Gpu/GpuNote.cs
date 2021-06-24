using System;

namespace GPU_Prices_Parser.Data.Gpu
{
    internal class GpuNote
    {
        public Gpu Gpu { get; }
        public DateTime DateStamp { get; }

        private readonly string _classStringRepresentation;

        public GpuNote(Gpu gpu, DateTime dateStamp)
        {
            Gpu = gpu;
            DateStamp = dateStamp;

            _classStringRepresentation = GpuModelHelper.GetRepresentation(Gpu.Model) + " " + "!" + Gpu.FullName;
        }

        public override string ToString() => _classStringRepresentation;
    }
}