using CardAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _cardService.GetCardsAsync());
        }

        [HttpGet("FilterCards")]
        public async Task<IActionResult> GetFilterCardsAsync(string? name, string? color, string? type)
        {
            return Ok(await _cardService.GetFilterCardsAsync(name, color, type));
        }
    }
}
