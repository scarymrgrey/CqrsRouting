using System;
using Incoding.Data;

namespace Operations.Entities
{
    public class RefExtentionValue : EntityBase
    {
        public int DisbursmentValueId { get; set; }
        public int ExtentionTerm { get; set; }
        public decimal ExtentionValue { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
