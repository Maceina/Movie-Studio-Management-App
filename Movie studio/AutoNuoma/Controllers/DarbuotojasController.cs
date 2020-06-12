using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class DarbuotojasController : Controller
    {
        //Apibrežiamos saugyklos kurios naudojamos šiame valdiklyje
        // GET: Darbuotojas
        DarbuotojasRepository darbuotojasRepository = new DarbuotojasRepository();
        public ActionResult Index()
        {
            //gražinamas darbuotoju sarašo vaizdas
            return View(darbuotojasRepository.getDarbuotojai());
        }

        // GET: Darbuotojas/Create
        public ActionResult Create()
        {
            //Grazinama darbuotojo kūrimo forma
            Darbuotojas darbuotojas = new Darbuotojas();
            return View(darbuotojas);
        }

        // POST: Darbuotojas/Create
        [HttpPost]
        public ActionResult Create(Darbuotojas collection)
        {
            try
            {
                // Patikrinama ar tokiod arbuotojo nėra duomenų bazėje
                Darbuotojas tmpDarbuotojas = darbuotojasRepository.getDarbuotojas(collection.tabelis);

                if (tmpDarbuotojas.tabelis!=null)
                {
                    ModelState.AddModelError("tabelis", "Darbuotojas su tokiu tabelio numeriu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                //Jei darbuotojo su tabelio nr neranda prideda naują
                if (ModelState.IsValid)
                {
                    darbuotojasRepository.addDarbuotojas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Darbuotojas/Edit/5
        public ActionResult Edit(string id)
        {
            //grazinama darbuotojo redagavimo forma
            return View(darbuotojasRepository.getDarbuotojas(id));
        }

        // POST: Darbuotojas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Darbuotojas collection)
        {
            try
            {
                // Atnaujina darbuotojo informacija
                if (ModelState.IsValid)
                {
                    darbuotojasRepository.updateDarbuotojas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Darbuotojas/Delete/5
        public ActionResult Delete(string id)
        {
            return View(darbuotojasRepository.getDarbuotojas(id));
        }

        // POST: Darbuotojas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (darbuotojasRepository.getDarbuotojasSutarciuCount(id)>0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Darbuotojas turi sudarytų sutarčių, pašalinti negalima.";
                    return View(darbuotojasRepository.getDarbuotojas(id));
                }

                if (!naudojama)
                {
                    darbuotojasRepository.deleteDarbuotojas(id);
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
