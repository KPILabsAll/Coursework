using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class VTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsDone { get; set; }
        public int? IssueId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual CEmployee Employee { get; set; }
        public virtual CIssue Issue { get; set; }
    }
}
