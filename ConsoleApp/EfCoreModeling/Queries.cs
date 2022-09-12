using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkoutTracker.Infrastructure;

namespace ConsoleApp.EfCoreModeling
{
    public class Queries
    {
        public static async Task RunQueries()
        {
            await using var context = new WorkoutContext();
            //Get the total volume lifted by a user
            var query1 = context.CompletedRoutines
                .Where(cr => cr.UserId == 2)
                .Select(cr => cr.Routine)
                .SelectMany(r => r.WorkoutSets)
                .SelectMany(ws => ws.Sets)
                .Sum(w => w.Weight * w.NumberOfReps);
            Console.WriteLine(query1);

            //Get the total of reps done by a user
            var query2 = context.CompletedRoutines
                .Where(cr => cr.UserId == 2)
                .Select(cr => cr.Routine)
                .SelectMany(r => r.WorkoutSets)
                .SelectMany(ws => ws.Sets)
                .Sum(w => w.NumberOfReps);
            Console.WriteLine(query2);

            //Get the total of sets done by a user
            var query3 = context.CompletedRoutines
                .Where(cr => cr.UserId == 2)
                .Select(r => r.Routine)
                .SelectMany(cr => cr.WorkoutSets)
                .SelectMany(ws => ws.Sets)
                .Count();
            Console.WriteLine(query3);

            //Get the muscle split percentages for all completed routines
            var query4 = context.CompletedRoutines
                .Where(cr => cr.UserId == 2)
                .Select(r => r.Routine)
                .SelectMany(cr => cr.WorkoutSets)
                .SelectMany(ws => ws.Sets).Include(s => s.Exercise)
                .AsEnumerable()
                .GroupBy(cr => cr.Exercise.Category);

            foreach(var item in query4)
            {
                Console.WriteLine(Math.Round(((double)item.Count()/query3) * 100) + "% " + $"{item.Key}");
            }

            //Get the completed routines in the past month
            var query5 = context.CompletedRoutines
                .Where(cr => cr.UserId == 2)
                .Where(cr => cr.CreatedAt > DateTime.Now.AddMonths(-1));

            foreach(var item in query5)
            {
                Console.WriteLine(JsonSerializer.Serialize(item));
            }

            //Get the exercises grouped by the muscle category
            var query6 = context.Exercises
                .AsEnumerable()
                .GroupBy(e => e.Category);
                
            foreach(var ex in query6)
            {
                Console.WriteLine("{0} {1}", ex.Key,ex.Count() );
            }

            //Get the average weigth lifted by routine
            var query7 = context.CompletedRoutines
                .Where(cr => cr.UserId == 1)
                .Select(r => r.Routine)
                .SelectMany(cr => cr.WorkoutSets)
                .SelectMany(ws => ws.Sets)
                .Average(s => s.Weight * s.NumberOfReps);
            Console.WriteLine(query7);

            //Get the max weigth lifted by exercise
            var query8 = context.CompletedRoutines
                .Where(cr => cr.UserId == 1)
                .Select(r => r.Routine)
                .SelectMany(cr => cr.WorkoutSets)
                .SelectMany(ws => ws.Sets)
                .AsEnumerable()
                .GroupBy(s => s.Exercise.Name)
                .Select(x => new
                {
                    Name = x.Key.ToString(),
                    MaxWeigth = x.Max(x => x.Weight),
                });
                
            foreach( var ex in query8)
            {
                Console.WriteLine("{0} {1}", ex.Name, ex.MaxWeigth);
            }































        }






    }
}
