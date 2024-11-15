using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VibeApiTestV11ByRvw.Models
{
    public class AvailableFilters
    {
       public List<string> Cliente { get; set; }
       public List<string> Situacao { get; set; }
       public List<string> Bairro { get; set; }
    }
}