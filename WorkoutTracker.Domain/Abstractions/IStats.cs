using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IStats
    {
        int TotalWeigthLifted();
        int TotalRepsDone();
        int TotalSetsDone();
        int RepsDoneByMuscleGroup(string muscleGroup);
        int SetsDoneByMuscleGroup(string muscleGroup);
        int WeigthLiftedByMuscleGroup(string muscleGroup);
        string[] GetStats();
        void GetStatsByMuscleGroup(string muscleGroup);

    }
}
