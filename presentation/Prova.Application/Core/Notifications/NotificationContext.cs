using FluentValidation.Results;

namespace Prova.Application.Core.Notifications;

public class Notification(string key, string messages)
{
    public string Key { get; } = key;
    public string Message { get; } = messages;
}

public sealed class NotificationContext
{
    private static NotificationContext? _instance;

    private readonly List<Notification> _notifications;
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public IReadOnlyDictionary<string, string[]> ToDictionary => 
        _notifications.GroupBy(not => not.Key.Substring(not.Key.IndexOf('.') + 1),
            not => not.Message, (propertyName, ErrorMessage) => new
            {
                Key = propertyName,
                Value = ErrorMessage.Distinct().ToArray()
            }
        )
        .ToDictionary(failure => failure.Key, failure => failure.Value);

    public bool HasNotifications => _notifications.Any();

    public static NotificationContext Instance
    {
        get
        {
            _instance ??= new NotificationContext();
            return _instance;
        }
    }


    public NotificationContext()
    {
        _notifications = [];
    }

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(IList<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ICollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddNotification(error.ErrorCode, error.ErrorMessage);
        }
    }
}
