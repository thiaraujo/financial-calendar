using Domain.Entities.ValueObjects;

namespace Domain.Entities.Entities
{
    public class InvoicePayment : Invoice
    {
        public DateTime? PaidDate { get; private set; }
        public decimal TotalPaid { get; private set; }

        public InvoicePayment(OrganizationAccount account, string description, string categoryId, InvoiceDate invoiceDate, decimal total, DateTime registrationDate, DateTime? paidDate, decimal totalPaid) 
            : base(account, description, categoryId, invoiceDate, total, registrationDate)
        {
            PaidDate = paidDate;
            TotalPaid = totalPaid;
        }
    }
}
