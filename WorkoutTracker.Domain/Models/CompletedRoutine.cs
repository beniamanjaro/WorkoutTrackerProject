using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class CompletedRoutine
    {
        public int CompletedRoutineId { get; set; }
        public int? RoutineId { get; set; }
        public Routine Routine { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

    }
}
