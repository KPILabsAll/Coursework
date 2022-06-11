using System;
using System.Linq;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ClientCompaniesController : Controller
    {
        private readonly ERMContext _context;

        public ClientCompaniesController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.CClientCompanies.ToList();
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
            var data = _context.CClientCompanies.FirstOrDefault(x => x.Id == id);  
            return View(data);  
        }  
        
        [HttpPost]  
        public ActionResult Edit(CClientCompany cClientCompany)  
        {  
            var data = _context.CClientCompanies.FirstOrDefault(x => x.Id == cClientCompany.Id);  
            if (data != null)  
            {  
                data.Name = cClientCompany.Name;
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
        public ActionResult Create(CClientCompany cClientCompany)  
        {
            if (cClientCompany != null)
            {
                _context.CClientCompanies.Add(cClientCompany);  
                _context.SaveChanges();  
            }
            return RedirectToAction("index");  
        }  
        
        public ActionResult Delete(int id)  
        {  
            var data = _context.CClientCompanies.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.CClientCompanies.Remove(data);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("index");  
        }  
    }
}