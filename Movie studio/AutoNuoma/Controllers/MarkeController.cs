using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class MarkeController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        MarkeRepository markeRepository = new MarkeRepository();
        // GET: Marke
        public ActionResult Index()
        {
            //grazinamas markiu sarašas
            return View(markeRepository.getMarkes());
        }

        // GET: Marke/Create
        public ActionResult Create()
        {
            Marke marke = new Marke();
            return View(marke);
        }

        // POST: Marke/Create
        [HttpPost]
        public ActionResult Create(Marke collection)
        {
            try
            {
                // išsaugo nauja markę duomenų bazėje
                if (ModelState.IsValid)
                {
                    markeRepository.addMarke(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Marke/Edit/5
        public ActionResult Edit(int id)
        {
            return View(markeRepository.getMarke(id));
        }

        // POST: Marke/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Marke collection)
        {
            try
            {
                // atnajina markes informacija
                if (ModelState.IsValid)
                {
                    markeRepository.updateMarke(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Marke/Delete/5
        public ActionResult Delete(int id)
        {
            return View(markeRepository.getMarke(id));
        }

        // POST: Marke/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            markeRepository.deleteMarke(id);


            return View();
        }
    }
}
