using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class ModelisViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Irangos Pavadinimas")]
        public string pavadinimas { get; set; }
        [DisplayName("FK_KinostudijosID")]
        public string marke { get; set; }

    }
}