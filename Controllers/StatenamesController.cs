using Enwage_API.DTOs;
using Enwage_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enwage_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatenamesController : ControllerBase
    {
        private readonly IStatenameService _statenameService;

        public StatenamesController(IStatenameService statenameService)
        {
            _statenameService = statenameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatenameDto>>> GetAllStatenames()
        {
            var statenames = await _statenameService.GetAllStatenamesAsync();
            return Ok(statenames);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatenameDto>> GetStatename(Guid id)
        {
            var statename = await _statenameService.GetStatenameByIdAsync(id);
            if (statename == null)
            {
                return NotFound();
            }
            return Ok(statename);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatename(StatenameDto statenameDto)
        {
            try
            {
                var createdStatename = await _statenameService.CreateStatenameAsync(statenameDto);
                return CreatedAtAction(nameof(GetStatename), new { id = createdStatename.Id }, createdStatename);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatename(Guid id, StatenameDto statenameDto)
        {
            try
            {
                var updatedStatename = await _statenameService.UpdateStatenameAsync(id, statenameDto);
                return Ok(updatedStatename);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatename(Guid id)
        {
            try
            {
                await _statenameService.DeleteStatenameAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}