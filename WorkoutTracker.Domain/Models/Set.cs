using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int NumberOfReps { get; set; }
        public int Weight { get; set; }
        public int WorkoutSetId { get; set; }
        public WorkoutSet WorkoutSet { get; set; }


    }
}
