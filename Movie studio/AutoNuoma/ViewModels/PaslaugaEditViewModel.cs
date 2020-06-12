using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using AutoNuoma.ViewModels;

namespace AutoNuoma.ViewModels
{
    public class PaslaugaEditViewModel
    {
        //Paslauga
        public Paslauga paslauga { get; set; }
        //Paslaugos kainų sąrašas
        public List<PaslaugosKainaViewModel> paslaugosKainos { get; set; }
    }
}