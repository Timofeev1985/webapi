using System;
using System.Collections.Generic;

namespace webapi.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public byte[]? Photo { get; set; }

    public int? Role { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
