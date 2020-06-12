using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class AtaskaitaController : Controller
    {
        AtaskaituRepository ataskaituRepository = new AtaskaituRepository();
        // GET: Ataskaita
        // Gali būti nenurodytos datos dėl to prie kintamuju ? 
        public ActionResult Index(DateTime ?nuo, DateTime ?iki)
        {
            // išrenka paslaugas
            PslgAtaskaitaViewModel pslgAtaskaita = ataskaituRepository.getBedraSumaUzsakytuPaslaugu(nuo, iki);
            pslgAtaskaita.paslaugos = ataskaituRepository.getUzsakytosPaslaugos(nuo, iki);
            //išsaugomos numatytos reiksmes datos intervalui
            pslgAtaskaita.nuo = nuo == null? null : nuo;
            pslgAtaskaita.iki = iki == null? null : iki;

            return View(pslgAtaskaita);
        }

        public ActionResult Sutartys(DateTime ?nuo, DateTime? iki)
        {
            //Sukuriamas ataskaitos vaizdo objektas ir užpildoma duomenimis
            SutartisAtaskViewModel ataskaita = new SutartisAtaskViewModel();
            ataskaita.nuo = nuo == null ? null : nuo;
            ataskaita.iki = iki == null ? null : iki;
            ataskaita.sutartys = ataskaituRepository.getAtaskaitaSutartciu(ataskaita.nuo, ataskaita.iki);
            //Suskaiciuojama bendra suma visų sutarčių
            foreach (var item in ataskaita.sutartys)
            {
                ataskaita.visoSumaSutartciu += item.kaina;
                ataskaita.visoSumaPaslauga += item.paslauguKaina;
            }

            return View(ataskaita);
        }

        public ActionResult Veluojancios(DateTime? nuo, DateTime? iki)
        {
            //Sukuriamas ataskaitos vaizdo objektoas ir užpildoma duomenimis
            VeluojanciosViewModel veluojancios = new VeluojanciosViewModel();
            veluojancios.nuo = nuo == null ? null : nuo;
            veluojancios.iki = iki == null ? null : iki;
            veluojancios.sutartys = ataskaituRepository.getVeluojanciosGrazinti(veluojancios.nuo, veluojancios.iki);
            return View(veluojancios);
        }
       
    }
}
