using System.Collections.Generic;
using System.Threading.Tasks;
using VibeApiTestV11ByRvw.Models;

namespace VibeApiTestV11ByRvw.Services.Interfaces
{
    public interface IKmlService
    {
        /// <summary>
        /// Obtém todos os placemarks do arquivo KML.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="PlacemarkModel"/>.</returns>
        Task<List<PlacemarkModel>> GetPlacemarksAsync();

        /// <summary>
        /// Obtém os placemarks filtrados com base nos critérios fornecidos.
        /// </summary>
        /// <param name="filter">Os critérios de filtro.</param>
        /// <returns>Uma lista filtrada de objetos <see cref="PlacemarkModel"/>.</returns>
        Task<List<PlacemarkModel>> GetFilteredPlacemarksAsync(PlacemarkFilter filter);

        /// <summary>
        /// Exporta os placemarks filtrados para um arquivo KML.
        /// </summary>
        /// <param name="filter">Os critérios de filtro.</param>
        /// <returns>Um array de bytes representando o arquivo KML.</returns>
        Task<byte[]> ExportFilteredKmlAsync(PlacemarkFilter filter);

        /// <summary>
        /// Obtém os valores disponíveis para filtros.
        /// </summary>
        /// <returns>Um objeto <see cref="AvailableFilters"/> contendo os valores disponíveis.</returns>
        Task<AvailableFilters> GetAvailableFiltersAsync();
    }
}
