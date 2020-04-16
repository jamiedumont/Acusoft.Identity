using System;
using System.Collections.Generic;

namespace Acusoft.Identity.Client.Models
{
    public class IdentityRole
    {
        public Guid Id { get; internal set; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public IdentityRole()
        { }

        public IdentityRole(Guid id) : this()
        {
            Id = id;
        }
    }

}
