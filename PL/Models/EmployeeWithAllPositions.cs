using System.Collections.Generic;
using DAL.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Models
{
    public class EmployeeWithAllPositions
    {
        public CEmployee Employee { get; set; }
        public SelectList CPositions { get; set; }
    }
}