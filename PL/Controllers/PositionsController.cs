using System;
using System.Linq;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ERMContext _context;

        public PositionsController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.CPositions.ToList();
            if (TempData["Message"] is not null)
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }
            return View(data);  
        } 
        
        [HttpGet]  
        public ActionResult Edit(int id)  
        {               
            var data = _context.CPositions.FirstOrDefault(x => x.Id == id);  
            return View(data);  
        }  
        
        [HttpPost]  
        public ActionResult Edit(CPosition cPosition)  
        {  
            var data = _context.CPositions.FirstOrDefault(x => x.Id == cPosition.Id);  
            if (data != null)  
            {  
                data.Name = cPosition.Name;
                _context.SaveChanges();  
            }  
  
            return RedirectToAction("index");  
        }  
        
        [HttpGet]  
        public ActionResult Create()  
        {  
            return View();  
        }

        [HttpPost]  
        public ActionResult Create(CPosition cPosition)  
        {
            if (cPosition != null)
            {
                _context.CPositions.Add(cPosition);  
                _context.SaveChanges();  
            }
            return RedirectToAction("index");  
        }  
        
        public ActionResult Delete(int id)  
        {  
            var data = _context.CPositions.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.CPositions.Remove(data);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Message"] = "You must delete all employees of this position";
            }
            return RedirectToAction("index");  
        }  
    }
}