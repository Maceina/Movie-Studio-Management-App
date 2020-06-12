using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class PaslauguAtaskaitaViewModel
    {
        [DisplayName("Kino Studjios ID")]
        public int id { get; set; }
        [DisplayName("Kino Studijos Pavadinimas")]
        public string pavadinimas { get; set; }
        [DisplayName("Darbo honorarai(eur.)")]
        public int kiekis { get; set; }
        [DisplayName("Aktorystes honorarai(eur.)")]
        public decimal suma { get; set; }
    }
}