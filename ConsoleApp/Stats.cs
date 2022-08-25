//using WorkoutTracker.Core.Abstractions;
//using System.Linq;

//namespace WorkoutTracker.Domain.Models
//{
//    public class Stats : IStats
//    {
//        private List<Routine> _routines;
//        private int _totalReps;
//        private int _totalSets;
//        private int _totalWeight;
//        public Stats(List<Routine> routines)
//        {
//            _routines = routines;
//        }

//        public int TotalWeigthLifted()
//        {
//            foreach (Routine routine in _routines)
//            {
//                _totalWeight += routine.WorkoutSets
//                    .SelectMany(w => w.sets)
//                    .ToList()
//                    .Sum(w => w.Weight * w.NumberOfReps);
//            }

//            return _totalWeight;
//        }

//        public int TotalRepsDone()
//        {
//            foreach (Routine routine in _routines)
//            {
//                _totalReps += routine.WorkoutSets
//                    .SelectMany(w => w.sets)
//                    .ToList()
//                    .Sum(w => w.NumberOfReps);
//            }

//            return _totalReps;
//        }

//        public int TotalSetsDone()
//        {

//            foreach (Routine routine in _routines)
//            {
//                _totalSets += routine.WorkoutSets
//                    .SelectMany(w => w.sets)
//                    .ToList()
//                    .Count;

//            }
//            return _totalSets;
//        }

//        public int AverageRepsPerSet()
//        {
//            return _totalReps / _totalSets;
//        }


//        public int RepsDoneByMuscleGroup(string muscleGroup)
//        {
//            int totalReps = 0;

//            foreach (Routine routine in _routines)
//            {
//                totalReps += routine.WorkoutSets
//                    .SelectMany(w => w.sets)
//                    .Where(s => s.Exercise.Category == muscleGroup)
//                    .ToList()
//                    .Sum(w => w.NumberOfReps);
//            }

//            return totalReps;
//        }

//        public int SetsDoneByMuscleGroup(string muscleGroup)
//        {
//            int totalSets = 0;

//            foreach (Routine routine in _routines)
//            {
//                totalSets += routine.WorkoutSets
//                    .SelectMany(w => w.sets)
//                    .Where(w => w.Exercise.Category == muscleGroup)
//                    .ToList()
//                    .Count;

//            }
//            return totalSets;
//        }

//        public int WeigthLiftedByMuscleGroup(string muscleGroup)
//        {
//            int totalWeight = 0;

//            foreach (Routine routine in _routines)
//            {
//                totalWeight += routine.WorkoutSets
//                    .SelectMany(w => w.sets)
//                    .Where(s => s.Exercise.Category == muscleGroup)
//                    .ToList()
//                    .Sum(w => w.Weight * w.NumberOfReps);
//            }

//            return totalWeight;
//        }

//        public void GetStats()
//        {
//            Console.WriteLine("Your statistics are:");
//            Console.WriteLine($"The total volume you lifted: {TotalWeigthLifted()} ");
//            Console.WriteLine($"Total reps {TotalRepsDone()}");
//            Console.WriteLine($"Total sets {TotalSetsDone()}");
//            Console.WriteLine($"Average reps per set {AverageRepsPerSet()}");
//        }

//        public void GetStatsByMuscleGroup(string muscleGroup)
//        {
//            Console.WriteLine($"Your {muscleGroup} statistics are:");
//            Console.WriteLine($"The total volume you lifted: {WeigthLiftedByMuscleGroup(muscleGroup)} ");
//            Console.WriteLine($"Total reps you've done: {RepsDoneByMuscleGroup(muscleGroup)} ");
//            Console.WriteLine($"Total sets you've done: {SetsDoneByMuscleGroup(muscleGroup)} ");
//        }
//    }
//}