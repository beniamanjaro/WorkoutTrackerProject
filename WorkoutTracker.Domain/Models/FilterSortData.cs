using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class FilterSortData
    {
        public string? SortBy { get; set; } = null;
        public int? TimesPerWeekFilter { get; set; } = null;
    }
}
