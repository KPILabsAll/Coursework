using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class CEmployee
    {
        public CEmployee()
        {
            VEmployeeAtMeetings = new HashSet<VEmployeeAtMeeting>();
            VTasks = new HashSet<VTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public int? PositionId { get; set; }
        public decimal? Salary { get; set; }

        public string GetFullName => Surname + " " + Name;

        public virtual CPosition Position { get; set; }
        public virtual ICollection<VEmployeeAtMeeting> VEmployeeAtMeetings { get; set; }
        public virtual ICollection<VTask> VTasks { get; set; }
    }
}
