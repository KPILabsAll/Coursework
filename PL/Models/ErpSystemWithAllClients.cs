using DAL.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Models
{
    public class ErpSystemWithAllClients
    {
        public CErpsystem Erpsystem { get; set; }
        public SelectList ClientCompanies { get; set; }
    }
}