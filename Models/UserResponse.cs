using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Entities;

namespace webapi.Models
{
    public class UserResponse
    {
    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public int? Role { get; set; }
    public UserResponse(User user){
        Lastname = user.Lastname;
        Firstname = user.Firstname;
        Middlename = user.Middlename;
        Role = user.Role;
    }
    }
}