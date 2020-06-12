using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoNuoma.ViewModels
{
    public class ModelisEditViewModel
    {
        [DisplayName("Irangos ID")]
        public int id { get; set; }
        [DisplayName("Irangos Pavadinimas")]
        [MaxLength(20)]
        [Required]
        public string pavadinimas { get; set; }
        [DisplayName("KinoStudija")]
        [Required]
        public int fk_marke { get; set; }

        //Markiu sąrašas pasirinkimui
        public IList<SelectListItem> MarkesList { get; set; }
    }
}