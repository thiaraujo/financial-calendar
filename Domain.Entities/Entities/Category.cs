using Domain.Entities.Shared;

namespace Domain.Entities.Entities;

public class Category : BaseEntity
{
    public string Description { get; set; }
    public string Color { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }

    public Category(string description, string color, DateTime registrationDate, bool isActive)
    {
        Description = description;
        Color = color;
        RegistrationDate = registrationDate;
        IsActive = isActive;
    }
}