using Domain.Entities.ValueObjects;

namespace Domain.Entities.Entities;

public class InvoiceRevenue : Invoice
{
    public DateTime? ReceiveDate { get; private set; }
    public decimal TotalReceive { get; private set; }

    public InvoiceRevenue(OrganizationAccount account, string description, string categoryId, InvoiceDate invoiceDate, decimal total, DateTime registrationDate, DateTime? receiveDate, decimal totalReceive)
        : base(account, description, categoryId, invoiceDate, total, registrationDate)
    {
        ReceiveDate = receiveDate;
        TotalReceive = totalReceive;
    }
}