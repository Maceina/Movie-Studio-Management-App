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
    public class AutoEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
        [DisplayName("Valstybinis numeris")]
        [MaxLength(6)]
        [Required]
        public string valstybinisNr { get; set; }
        [DisplayName("Pagaminimo data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime pagaminimoData { get; set; }
        [DisplayName("Rida")]
        [Required]
        public int rida { get; set; }
        [DisplayName("Radijas")]
        public bool radijas { get; set; }
        [DisplayName("Grotuvas")]
        public bool grotuvas { get; set; }
        [DisplayName("Kondicionierius")]
        public bool kondicionierius { get; set; }
        [DisplayName("Vietų skaičius")]
        [Required]
        public int vietuSkaicius { get; set; }
        [DisplayName("Registravimo data")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime registravimoData { get; set; }
        [DisplayName("Vertė")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal verte { get; set; }
        [DisplayName("Pavarų dėžė")]
        [Required]
        public int pavaruDeze { get; set; }
        [DisplayName("Degalų tipas")]
        [Required]
        public int degaluTipas { get; set; }
        [DisplayName("Kėbulo tipas")]
        [Required]
        public int kebulas { get; set; }
        [DisplayName("Bagažo dydis")]
        [Required]
        public int bagazoDydis { get; set; }
        [DisplayName("Būsena")]
        [Required]
        public int busena { get; set; }
        [DisplayName("Modelis")]
        [Required]
        public int fk_modelis { get; set; }

        //Sąrašai skirti pasirinkimams 
        public IList<SelectListItem> ModeliaiList { get; set; }
        public IList<SelectListItem> PavaruDezesList { get; set; }
        public IList<SelectListItem> KebuluList { get; set; }
        public IList<SelectListItem> DegaluTipaiList { get; set; }
        public IList<SelectListItem> BagazoList { get; set; }
        public IList<SelectListItem> BusenosList { get; set; }

    }
}