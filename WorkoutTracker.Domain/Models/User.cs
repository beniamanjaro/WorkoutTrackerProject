using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int? SelectedWorkoutPlanId { get; set; }
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }
        public ICollection<CompletedRoutine> CompletedRoutines { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public IdentityUser Identity {get; set;}
        public string IdentityId {get; set; }

    }
}

