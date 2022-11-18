using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class WorkoutPlan 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TimesPerWeek { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Routine> Routines { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
