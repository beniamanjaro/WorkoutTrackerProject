using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.Hubs
{
    public interface INotification
    {
        Task ReceiveMessage(Notification notification);
    }
}
