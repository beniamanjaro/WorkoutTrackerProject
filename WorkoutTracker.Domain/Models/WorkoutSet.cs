using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class WorkoutSet
    {
        public int Id { get; set; }
        public int? RoutineId { get; set; }
        public ICollection<Set> Sets { get; set; }
        public Routine Routine { get; set; }

    }
}
