using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Entities
{
    public partial class User
    {
        public User()
        {

        }
        public User(UserRequest model)
        {
            Lastname = model.Lastname;
            Firstname = model.Firstname;
            Middlename = model.Middlename;
            Role = model.Role;
        }
         public void Copy(UserRequest model)
        {
            Lastname = model.Lastname;
            Firstname = model.Firstname;
            Middlename = model.Middlename;
            Role = model.Role;
        }
    }
}