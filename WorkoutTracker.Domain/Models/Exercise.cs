using System;
using WorkoutTracker.Domain.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public ICollection<PrimaryMuscle> PrimaryMuscles { get; set; } 
    public ICollection<SecondaryMuscle> SecondaryMuscles { get; set; }
    public string Equipment { get; set; }
    public override string ToString()
    {
        return $"Name: {Name}, Primary Muscles:, Secondary Muscles: ";
    }

}
