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
    public class IssuesController : Controller
    {
        private readonly ERMContext _context;

        public IssuesController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.CIssues
                .Include(c => c.System).ToList();
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
            var data = _context.CIssues.FirstOrDefault(x => x.Id == id);
            var view = new IssueWithAllSystems()
            {
                Issue = data,
                Systems = GetSelectListOfSystems()
            };
            return View(view);  
        }  
        
        [HttpPost]  
        public ActionResult Edit(IssueWithAllSystems problem)  
        {  
            var data = _context.CIssues.FirstOrDefault(x => x.Id == problem.Issue.Id);  
            if (data != null)
            {
                data.Description = problem.Issue.Description;
                data.IsAccepted = problem.Issue.IsAccepted;
                data.IsFixed = problem.Issue.IsFixed;
                data.SystemId = problem.Issue.SystemId;
                _context.SaveChanges();  
            }  
  
            return RedirectToAction("index");  
        }  
        
        [HttpGet]  
        public ActionResult Create()  
        {  
            var view = new IssueWithAllSystems()
            {
                Systems = GetSelectListOfSystems()
            };
            return View(view);  
        }

        [HttpPost]  
        public ActionResult Create(IssueWithAllSystems problem)  
        {
            if (problem != null)
            {
                _context.CIssues.Add(problem.Issue);  
                _context.SaveChanges();  
            }
            return RedirectToAction("index");  
        }  
        
        public ActionResult Delete(int id)  
        {  
            var data = _context.CIssues.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.CIssues.Remove(data);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("index"); 
        }

        private SelectList GetSelectListOfSystems()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var systems = _context.CErpsystems.ToList();
            foreach (var system in systems)  
            {  
                list.Add(new SelectListItem()  
                {  
                    Text = system.Name,  
                    Value = system.Id.ToString()  
                });  
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}