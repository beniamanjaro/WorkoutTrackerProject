using Microsoft.AspNetCore.SignalR;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Presentation.Hubs
{
    public class NotificationHub : Hub<INotification>
    {
        public async Task SendMessage(Notification notification)
        {
            await Clients.All.ReceiveMessage(notification);
        }
    }
}
