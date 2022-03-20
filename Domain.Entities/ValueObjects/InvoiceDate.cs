using Domain.Entities.Shared;

namespace Domain.Entities.ValueObjects;

public class InvoiceDate : BaseValueObject
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }

    public InvoiceDate(DateTime date)
    {
        Year = date.Year;
        Month = date.Month;
        Day = date.Day;
    }
}