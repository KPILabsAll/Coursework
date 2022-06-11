using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class CPosition
    {
        public CPosition()
        {
            CEmployees = new HashSet<CEmployee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CEmployee> CEmployees { get; set; }
    }
}
