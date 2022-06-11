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
    public class TasksController : Controller
    {
        private readonly ERMContext _context;

        public TasksController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.VTasks
                .Include(t => t.Employee)
                .Include(t => t.Issue)
                .ToList();
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
            var data = _context.VTasks.FirstOrDefault(x => x.Id == id);
            var view = new TaskWIthAllEmployeesAndIssues()
            {
                Task = data,
                Issues = GetSelectListOfIssues(),
                Employees = GetSelectListOfEmployees()
            };
            return View(view);  
        }  
        
        [HttpPost]  
        public ActionResult Edit(TaskWIthAllEmployeesAndIssues assignment)  
        {  
            var data = _context.VTasks.FirstOrDefault(x => x.Id == assignment.Task.Id);  
            if (data != null)
            {
                data.Description = assignment.Task.Description;
                data.Deadline = assignment.Task.Deadline;
                data.IsDone = assignment.Task.IsDone;
                data.IssueId = assignment.Task.IssueId;
                data.EmployeeId = assignment.Task.EmployeeId;
                _context.SaveChanges();  
            }  
  
            return RedirectToAction("index");  
        }  
        
        [HttpGet]  
        public ActionResult Create()  
        {  
            var view = new TaskWIthAllEmployeesAndIssues()
            {
                Issues = GetSelectListOfIssues(),
                Employees = GetSelectListOfEmployees()
            };
            return View(view);  
        }

        [HttpPost]  
        public ActionResult Create(TaskWIthAllEmployeesAndIssues assignment)  
        {
            if (assignment != null)
            {
                _context.VTasks.Add(assignment.Task);  
                _context.SaveChanges();  
            }
            return RedirectToAction("index");  
        }  
        
        public ActionResult Delete(int id)  
        {  
            var data = _context.VTasks.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.VTasks.Remove(data);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("index"); 
        }

        private SelectList GetSelectListOfEmployees()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var employees = _context.CEmployees.ToList();
            foreach (var employee in employees)  
            {  
                list.Add(new SelectListItem()  
                {  
                    Text = employee.Surname + " " + employee.Name,  
                    Value = employee.Id.ToString()  
                });  
            }
            return new SelectList(list, "Value", "Text");
        }
        
        private SelectList GetSelectListOfIssues()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var issues = _context.CIssues.ToList();
            foreach (var issue in issues)  
            {  
                list.Add(new SelectListItem()  
                {  
                    Text = issue.Description,  
                    Value = issue.Id.ToString()  
                });  
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}