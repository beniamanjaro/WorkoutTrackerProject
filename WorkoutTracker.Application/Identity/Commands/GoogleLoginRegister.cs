using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Application.Identity.Commands
{
    public class GoogleLoginRegister : IRequest<UserManagerResponse>
    {
        public string IdToken { get; set; }

    }
}
