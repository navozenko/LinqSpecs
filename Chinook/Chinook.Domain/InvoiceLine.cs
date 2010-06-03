using System;

namespace Chinook.Domain
{
    public class InvoiceLine
    {
        public virtual int InvoiceLineId { get; private set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Track Track { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual int Quantity { get; set; }

    }
}