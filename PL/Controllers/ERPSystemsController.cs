using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PL.Models;

namespace PL.Controllers
{
    public class ERPSystemsController : Controller
    {
        private readonly ERMContext _context;

        public ERPSystemsController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.CErpsystems
                .Include(c => c.Client).ToList();
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
            var data = _context.CErpsystems.FirstOrDefault(x => x.Id == id);
            var view = new ErpSystemWithAllClients()
            {
                Erpsystem = data,
                ClientCompanies = GetSelectListOfClients()
            };
            return View(view);  
        }  
        
        [HttpPost]  
        public ActionResult Edit(ErpSystemWithAllClients cERPSystem)  
        {  
            var data = _context.CErpsystems.FirstOrDefault(x => x.Id == cERPSystem.Erpsystem.Id);  
            if (data != null)  
            {  
                data.Name = cERPSystem.Erpsystem.Name;
                data.CurrentVersion = cERPSystem.Erpsystem.CurrentVersion;
                data.ClientId = cERPSystem.Erpsystem.ClientId;
                _context.SaveChanges();  
            }  
  
            return RedirectToAction("index");  
        }  
        
        [HttpGet]  
        public ActionResult Create()  
        {  
            var view = new ErpSystemWithAllClients()
            {
                ClientCompanies = GetSelectListOfClients()
            };
            return View(view);  
        }

        [HttpPost]  
        public ActionResult Create(ErpSystemWithAllClients cERPSystem)  
        {
            if (cERPSystem != null)
            {
                _context.CErpsystems.Add(cERPSystem.Erpsystem);  
                _context.SaveChanges();  
            }
            return RedirectToAction("index");  
        }  
        
        public ActionResult Delete(int id)  
        {  
            var data = _context.CErpsystems.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.CErpsystems.Remove(data);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("index");  
        }

        private SelectList GetSelectListOfClients()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var clients = _context.CClientCompanies.ToList();
            foreach (var client in clients)  
            {  
                list.Add(new SelectListItem()  
                {  
                    Text = client.Name,  
                    Value = client.Id.ToString()  
                });  
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}