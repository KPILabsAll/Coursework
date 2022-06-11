using DAL.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Models
{
    public class TaskWIthAllEmployeesAndIssues
    {
        public VTask Task { get; set; }
        public SelectList Employees { get; set; }
        public SelectList Issues { get; set; }
    }
}