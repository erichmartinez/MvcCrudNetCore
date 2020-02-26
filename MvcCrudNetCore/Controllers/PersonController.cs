using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCrudNetCore.Models;
using Microsoft.Extensions.Configuration;

namespace MvcCrudNetCore.Controllers
{
    public class PersonController : Controller
    {
        private IConfiguration _configuration;

        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: Person
        public ActionResult Index()
        {
            PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
            return View(PersonHandler.GetPersonList());
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
            return View(PersonHandler.GetPersonList().Find(personmodel => personmodel.ID == id));
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonModel iList)
        {
            try
            {
                PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
                if (PersonHandler.InsertPerson(iList))
                {
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
            return View(PersonHandler.GetPersonList().Find(personmodel => personmodel.ID == id));
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PersonModel iList)
        {
            try
            {
                PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
                PersonHandler.UpdatePerson(iList);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
            return View(PersonHandler.GetPersonList().Find(personmodel => personmodel.ID == id));
        }

        // POST: Person/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                PersonDBHandler PersonHandler = new PersonDBHandler(_configuration);
                if (PersonHandler.DeletePerson(id))
                {
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}