using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class PaslaugaController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos šiame valdiklyje
        PaslaugaRepository paslaugaRepository = new PaslaugaRepository();
        PaslaugosKainaRepository paslaugosKainaRepository = new PaslaugosKainaRepository();
        // GET: Paslauga
        public ActionResult Index()
        {

            return View(paslaugaRepository.getPaslaugos());
        }      

        // GET: Paslauga/Create
        public ActionResult Create()
        {
            var paslauga = new PaslaugaEditViewModel();
            return View(paslauga);
        }

        // POST: Paslauga/Create
        [HttpPost]
        public ActionResult Create(PaslaugaEditViewModel collection)
        {
            try
            {
                // Patikrina ar pavyko iterpti paslauga
                int paslauga_id = paslaugaRepository.insertPaslauga(collection.paslauga);

                if (paslauga_id < 0)
                {
                    ViewBag.failed = "Nepavyko iterpti paslaugos";
                    return View(collection);
                }
                if (collection.paslaugosKainos != null)
                {
                    //Jei pavyko iterpti prideda ir visas kainas apibreztas šiai paslaugai
                    foreach (var item in collection.paslaugosKainos)
                    {
                        item.fk_paslauga = paslauga_id;
                        paslaugosKainaRepository.insertPaslaugosKaina(item);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.klaida = "Ivyko nenumatyta klaida";
                return View(collection);
            }
        }

        // GET: Paslauga/Edit/5
        public ActionResult Edit(int id)
        {
            PaslaugaEditViewModel editViewModel = new PaslaugaEditViewModel();

            editViewModel.paslauga = paslaugaRepository.getPaslauga(id);
            editViewModel.paslaugosKainos = paslaugosKainaRepository.getPaslaugosKainos2(id);
            return View(editViewModel);
        }

        // POST: Paslauga/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PaslaugaEditViewModel collection)
        {
            try
            {
                // Atnaujina paslaugos informacija

                paslaugaRepository.updatePaslauga(collection.paslauga);

                paslaugosKainaRepository.deletePaslaugosKainos(id);

                if (collection.paslaugosKainos != null)
                {
                    foreach (var item in collection.paslaugosKainos)
                    {
                        if (item.kiekis == 0)
                        {
                            //Pridant naujas paslaugas html element nėra nustatytas paslaugos id todėl reikia nustatyti cia
                            if (item.fk_paslauga == 0)
                            {
                                item.fk_paslauga = id;
                            }
                            paslaugosKainaRepository.insertPaslaugosKaina(item);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Paslauga/Delete/5
        public ActionResult Delete(int id)
        {
            PaslaugaEditViewModel editViewModel = new PaslaugaEditViewModel();

            editViewModel.paslauga = paslaugaRepository.getPaslauga(id);
            editViewModel.paslaugosKainos = paslaugosKainaRepository.getPaslaugosKainos2(id);
            return View(editViewModel);
        }

        // POST: Paslauga/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PaslaugaEditViewModel editViewModel = new PaslaugaEditViewModel();

                editViewModel.paslauga = paslaugaRepository.getPaslauga(id);
                editViewModel.paslaugosKainos = paslaugosKainaRepository.getPaslaugosKainos2(id);
                bool naudojama = false;

                foreach (var item in editViewModel.paslaugosKainos)
                {
                    if (item.kiekis>0)
                    {
                        naudojama = true;
                    }
                }

                if (!naudojama)
                {
                    paslaugosKainaRepository.deletePaslaugosKainos(id);
                    paslaugaRepository.deletePaslauga(id);
                }

                if (naudojama)
                {
                    ViewBag.naudojama = "Paslauga yra naudojama sutartyse, pašalinti negalima";
                    return View(editViewModel);
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
