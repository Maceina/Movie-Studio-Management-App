using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Modelis2
    {
        [DisplayName("IrangosID")]
        public int id { get; set; }
        [DisplayName("IPavadinimas")]
        public string pavadinimas { get; set; }


        [DisplayName("IPagaminimoMetai")]
        public string pagaminimoMetai { get; set; }

        [DisplayName("IBusena")]
        public string busena { get; set; }

        [DisplayName("IMOdelis")]
        public string modelis { get; set; }

        //Markė
        [DisplayName("KinosStudijaID")]
        public virtual Marke marke { get; set; }

    }
}