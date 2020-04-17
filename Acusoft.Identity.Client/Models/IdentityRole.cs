using System;
using System.Collections.Generic;

namespace Acusoft.Identity.Client.Models
{
    public class AcuRole
    {
        public Guid Id { get; internal set; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public AcuRole()
        { }

        public AcuRole(Guid id) : this()
        {
            Id = id;
        }
    }

}
