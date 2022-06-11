using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class CIssue
    {
        public CIssue()
        {
            VTasks = new HashSet<VTask>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFixed { get; set; }
        public int? SystemId { get; set; }

        public virtual CErpsystem System { get; set; }
        public virtual ICollection<VTask> VTasks { get; set; }
    }
}
