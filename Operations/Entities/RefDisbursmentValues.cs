using System;
using Incoding.Data;

namespace Operations.Entities
{
    public class RefDisbursmentValues : EntityBase
    {
        public int LoanOrder { get; set; }
        public decimal Principal { get; set; }
        public decimal Comission { get; set; }
        public int Term { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
