using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class AutomobilisController : Controller
    {
        //Apibrežiamos saugyklos kurios bus naudojamos šiame valdiklyje
        AutomobiliaiRepository automobiliaiRepository = new AutomobiliaiRepository();
        ModeliuRepository modeliuRepository = new ModeliuRepository();
        PavaruDezeRepository pavaruRepository = new PavaruDezeRepository();
        KebulasRepository kebuluRepository = new KebulasRepository();
        DegaluTipasRepository degaluTipaiRepository = new DegaluTipasRepository();
        BagazuRepository bagazuTipaiRepository = new BagazuRepository();
        AutoBusenaRepository autoBusenaRepository = new AutoBusenaRepository();
        // GET: Automobilis
        //Gražinamas automobiliu sąrašo vaizdas
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(automobiliaiRepository.getCars());
        }

        // GET: Automobilis/Create
        public ActionResult Create()
        {
            AutoEditViewModel autoEditViewModel = new AutoEditViewModel();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            PopulateSelections(autoEditViewModel);
            return View(autoEditViewModel);
        }

        // POST: Automobilis/Create
        [HttpPost]
        public ActionResult Create(AutoEditViewModel collection)
        {
            try
            {
                //Pridedamas naujas automobilis
                automobiliaiRepository.addAuto(collection);

                //Nukreipia i sąrašą
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Automobilis/Edit/5
        public ActionResult Edit(int id)
        {
            //Surenkama automobilio informacija iš duomenų bazės
            AutoEditViewModel autoEditViewModel = automobiliaiRepository.getCar(id);
            //Užpildomi pasirinkimų sąrašai
            PopulateSelections(autoEditViewModel);
            return View(autoEditViewModel);
        }

        // POST: Automobilis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AutoEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                automobiliaiRepository.updateAuto(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Automobilis/Delete/5
        public ActionResult Delete(int id)
        {
            AutoEditViewModel autoEditViewModel = automobiliaiRepository.getCar(id);
            return View(autoEditViewModel);
        }

        // POST: Automobilis/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (automobiliaiRepository.getAutomobilisSutarciuCount(id)>0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Automobilis naudojamas sutartyse, negalima pašalinti.";
                    return View(automobiliaiRepository.getCar(id));
                }

                if (!naudojama)
                {
                    automobiliaiRepository.deleteAutomobilis(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(AutoEditViewModel autoEditViewModel)
        {
            var modeliai = modeliuRepository.getModeliai();
            var pavaruDezes = pavaruRepository.getPavaruDezes();
            var kebulai = kebuluRepository.getKebuloTipai();
            var degalai = degaluTipaiRepository.getDegaluTipai();
            var bagazai = bagazuTipaiRepository.getBagazai();
            var busenos = autoBusenaRepository.getBusenos();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<SelectListItem> selectListkebulai = new List<SelectListItem>();
            List<SelectListItem> selectListpavarudezes = new List<SelectListItem>();
            List<SelectListItem> selectListdegalai = new List<SelectListItem>();
            List<SelectListItem> selectListlagaminai = new List<SelectListItem>();
            List<SelectListItem> selectListBusenos = new List<SelectListItem>();
            List<SelectListGroup> groups = new List<SelectListGroup>();
            bool yra = false;

            //Sukuriamos pasirinkimo grupės
            foreach (var item in modeliai)
            {
                yra = false;
                foreach (var i in groups)
                {
                    if (i.Name.Equals(item.marke))
                    {
                        yra = true;
                    }
                }
                if (!yra)
                {
                    groups.Add(new SelectListGroup() { Name = item.marke });
                }
            }

            //Užpildomas pasirinkimo sąrašas pagal grupes(markes) autombolių modelių
            foreach (var item in modeliai)
            {
                var optGroup = new SelectListGroup() { Name = "--------" };
                foreach (var i in groups)
                {
                    if (i.Name.Equals(item.marke))
                    {
                        optGroup = i;
                    }
                }
                selectListItems.Add(
                    new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas, Group = optGroup }
                    );
            }


            //užpildomas kebulų sąrašas iš duomenų bazės
            foreach (var item in kebulai)
            {
                selectListkebulai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Užpildomas degalų tipų sąrašas iš duomenų bazės
            foreach (var item in degalai)
            {
                selectListdegalai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Užpildomas pavarų dežių sąrašas iš duomenų bazės
            foreach (var item in pavaruDezes)
            {
                selectListpavarudezes.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Užpildomas bagažo tipo sąrašas iš duomenų bazės
            foreach (var item in bagazai)
            {
                selectListlagaminai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Užpildomas būsenų sąrašas iš duomenų bazės
            foreach (var item in busenos)
            {
                selectListBusenos.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Sarašai priskiriami vaizdo objektui
            autoEditViewModel.ModeliaiList = selectListItems;
            autoEditViewModel.PavaruDezesList = selectListpavarudezes;
            autoEditViewModel.KebuluList = selectListkebulai;
            autoEditViewModel.DegaluTipaiList = selectListdegalai;
            autoEditViewModel.BagazoList = selectListlagaminai;
            autoEditViewModel.BusenosList = selectListBusenos;
        }

    }
}
