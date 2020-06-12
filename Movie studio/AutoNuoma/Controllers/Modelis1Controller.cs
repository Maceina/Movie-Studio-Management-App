﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

// Vietoje Modelis operuojame su Darbo sustartis duomenimis.

namespace AutoNuoma.Controllers
{
    public class Modelis1Controller : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        Modeliu1Repository modeliuRepository = new Modeliu1Repository();
         Marke3Repository markeRepository = new Marke3Repository();
        // GET: Modelis
        public ActionResult Index()
        {
            return View(modeliuRepository.getModeliai());
        }

        // GET: Modelis/Create
        public ActionResult Create()
        {
            Modelis1EditViewModel modelis = new Modelis1EditViewModel();
            PopulateSelections(modelis);
            return View(modelis);
        }

        // POST: Modelis/Create
        [HttpPost]
        public ActionResult Create(Modelis1EditViewModel collection)
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
            Modelis1EditViewModel modelis = modeliuRepository.getModelis(id);
            PopulateSelections(modelis);
            return View(modelis);
        }

        // POST: Modelis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Modelis1EditViewModel collection)
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
            Modelis1EditViewModel modelis = modeliuRepository.getModelis(id);
            return View(modelis);
        }

        // POST: Modelis/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
                    modeliuRepository.deleteModelis(id);
            
                return View();
          
        }

        public void PopulateSelections(Modelis1EditViewModel modelis)
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
