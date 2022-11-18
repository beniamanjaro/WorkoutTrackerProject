using WorkoutTracker.Domain.Abstractions;
using System.Linq;
using WorkoutTracker.Domain.Models;
using ConsoleApp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json.Serialization;

namespace WorkoutTracker.Application.Stats.Queries
{
    public class StatsHelper 
    {
        private List<CompletedRoutine>? _completedRoutines;
        private List<Routine>? _routines;
        private List<Exercise>? _exercises;
        private int _totalReps;
        private int _totalSets;
        private int _totalWeight;
        public StatsHelper(List<CompletedRoutine> completedRoutines)
        {
            _completedRoutines = completedRoutines;
        }

        public StatsHelper(List<Routine> routines)
        {
            _routines = routines;
        }
        public StatsHelper(List<Exercise> exercises)
        {
            _exercises = exercises;
        }


        public double GetMaxWeightByExercise(string exercise)
        {
            var maxWeight = _completedRoutines
                  .SelectMany(cr => cr.Exercises)
                  .Where(e => e.Exercise.Name.ToLower() == exercise.ToLower())
                  .Max(e => e.Weight);
            if (maxWeight == null) return 0;

            return maxWeight;

        }

        public List<Dictionary<string, int>> GetMuscleSplitForRoutines()
        {
            List<Dictionary<string, int>> muscleSplits = new List<Dictionary<string, int>>();
            foreach(var routine in _routines)
            {
                Dictionary<string, int> muscleSplit = new Dictionary<string, int>();

                var count = routine.WorkoutSets
                    .SelectMany(ws => ws.Sets)
                    .Count();

                var groupedExercises = routine.WorkoutSets
                    .SelectMany(ws => ws.Sets)
                    .AsEnumerable()
                    .GroupBy(s => s.Exercise.Category);

                foreach(var item in groupedExercises)
                {
                    var s = item.Count();

                    var r = Math.Round(((double)item.Count() / count) * 100);
                    muscleSplit.Add(item.Key,(int)Math.Round(((double)item.Count() / count) * 100));
                }
                muscleSplit.Add("dayOrder", routine.DayOrderNumber);
                muscleSplits.Add(muscleSplit);
            }
            return muscleSplits;
        }

        public List<KeyValuePair<string, int>> GetMuscleSplitForExercises()
        {
            Dictionary<string, int> muscleSplit = new Dictionary<string, int>();

            var groupedExercises = _exercises.GroupBy(e => e.Category);
            var count = _exercises.Count();
                foreach (var item in groupedExercises)
                {
                    var s = item.Count();

                    var r = Math.Round(((double)item.Count() / count) * 100);
                muscleSplit.Add(char.ToUpper(item.Key[0]) + item.Key.Substring(1) , (int)Math.Round(((double)item.Count() / count) * 100));
                }
            
            return muscleSplit.OrderByDescending(ms => ms.Value).ToList();
        }

        public Dictionary<string, int> GetNumberOfExercisesCompletedByMuscleGroup()
        {
            Dictionary<string, int> muscleSplits = new Dictionary<string, int>();
            foreach (var routine in _routines)
            {
                var groupedExercises = routine.WorkoutSets
                    .SelectMany(ws => ws.Sets)
                    .AsEnumerable()
                    .GroupBy(s => s.Exercise.Category);

                foreach (var item in groupedExercises)
                {
                    var s = item.Count();
                    if (muscleSplits.ContainsKey(item.Key))
                    {
                        muscleSplits[item.Key] += s;
                    }
                    else
                    {
                        muscleSplits[item.Key] = s;
                    }
                }
            }
            return muscleSplits;

        }


        public int TotalWeigthLifted()
        {
            foreach (var completedRoutine in _completedRoutines)
            {
                _totalWeight += completedRoutine.TotalVolume;
            }

            return _totalWeight;
        }

        public int TotalRepsDone()
        {
            foreach (var completedRoutine in _completedRoutines)
            {
                _totalReps += completedRoutine.TotalReps;
            }

            return _totalReps;
        }

        public int TotalSetsDone()
        {

            foreach (var completedRoutine in _completedRoutines)
            {
                _totalSets += completedRoutine.TotalSets;
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
                _totalReps += completedRoutine
                    .Exercises
                    .Where(e => e.Exercise.Category == muscleGroup)
                    .ToList()
                    .Sum(w => w.Reps);
            }

            return totalReps;
        }

        public int SetsDoneByMuscleGroup(string muscleGroup)
        {
            int totalSets = 0;

            foreach (var completedRoutine in _completedRoutines)
            {
                totalSets += completedRoutine.Exercises
                    .Where(w => w.Exercise.Category == muscleGroup)
                    .ToList()
                    .Count;

            }
            return totalSets;
        }

        public double WeigthLiftedByMuscleGroup(string muscleGroup)
        {
            double totalWeight = 0;

            foreach (var completedRoutine in _completedRoutines)
            {
                totalWeight += completedRoutine.Exercises
                    .Where(s => s.Exercise.Category == muscleGroup)
                    .ToList()
                    .Sum(w => w.Weight * w.Reps);
            }

            return totalWeight;
        }

        public Dictionary<string, int> GetStats()
        {
            var dict = new Dictionary<string, int>();
            dict["Volume"] = TotalWeigthLifted();
            dict["Reps"] = TotalRepsDone();
            dict["Sets"] = TotalSetsDone();
            dict["Average Reps per Set"] = AverageRepsPerSet();
            dict["Frequency"] = _completedRoutines.Count();

            return dict;
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
