using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VibeApiTestV11ByRvw.Models;

namespace VibeApiTestV11ByRvw.Services.Interfaces
{
    public interface IFilterValidator
    {
        ValidationResult Validate(PlacemarkFilter filter, AvailableFilters availableFilters);
    }
}