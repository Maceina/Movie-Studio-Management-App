using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Models;


namespace AutoNuoma.ViewModels
{
    public class SutartisEditViewModel
    {
        [DisplayName("Sutarties nr.")]
        [Required]
        public int nr { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Sutarties data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime sutartiesData { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Nuomos data ir laikas")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime nuomosDataLaikas { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Planuojama gražinimo data ir laikas")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime planuojamaGrDataLaikas { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Faktinė gražinimo data ir laikas")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime faktineGrDataLaikas { get; set; }
        [DisplayName("Pradinė rida")]
        public int pradineRida { get; set; }
        [DisplayName("Galinė rida")]
        public int? galineRida { get; set; }
        [DisplayName("Nuomos kaina")]
        [Required]
        public decimal kaina { get; set; }
        [DisplayName("Degalų kiekis paimant")]
        [Required]
        public int degaluKiekisPaimant { get; set; }
        [DisplayName("Degalų kiekis gražinus")]
        public int? degaluKiekisGrazinant { get; set; }
        [DisplayName("Būsena")]
        [Required]
        public int busena { get; set; }
        [DisplayName("Klientas")]
        [Required]
        public string fk_klientas { get; set; }
        [DisplayName("Darbuotojas")]
        [Required]
        public string fk_darbuotojas { get; set; }
        [DisplayName("Automobilis")]
        [Required]
        public int fk_automobilis { get; set; }
        [DisplayName("Paėmimo vieta")]
        [Required]
        public int fk_grazinimoVieta { get; set; }
        [DisplayName("Gražinimo vieta")]
        public int fk_paemimoVieta { get; set; }
        //Užsakytų papildomų paslaugų sąrašas
        public virtual List<UzsakytaPaslauga> paslaugos {get;set;}

        //Sąrašai skirti sugeneruoti pasirinkimams
        public IList<SelectListItem> KlientaiList { get; set; }
        public IList<SelectListItem> DarbuotojaiList { get; set; }
        public IList<SelectListItem> BusenosList { get; set; }
        public IList<SelectListItem> AutomobiliaiList { get; set; }
        public IList<SelectListItem> VietosList { get; set; }
        public IList<SelectListItem> UzsPaslaugosList { get; set; }
    }
}