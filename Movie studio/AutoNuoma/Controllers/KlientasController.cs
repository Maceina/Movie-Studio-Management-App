using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class KlientasController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos šiame valdiklyje
        KlientasRepository klientasRepository = new KlientasRepository();
        // GET: Klientas
        public ActionResult Index()
        {
            //grazinamas klientų sarašas
            return View(klientasRepository.getKlientai());
        }

        // GET: Klientas/Create
        public ActionResult Create()
        {
            Klientas klientas = new Klientas();
            return View(klientas);
        }

        // POST: Klientas/Create
        [HttpPost]
        public ActionResult Create(Klientas collection)
        {
            try
            {
                // Patikrinama ar klientas su tokiu asmens kodu jau egzistuoja
                Klientas tmpKlientas = klientasRepository.getKlientas(collection.asmensKodas);
                if (tmpKlientas.asmensKodas!=null)
                {
                    ModelState.AddModelError("asmensKodas", "Klientas su tokiu asmens kodu jau užregistruotas");
                    return View(collection);
                }
                //Jei nera sukuria nauja klienta
                if (ModelState.IsValid)
                {
                    klientasRepository.addKlientas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Edit/5
        public ActionResult Edit(string id)
        {
            return View(klientasRepository.getKlientas(id));
        }

        // POST: Klientas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Klientas collection)
        {
            try
            {
                // Atnaujina kliento informacija
                if (ModelState.IsValid)
                {
                    klientasRepository.updateKlientas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Delete/5
        public ActionResult Delete(string id)
        {
            return View(klientasRepository.getKlientas(id));
        }

        // POST: Klientas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {


                    klientasRepository.deleteKlientas(id);

                return View();
        }
    }
}
