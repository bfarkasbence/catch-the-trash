using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catch_the_trash.Models
{
    public class User : IdentityUser
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }
    }
}
