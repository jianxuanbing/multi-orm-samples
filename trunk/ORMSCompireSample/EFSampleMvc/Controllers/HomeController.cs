using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.EF4;
using Domain;

namespace EFSampleMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleService _peopleService;

        public HomeController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }
        public ActionResult Index()
        {
            var models = _peopleService.GetAll();

            return View(models);
        }

        public ActionResult Create()
        {
            var model = new Person();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                _peopleService.CreatePerson(person);
            }
            catch (Exception)
            {
                return View("NotFound");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = _peopleService.Find(id);
            if (model == null)
                return View("NotFound");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            try
            {
                _peopleService.EditPerson(person);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("NotFound");
            }


        }

        public ActionResult Delete(int id)
        {
            var model = _peopleService.Find(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Person model)
        {
            try
            {
                _peopleService.DeletePerson(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("NotFound");
            }
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
