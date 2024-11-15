using System.Collections.Generic;

namespace VibeApiTestV11ByRvw.Models
{
    public class PlacemarkFilter
    {
        public List<string> Cliente { get; set; }
        public List<string> Situacao { get; set; }
        public List<string> Bairro { get; set; }
        public string Referencia { get; set; }
        public string RuaCruzamento { get; set; }
    }
}
