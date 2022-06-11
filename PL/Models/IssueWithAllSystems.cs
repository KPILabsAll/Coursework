using DAL.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Models
{
    public class IssueWithAllSystems
    {
        public CIssue Issue { get; set; }
        public SelectList Systems { get; set; }
    }
}