using WorkoutTracker.Domain.Abstractions;
using System.Linq;
using WorkoutTracker.Domain.Models;
using ConsoleApp;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class StatsHelper : IStats
    {
        private List<CompletedRoutine> _completedRoutines;
        private int _totalReps;
        private int _totalSets;
        private int _totalWeight;
        public StatsHelper(List<CompletedRoutine> completedRoutines)
        {
            _completedRoutines = completedRoutines;
        }

        public int GetMaxWeightByExercise( string exercise)
        {
            var maxWeight = _completedRoutines
                  .Select(r => r.Routine)
                  .SelectMany(cr => cr.WorkoutSets)
                  .SelectMany(ws => ws.Sets)
                  .Where(s => s.Exercise.Name.ToLower() == exercise.ToLower())
                  .Max(s => s.Weight);
            if (maxWeight == null) return 0;

            return maxWeight;

        }



        public int TotalWeigthLifted()
        {
            foreach (var completedRoutine in _completedRoutines)
            {
                _totalWeight += completedRoutine.Routine.WorkoutSets
                    .SelectMany(w => w.Sets)
                    .ToList()
                    .Sum(w => w.Weight * w.NumberOfReps);
            }

            return _totalWeight;
        }

        public int TotalRepsDone()
        {
            foreach (var completedRoutine in _completedRoutines)
            {
                _totalReps += completedRoutine.Routine.WorkoutSets
                    .SelectMany(w => w.Sets)
                    .ToList()
                    .Sum(w => w.NumberOfReps);
            }

            return _totalReps;
        }

        public int TotalSetsDone()
        {

            foreach (var completedRoutine in _completedRoutines)
            {
                _totalSets += completedRoutine.Routine.WorkoutSets
                    .SelectMany(w => w.Sets)
                    .ToList()
                    .Count;
            }
            return _totalSets;
        }

        public int AverageRepsPerSet()
        {
            return _totalReps / _totalSets;
        }


        public int RepsDoneByMuscleGroup(string muscleGroup)
        {
            int totalReps = 0;

            foreach (var completedRoutine in _completedRoutines)
            {
                _totalReps += completedRoutine.Routine.WorkoutSets
                    .SelectMany(w => w.Sets)
                    .Where(s => s.Exercise.Category == muscleGroup)
                    .ToList()
                    .Sum(w => w.NumberOfReps);
            }

            return totalReps;
        }

        public int SetsDoneByMuscleGroup(string muscleGroup)
        {
            int totalSets = 0;

            foreach (var completedRoutine in _completedRoutines)
            {
                totalSets += completedRoutine.Routine.WorkoutSets
                    .SelectMany(w => w.Sets)
                    .Where(w => w.Exercise.Category == muscleGroup)
                    .ToList()
                    .Count;

            }
            return totalSets;
        }

        public int WeigthLiftedByMuscleGroup(string muscleGroup)
        {
            int totalWeight = 0;

            foreach (var completedRoutine in _completedRoutines)
            {
                totalWeight += completedRoutine.Routine.WorkoutSets
                    .SelectMany(w => w.Sets)
                    .Where(s => s.Exercise.Category == muscleGroup)
                    .ToList()
                    .Sum(w => w.Weight * w.NumberOfReps);
            }

            return totalWeight;
        }

        public string[] GetStats()
        {
            return new string[] {$"volume {TotalWeigthLifted()}",$"reps {TotalRepsDone()}",$"sets {TotalSetsDone()}" };
        }

        public void GetStatsByMuscleGroup(string muscleGroup)
        {
            Console.WriteLine($"Your {muscleGroup} statistics are:");
            Console.WriteLine($"The total volume you lifted: {WeigthLiftedByMuscleGroup(muscleGroup)} ");
            Console.WriteLine($"Total reps you've done: {RepsDoneByMuscleGroup(muscleGroup)} ");
            Console.WriteLine($"Total sets you've done: {SetsDoneByMuscleGroup(muscleGroup)} ");
        }
    }
}