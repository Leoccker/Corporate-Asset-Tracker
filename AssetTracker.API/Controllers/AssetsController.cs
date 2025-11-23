using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AssetTracker.Application.Interfaces;
using AssetTracker.Domain;

namespace AssetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        // Injeção de Dependência: O Controller pede o Service
        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpPost("create")] // POST: api/Assets/create
        public async Task<IActionResult> Create([FromBody] CreateAssetRequest request)
        {
            try 
            {
                var asset = await _assetService.RegisterAssetAsync(request.Name, request.Serial, request.Tag);
                
                // Retorna 201 Created
                return CreatedAtAction(nameof(GetAll), new { id = asset.Id }, asset);
            }
            catch (InvalidOperationException ex)
            {
                // Retorna 400 Bad Request se for duplicado (nossa regra de negócio)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("all")] // GET: api/Assets/all
        public async Task<IActionResult> GetAll()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            return Ok(assets);
        }
    }

    public record CreateAssetRequest(string Name, string Serial, string Tag);
}
