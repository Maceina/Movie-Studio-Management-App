using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Automobilis
    {
        public int id { get; set; }
        public string valstybinisNr { get; set; }
        public DateTime pagaminimoData { get; set; }
        public int rida { get; set; }
        public bool radijas { get; set; }
        public bool kondicionierius { get; set; }
        public int vietuSkaicius { get; set; }
        public DateTime registravimoData { get; set; }
        public double verte { get; set; }
        public PavaruDeze pavaruDeze { get; set; }
        public DegaluTipas degaluTipas { get; set; }
        public KebuloTipas kebuloTipas { get; set; }
        public LagaminoTipas bagazoDydis { get; set; }
        public AutoBusena busena { get; set; }
        public int MyProperty { get; set; }
        public AutomobilioGrupe automobilioGrupe { get; set; }
        public Modelis modelis { get; set; }

    }
}