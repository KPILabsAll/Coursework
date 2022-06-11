using System.Linq;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ERMContext _context;

        public MeetingsController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.CMeetings
                .ToList();
            if (TempData["Message"] is not null)
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }
            return View(data);  
        } 
    }
}