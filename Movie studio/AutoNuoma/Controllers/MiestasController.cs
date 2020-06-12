using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Models;
using AutoNuoma.Repos;

namespace AutoNuoma.Controllers
{
    public class MiestasController : Controller
    {
        MiestasRepository miestasRepository = new MiestasRepository();
        // GET: Miestas
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(miestasRepository.getMiestai());
        }

        // GET: Miestas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Miestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Miestas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Miestas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Miestas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Miestas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Miestas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
