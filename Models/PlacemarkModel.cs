using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VibeApiTestV11ByRvw.Models
{
    public class PlacemarkModel
    {
        public string Cliente { get; set; }
        public string Situacao { get; set; }
        public string Bairro { get; set; }
        public string Referencia { get; set; }
        public string RuaCruzamento { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}