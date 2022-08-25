using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Core.Abstractions
{
    public interface IStats
    {
        int TotalWeigthLifted();
        int TotalRepsDone();
        int TotalSetsDone();
        int RepsDoneByMuscleGroup(string muscleGroup);
        int SetsDoneByMuscleGroup(string muscleGroup);
        int WeigthLiftedByMuscleGroup(string muscleGroup);
        void GetStats();
        void GetStatsByMuscleGroup(string muscleGroup);

    }
}
