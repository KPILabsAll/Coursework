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
    public class EmployeesController : Controller
    {
        private readonly ERMContext _context;

        public EmployeesController(ERMContext context)
        {
            _context = context;
        }

        public ActionResult Index()  
        {  
            var data = _context.CEmployees
                .Include(c => c.Position).ToList();
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
            var data = _context.CEmployees.FirstOrDefault(x => x.Id == id);
            var view = new EmployeeWithAllPositions()
            {
                Employee = data,
                CPositions = GetSelectListOfPositions()
            };
            return View(view);  
        }  
        
        [HttpPost]  
        public ActionResult Edit(EmployeeWithAllPositions cEmployee)  
        {  
            var data = _context.CEmployees.FirstOrDefault(x => x.Id == cEmployee.Employee.Id);  
            if (data != null)  
            {  
                data.Name = cEmployee.Employee.Name;
                data.Surname = cEmployee.Employee.Surname;
                data.Email = cEmployee.Employee.Email;
                data.PositionId = cEmployee.Employee.PositionId;
                data.Salary = cEmployee.Employee.Salary;
                _context.SaveChanges();  
            }  
  
            return RedirectToAction("index");  
        }  
        
        [HttpGet]  
        public ActionResult Create()  
        {  
            var view = new EmployeeWithAllPositions()
            {
                CPositions = GetSelectListOfPositions()
            };
            return View(view);  
        }

        [HttpPost]  
        public ActionResult Create(EmployeeWithAllPositions cEmployee)  
        {
            if (cEmployee != null)
            {
                _context.CEmployees.Add(cEmployee.Employee);  
                _context.SaveChanges();  
            }
            return RedirectToAction("index");  
        }  
        
        public ActionResult Delete(int id)  
        {  
            var data = _context.CEmployees.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.CEmployees.Remove(data);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("index");  
        }

        private SelectList GetSelectListOfPositions()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var positions = _context.CPositions.ToList();
            foreach (var position in positions)  
            {  
                list.Add(new SelectListItem()  
                {  
                    Text = position.Name,  
                    Value = position.Id.ToString()  
                });  
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}