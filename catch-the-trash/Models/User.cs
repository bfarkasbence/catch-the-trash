using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catch_the_trash.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
