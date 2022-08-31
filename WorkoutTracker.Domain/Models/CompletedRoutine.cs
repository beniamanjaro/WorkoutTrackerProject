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
        public string Name { get; set; }
        public ICollection<WorkoutSet> WorkoutSets { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
