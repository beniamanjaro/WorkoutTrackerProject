using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? SelectedWorkoutPlanId { get; set; }
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }

        public void ExportWorkout(int workoutId, string filePath)
        {
            try
            {
                var workoutPlan = WorkoutPlans.First(w => w.Id == workoutId);
                var routines = workoutPlan.Routines;
                using (StreamWriter file = new(@filePath, false))
                {
                    file.WriteLine(workoutPlan.Name);
                    file.WriteLine();
                    foreach (Routine routine in routines)
                    {
                        file.WriteLine(routine.Name);
                        foreach (WorkoutSet workoutSet in routine.WorkoutSets)
                        {
                            foreach (Set set in workoutSet.Sets)
                            {
                                file.WriteLine($"{set.Exercise.Name}, {set.NumberOfReps}, {set.Weight}");
                            }
                            file.WriteLine();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw new ApplicationException("exception", ex);

            }
        }

        public void AddWorkoutPlan(WorkoutPlan workoutPlan)
        {
            WorkoutPlans.Add(workoutPlan);
        }


        //public void CompleteRoutine()
        //{
        //    CompletedRoutines.Add(SelectedWorkoutPlan.Routines[0]);
        //    var temp = SelectedWorkoutPlan.Routines[0];
        //    SelectedWorkoutPlan.Routines.RemoveAt(0);

        //}

    }
}
