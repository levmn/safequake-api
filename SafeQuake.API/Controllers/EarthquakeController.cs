using Microsoft.AspNetCore.Mvc;
using SafeQuake.Application.Interfaces.Earthquake;
using SafeQuake.Domain.Entities;
using SafeQuake.Service.Interfaces;

namespace SafeQuake.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EarthquakeController : ControllerBase
    {
        private readonly IGetEarthquakeUseCase _getEarthquakeUseCase;
        private readonly ICreateEarthquakeUseCase _createEarthquakeUseCase;
        private readonly IUpdateEarthquakeUseCase _updateEarthquakeUseCase;
        private readonly IEarthquakeService _earthquakeService;

        public EarthquakeController(
            IGetEarthquakeUseCase getEarthquakeUseCase,
            ICreateEarthquakeUseCase createEarthquakeUseCase,
            IUpdateEarthquakeUseCase updateEarthquakeUseCase,
            IEarthquakeService earthquakeService)
        {
            _getEarthquakeUseCase = getEarthquakeUseCase;
            _createEarthquakeUseCase = createEarthquakeUseCase;
            _updateEarthquakeUseCase = updateEarthquakeUseCase;
            _earthquakeService = earthquakeService;
        }

        /// <summary>
        /// Obtém um evento de terremoto pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EarthquakeEntity>> GetById(int id)
        {
            var earthquake = await _getEarthquakeUseCase.ExecuteAsync(id);
            if (earthquake == null)
                return NotFound();

            return Ok(earthquake);
        }

        /// <summary>
        /// Obtém terremotos recentes da API pública
        /// </summary>
        [HttpGet("recentes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EarthquakeEntity>>> GetRecentes(
            [FromQuery] int limiteDeDias = 30,
            [FromQuery] double magnitudeMinima = 2.5)
        {
            var terremotos = await _earthquakeService.ObterTerremotosRecentesAsync(limiteDeDias, magnitudeMinima);
            return Ok(terremotos);
        }

        /// <summary>
        /// Obtém alertas de terremotos de alta magnitude
        /// </summary>
        [HttpGet("alertas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EarthquakeEntity>>> GetAlertas()
        {
            // Consideramos terremotos com magnitude >= 6.0 como de alto risco
            var alertas = await _earthquakeService.ObterTerremotosRecentesAsync(
                limiteDeDias: 7,  // Últimos 7 dias
                magnitudeMinima: 6.0  // Magnitude considerada perigosa
            );

            return Ok(alertas);
        }

        /// <summary>
        /// Registra um novo evento de terremoto
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EarthquakeEntity earthquake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _createEarthquakeUseCase.ExecuteAsync(earthquake);
            return CreatedAtAction(nameof(GetById), new { id = earthquake.Id }, earthquake);
        }

        /// <summary>
        /// Atualiza um evento de terremoto
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EarthquakeEntity earthquake)
        {
            if (id != earthquake.Id)
                return BadRequest();

            var existing = await _getEarthquakeUseCase.ExecuteAsync(id);
            if (existing == null)
                return NotFound();

            await _updateEarthquakeUseCase.ExecuteAsync(earthquake);
            return NoContent();
        }
    }
} 