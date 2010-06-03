using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinook.Domain
{
    public class Invoice
    {
        public virtual int InvoiceId { get; private set; }
        public virtual Customer Customer { get; set; }
        public virtual DateTime InvoiceDate { get; set; }

        public virtual string BillingAddress { get; set; }
        public virtual string BillingCity { get; set; }
        public virtual string BillingState { get; set; }
        public virtual string BillingCountry { get; set; }
        public virtual string BillingPostalCode { get; set; }
        
        public virtual decimal Total { 
            get {
                return Lines.Sum(l => l.Quantity * l.UnitPrice);
            } 
        }

        public virtual IList<InvoiceLine> Lines { get; private set; }

        public virtual void AddLine(InvoiceLine line)
        {
            line.Invoice = this;
            Lines.Add(line);
        }

        public Invoice()
        {
            Lines = new List<InvoiceLine>();
        }
    }
}