using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class SutartisController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        SutartisRepository sutartisRepository = new SutartisRepository();
        AutomobiliaiRepository automobiliaiRepository = new AutomobiliaiRepository();
        SutartiesBusenosRepository sutartiesBusenosRepository = new SutartiesBusenosRepository();
        MiestasRepository miestasRepository = new MiestasRepository();
        AikstelesRepository aikstelesRepository = new AikstelesRepository();
        DarbuotojasRepository darbuotojasRepository = new DarbuotojasRepository();
        KlientasRepository klientasRepository = new KlientasRepository();
        UzsakytaPaslaugaRepository uzsakytaPaslaugaRepository = new UzsakytaPaslaugaRepository();
        PaslaugaRepository paslaugaRepository = new PaslaugaRepository();
        PaslaugosKainaRepository paslaugosKainaRepository = new PaslaugosKainaRepository();
        // GET: Sutartis
        public ActionResult Index()
        {
            return View(sutartisRepository.getSutartys());
        }

        // GET: Sutartis/Create
        public ActionResult Create()
        {
            SutartisEditViewModel sutartis = new SutartisEditViewModel();
            //uzpildo pasirinkimo sąrašus
            PopulateSelections(sutartis);
            //grazinama paslaugos kurimo puslapį
            return View(sutartis);
        }

        // POST: Sutartis/Create
        [HttpPost]
        public ActionResult Create(SutartisEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //išsaugo nauja sutarti
                    sutartisRepository.addSutartis(collection);

                    //jei yra prideta paslaugų išųsaugojo ir paslaugas
                    if (collection.paslaugos != null)
                    {
                        foreach (var item in collection.paslaugos)
                        {
                            //html elemente sutarties id nenustatytas todel duomenis sutvarkome programiskai cia
                            if (item.fk_sutartis == 0)
                            {
                                item.fk_paslauga = Convert.ToInt32(item.fk_key.Substring(0, item.fk_key.IndexOf("_"))); // fk_key elemente išsaugotas paslaugos id iki _ simbolio
                                var ticks = item.fk_key.Substring(item.fk_key.IndexOf('_') + 1, item.fk_key.Length - (item.fk_key.IndexOf('_') + 1));
                                item.fk_kainaGaliojaNuo = new DateTime(Convert.ToInt64(ticks)); // ticks paverciami į datos reikšmę
                                item.fk_sutartis = collection.nr; // nustatomas sutarties nr
                            }
                        }

                        //kiekviena uzsakyta paslauga isaugojama duomenu bazeje
                        foreach (var item in collection.paslaugos)
                        {
                            uzsakytaPaslaugaRepository.insertUzsakytaPaslauga(item);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Sutartis/Edit/5
        public ActionResult Edit(int id)
        {
            SutartisEditViewModel sutartis = sutartisRepository.getSutartis(id);

            PopulateSelections(sutartis);
            List<UzsakytaPaslauga> paslaugos = new List<UzsakytaPaslauga>();
            paslaugos.Add(new UzsakytaPaslauga());
            ViewBag.paslaugos = paslaugos;
            decimal bendraSuma = 0;

            //Suskaiciuojama bendra suma
            bendraSuma += sutartis.kaina;

            foreach (var item in sutartis.paslaugos)
            {
                bendraSuma += item.kaina;
            }
            //paduodama bendra suma per viewbag kintamajį į vaizda
            ViewBag.bendraSuma = bendraSuma;

            return View(sutartis);
        }

        // POST: Sutartis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SutartisEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    sutartisRepository.updateSutartis(collection);

                    if (collection.paslaugos != null)
                    {

                        foreach (var item in collection.paslaugos)
                        {
                            //naujoms pridetos paslaugos sutvarkomi duomenys 
                            if (item.fk_sutartis == 0)
                            {
                                item.fk_paslauga = Convert.ToInt32(item.fk_key.Substring(0, item.fk_key.IndexOf("_"))); // nustatomas paslaugos id kuris issaugotas fk_key element iki _ simbolio
                                var ticks = item.fk_key.Substring(item.fk_key.IndexOf('_') + 1, item.fk_key.Length - (item.fk_key.IndexOf('_') + 1));
                                item.fk_kainaGaliojaNuo = new DateTime(Convert.ToInt64(ticks)); //nustatoma paslaugos kainos data
                                item.fk_sutartis = collection.nr; // nustatomas sutarties nr
                                item.kaina = paslaugosKainaRepository.getPaslaugosKaina(item.fk_paslauga, item.fk_kainaGaliojaNuo); //iš duomenų bazės paimama paslaugos kaina 
                            }
                        }

                        // istrina visas sutarties uzsakytas paslaugas
                        uzsakytaPaslaugaRepository.deleteUzsakytasPaslaugas(collection.nr);

                        //per nauja prideda visas sutarties uzsakytas paslaugas
                        foreach (var item in collection.paslaugos)
                        {
                            uzsakytaPaslaugaRepository.insertUzsakytaPaslauga(item);
                        }
                    }
                    else
                    {
                        uzsakytaPaslaugaRepository.deleteUzsakytasPaslaugas(collection.nr);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Sutartis/Delete/5
        public ActionResult Delete(int id)
        {
            SutartisEditViewModel sutartis = sutartisRepository.getSutartis(id);
            return View(sutartis);
        }

        // POST: Sutartis/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SutartisEditViewModel sutartis = sutartisRepository.getSutartis(id);
                if (sutartis.busena == 1 || sutartis.busena == 3)
                {
                    uzsakytaPaslaugaRepository.deleteUzsakytasPaslaugas(id);
                    sutartisRepository.deleteSutartis(id);
                } else
                {
                    ViewBag.naudojama = "Sutartis yra patvirtinta arba užbaigta, pašalinti negalima.";
                    return View(sutartis);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(SutartisEditViewModel sutartis)
        {
            //surenka sarasu informacija is duomenu bazes
            var automobiliai = automobiliaiRepository.getCars();
            var busenos = sutartiesBusenosRepository.getSutartiesBusenos();
            var darbuotojai = darbuotojasRepository.getDarbuotojai();
            var klientai = klientasRepository.getKlientai();
            var aiksteles = aikstelesRepository.getAiksteles();
            List<SelectListItem> selectListAutomobiliai = new List<SelectListItem>();
            List<SelectListItem> selectListBusenos = new List<SelectListItem>();
            List<SelectListItem> selectListDarbuotojai = new List<SelectListItem>();
            List<SelectListItem> selectListKlientai = new List<SelectListItem>();
            List<SelectListItem> selectListAiksteles = new List<SelectListItem>();

            //sukuria selectlistitem sarašus
            foreach (var item in automobiliai)
            {
                selectListAutomobiliai.Add(new SelectListItem { Value = Convert.ToString(item.id), Text = item.valstybinisNr + " - " + item.marke + " " + item.modelis });
            }

            foreach (var item in darbuotojai)
            {
                selectListDarbuotojai.Add(new SelectListItem { Value = item.tabelis, Text = item.vardas + " " + item.pavarde });
            }

            foreach (var item in klientai)
            {
                selectListKlientai.Add(new SelectListItem { Value = item.asmensKodas, Text = item.vardas + " " + item.pavarde });
            }

            foreach (var item in busenos)
            {
                selectListBusenos.Add(new SelectListItem { Value = Convert.ToString(item.id), Text = item.name });
            }

            foreach (var item in aiksteles)
            {
                selectListAiksteles.Add(new SelectListItem { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //priskiria sarašus vaizdo objektui
            sutartis.VietosList = selectListAiksteles;
            sutartis.DarbuotojaiList = selectListDarbuotojai;
            sutartis.KlientaiList = selectListKlientai;
            sutartis.BusenosList = selectListBusenos;
            sutartis.AutomobiliaiList = selectListAutomobiliai;

            sutartis.paslaugos = uzsakytaPaslaugaRepository.getUzsakytosPaslaugos(sutartis.nr);

            //uzsakytose paslaugose sukuria fk_key raktini elementą kuris naudojamas pasirinkime
            for (int i = 0; i < sutartis.paslaugos.Count; i++)
            {
                sutartis.paslaugos[i].fk_key = sutartis.paslaugos[i].fk_paslauga + "_" + sutartis.paslaugos[i].fk_kainaGaliojaNuo.Ticks;
            }

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<SelectListGroup> groups = new List<SelectListGroup>();

            var paslaugos = paslaugaRepository.getPaslaugos();

            List<PaslaugaEditViewModel> paslaugosViewModel = new List<PaslaugaEditViewModel>();

            foreach (var item in paslaugos)
            {
                paslaugosViewModel.Add(new PaslaugaEditViewModel { paslauga = item, paslaugosKainos = paslaugosKainaRepository.getPaslaugosKainos2(item.id) });
            }

            bool yra = false;
            //sukuriamos paslaugu kainu grupes
            foreach (var item in paslaugosViewModel)
            {
                yra = false;
                foreach (var i in groups)
                {
                    if (i.Name.Equals(item.paslauga.pavadinimas))
                    {
                        yra = true;
                    }
                }
                if (!yra)
                {
                    groups.Add(new SelectListGroup() { Name = item.paslauga.pavadinimas });
                }
            }

            //sudaromas pasirinkimo sarašas pagal paslaugu grupes
            foreach (var x in paslaugosViewModel)
            {
                foreach (var item in x.paslaugosKainos)
                {
                    var optGroup = new SelectListGroup() { Name = "--------" };
                    foreach (var i in groups)
                    {
                        if (i.Name.Equals(x.paslauga.pavadinimas))
                        {
                            optGroup = i;
                        }
                    }

                    selectListItems.Add(
                        new SelectListItem()
                        {
                            Value = Convert.ToString(item.fk_paslauga + "_" + item.galiojaNuo.Ticks),
                            Text = x.paslauga.pavadinimas + " " + item.kaina + " EUR (" + item.galiojaNuo + ")",
                            Group = optGroup
                        }
                        );
                }
            }

            //paslaugu kainu sarašas pagal paslaugu grupes priskiriamas vaizdo objektui
            sutartis.UzsPaslaugosList = selectListItems;
        }
    }
}
