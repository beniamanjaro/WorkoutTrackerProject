using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class TopUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Frequency { get; set; }
    }
}
