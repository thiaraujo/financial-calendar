using FluentValidation;
using FluentValidation.Results;
using Middleware.Notifications;

namespace Domain.Entities.Shared;

public abstract class BaseEntity : AbstractNotificationContext
{
    public string Id { get; private set; }
    public bool Valid { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
        Valid = true;
    }

    public ValidationResult ValidationResult { get; private set; }

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return Valid = ValidationResult.IsValid;
    }
}