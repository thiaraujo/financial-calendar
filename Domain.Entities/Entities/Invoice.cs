using Domain.Entities.Shared;
using Domain.Entities.ValueObjects;

namespace Domain.Entities.Entities;

public abstract class Invoice : BaseEntity
{
    public OrganizationAccount Account { get; private set; }
    public string Description { get; private set; }
    public string CategoryId { get; private set; }
    public InvoiceDate InvoiceDate { get; private set; }
    public decimal Total { get; private set; }
    public DateTime RegistrationDate { get; private set; }

    protected Invoice(OrganizationAccount account, string description, string categoryId, InvoiceDate invoiceDate, decimal total, DateTime registrationDate)
    {
        Account = account;
        Description = description;
        CategoryId = categoryId;
        InvoiceDate = invoiceDate;
        Total = total;
        RegistrationDate = registrationDate;
    }
}