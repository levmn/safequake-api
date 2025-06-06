using SafeQuake.Domain.Entities;

namespace SafeQuake.Service.Interfaces
{
    public interface IEarthquakeService
    {
        /// <summary>
        /// Obtém terremotos recentes do USGS e os converte para o formato do SafeQuake
        /// </summary>
        /// <param name="limiteDeDias">Número de dias para buscar (padrão: 30)</param>
        /// <param name="magnitudeMinima">Magnitude mínima dos terremotos (padrão: 2.5)</param>
        /// <returns>Lista de terremotos convertidos para o formato SafeQuake</returns>
        Task<IEnumerable<EarthquakeEntity>> ObterTerremotosRecentesAsync(int limiteDeDias = 30, double magnitudeMinima = 2.5);

        /// <summary>
        /// Traduz a localização de um terremoto para português
        /// </summary>
        /// <param name="localizacaoOriginal">Localização original em inglês</param>
        /// <returns>Localização traduzida para português</returns>
        Task<string> TraduzirLocalizacaoAsync(string localizacaoOriginal);
    }
} 