using System;

namespace ParishForms.Common.Models.Exports
{
    public sealed class CompressedResult
    {
        public CompressedResult()
        {
            Data = new byte[0];
        }

        public Guid RequestId { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public byte[] Data { get; set; }
    }
}
