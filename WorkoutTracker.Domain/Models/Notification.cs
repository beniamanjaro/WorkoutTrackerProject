using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public int WorkoutPlanId { get; set; }
        public bool Seen { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
