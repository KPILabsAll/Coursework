using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class CErpsystem
    {
        public CErpsystem()
        {
            CIssues = new HashSet<CIssue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentVersion { get; set; }
        public int? ClientId { get; set; }

        public virtual CClientCompany Client { get; set; }
        public virtual ICollection<CIssue> CIssues { get; set; }
    }
}
