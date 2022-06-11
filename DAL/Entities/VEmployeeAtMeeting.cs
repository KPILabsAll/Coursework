using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class VEmployeeAtMeeting
    {
        public int EmployeeId { get; set; }
        public int MeetingId { get; set; }

        public virtual CEmployee Employee { get; set; }
        public virtual CMeeting Meeting { get; set; }
    }
}
