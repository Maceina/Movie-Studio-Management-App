using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

// Vietoje Modelis operuojame su IRANGA duomenimis.

namespace AutoNuoma.Controllers
{
    public class ModelisController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        ModeliuRepository modeliuRepository = new ModeliuRepository();
         Marke3Repository markeRepository = new Marke3Repository();
        // GET: Modelis
        public ActionResult Index()
        {
            return View(modeliuRepository.getModeliai());
        }

        // GET: Modelis/Create
        public ActionResult Create()
        {
            ModelisEditViewModel modelis = new ModelisEditViewModel();
            PopulateSelections(modelis);
            return View(modelis);
        }

        // POST: Modelis/Create
        [HttpPost]
        public ActionResult Create(ModelisEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    modeliuRepository.addModelis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Modelis/Edit/5
        public ActionResult Edit(int id)
        {
            ModelisEditViewModel modelis = modeliuRepository.getModelis(id);
            PopulateSelections(modelis);
            return View(modelis);
        }

        // POST: Modelis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ModelisEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    modeliuRepository.updateModelis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Modelis/Delete/5
        public ActionResult Delete(int id)
        {
            ModelisEditViewModel modelis = modeliuRepository.getModelis(id);
            return View(modelis);
        }

        // POST: Modelis/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
                    modeliuRepository.deleteModelis(id);
            
                return View();
          
        }

        public void PopulateSelections(ModelisEditViewModel modelis)
        {
            var markes = markeRepository.getMarkes();
            List<SelectListItem> selectListmarkes = new List<SelectListItem>();

            foreach (var item in markes)
            {
                selectListmarkes.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            modelis.MarkesList = selectListmarkes;
        }
    }
}
