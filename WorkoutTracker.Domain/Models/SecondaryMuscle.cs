using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class SecondaryMuscle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ExerciseId { get; set; }
        public Exercise Exercise { get; set; }


    }
}
