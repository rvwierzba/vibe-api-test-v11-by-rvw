using System.Linq;
using VibeApiTestV11ByRvw.Models;
using VibeApiTestV11ByRvw.Services.Interfaces;


namespace VibeApiTestV11ByRvw.Services
{
    public class FilterValidator : IFilterValidator
    {
        public ValidationResult Validate(PlacemarkFilter filter, AvailableFilters availableFilters)
        {
            if (filter.Cliente != null && filter.Cliente.Except(availableFilters.Cliente).Any())
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Cliente contém valores inválidos."
                };
            }

            if (filter.Situacao != null && filter.Situacao.Except(availableFilters.Situacao).Any())
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Situação contém valores inválidos."
                };
            }

            if (filter.Bairro != null && filter.Bairro.Except(availableFilters.Bairro).Any())
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Bairro contém valores inválidos."
                };
            }

            if (!string.IsNullOrEmpty(filter.Referencia) && filter.Referencia.Length < 3)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Referência deve ter pelo menos 3 caracteres."
                };
            }

            if (!string.IsNullOrEmpty(filter.RuaCruzamento) && filter.RuaCruzamento.Length < 3)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Rua/Cruzamento deve ter pelo menos 3 caracteres."
                };
            }

            return new ValidationResult { IsValid = true };
        }
    }
}
