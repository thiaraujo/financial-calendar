using FluentValidation.Results;

namespace Middleware.Notifications;

public abstract class AbstractNotificationContext
{
    public void AddNotification(string key, string message) { }
    public void AddNotification(Notification notification) { }
    public void AddNotifications(IReadOnlyCollection<Notification> notifications) { }
    public void AddNotifications(IList<Notification> notifications) { }
    public void AddNotifications(ICollection<Notification> notifications) { }
    public void AddNotifications(ValidationResult validationResult) { }
}