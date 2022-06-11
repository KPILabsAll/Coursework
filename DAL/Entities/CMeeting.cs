using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Model
{
    public partial class CMeeting
    {
        public CMeeting()
        {
            VEmployeeAtMeetings = new HashSet<VEmployeeAtMeeting>();
        }

        public int Id { get; set; }
        public string Topic { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual ICollection<VEmployeeAtMeeting> VEmployeeAtMeetings { get; set; }
    }
}
