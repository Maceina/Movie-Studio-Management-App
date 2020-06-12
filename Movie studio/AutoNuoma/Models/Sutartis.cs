using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Sutartis
    {
        [DisplayName("Nr.")]
        public int nr { get; set; }
        [DisplayName("Sutarties data")]

        public DateTime sutartiesData { get; set; }
        public DateTime nuomosDataLaikas { get; set; }
        public DateTime planuojamaGrDataLaikas { get; set; }
        public DateTime faktineGrDataLaikas { get; set; }
        public int pradineRida { get; set; }
        public int galineRida { get; set; }
        public decimal kaina { get; set; }
        public int degaluKiekisPaimant { get; set; }
        public int degaluKiekisGrazinant { get; set; }
        public int busena { get; set; }
        public int fk_klientas { get; set; }
        public int fk_darbuotojas { get; set; }
        public int fk_automobilis { get; set; }
        public int fk_grazinimoVieta { get; set; }
        public int fk_paemimoVieta { get; set; }

    }
}