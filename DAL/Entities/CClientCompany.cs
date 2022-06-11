using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class CClientCompany
    {
        public CClientCompany()
        {
            CErpsystems = new HashSet<CErpsystem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CErpsystem> CErpsystems { get; set; }
    }
}
