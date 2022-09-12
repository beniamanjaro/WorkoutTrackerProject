using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Identity
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
