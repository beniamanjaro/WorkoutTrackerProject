using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class Routine
    {
        public int WorkoutPlanId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayOrderNumber { get; set; }
        public ICollection<WorkoutSet> WorkoutSets { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }

    }
}
