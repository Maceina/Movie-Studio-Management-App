using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class VeluojanciosSutartysViewModel
    {
        [DisplayName("Sutartis")]
        public int nr { get; set; }

        public DateTime sutartiesData { get; set; }
        [DisplayName("Klientas")]
        public string klientas { get; set; }
        [DisplayName("Planuota grąžinti")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime planuojamaGrData { get; set; }
        [DisplayName("Grąžintina")]
        public string faktineGrData { get; set; }
    }
}