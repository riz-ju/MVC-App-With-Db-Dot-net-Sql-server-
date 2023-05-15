using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperations;

namespace MVCAppWithDb.Controllers
{
    public class HomeController : Controller
    {

        EmployeeRepository repository = null;
        public HomeController()
        {
            repository = new EmployeeRepository();
        }

        // GET: Home
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel employeeModel)
        {

            if (ModelState.IsValid)
            {
               int id = repository.AddEmployee(employeeModel);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = "Data Added successfully";
                }

            }
           
            return View();
        }

        public ActionResult GetAllRecords()
        {
            var records = repository.GetAllRecords();
            return View(records);
        }

        public ActionResult Details(int id)
        {

            var records = repository.GetRecordsById(id);
            return View(records);
        }

        public ActionResult Edit(int id)
        {
            var employee = repository.GetRecordsById(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEmployee(model.Id, model);

            }
            
            return RedirectToAction("GetAllRecords");
        }

       // [HttpPost]
        public ActionResult Delete(int id)
        {
            repository.DeleteEmployee(id);
            return RedirectToAction("GetAllRecords");
        }
    }
}