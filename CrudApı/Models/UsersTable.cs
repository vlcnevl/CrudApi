using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudApı.Models
{
    public class UsersTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }

    }
}