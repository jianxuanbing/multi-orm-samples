using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.SD1;
using Domain;


namespace SDSampleMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleService _peopleServie;

        public HomeController(IPeopleService peopleService)
        {
            _peopleServie = peopleService;
        }

        public ActionResult Index()
        {
            var models = _peopleServie.GetAll();

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
                _peopleServie.CreatePerson(person);
            }
            catch (Exception)
            {
                return View("NotFound");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = _peopleServie.Find(id);
            if (model == null)
                return View("NotFound");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            try
            {
                _peopleServie.EditPerson(person);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("NotFound");
            }


        }

        public ActionResult Delete(int id)
        {
            var model = _peopleServie.Find(id);
            if (model == null)
                return View("NotFound");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Person model)
        {
            try
            {
                _peopleServie.DeletePerson(model);
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
