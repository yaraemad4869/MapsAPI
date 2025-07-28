using Maps.Data;
using Maps.src.Maps.Core.Interfaces;
using Maps.src.Maps.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maps.src.Maps.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public BranchesController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Branches.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Branch? branch = await _uow.Branches.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound($"There is no branch with ID {id}");
            }
            return Ok(branch);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Branch branch)
        {
            if (branch == null)
            {
                return BadRequest("Branch cannot be null");
            }
            if (string.IsNullOrEmpty(branch.Name))
            {
                return BadRequest("Branch name is required");
            }
            if (string.IsNullOrEmpty(branch.Address))
            {
                return BadRequest("Branch address is required");
            }
            if (string.IsNullOrEmpty(branch.PhoneNumber))
            {
                return BadRequest("Branch phone number is required");
            }
            var createdBranch = await _uow.Branches.AddAsync(branch);
            return CreatedAtAction(nameof(Get), new { id = createdBranch.Id }, createdBranch);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Branch branch)
        {
            if (branch == null || id != branch.Id)
            {
                return BadRequest("Branch cannot be null and ID must match");
            }
            if (string.IsNullOrEmpty(branch.Name))
            {
                return BadRequest("Branch name is required");
            }
            if (string.IsNullOrEmpty(branch.Address))
            {
                return BadRequest("Branch address is required");
            }
            if (string.IsNullOrEmpty(branch.PhoneNumber))
            {
                return BadRequest("Branch phone number is required");
            }
            var updatedBranch = await _uow.Branches.UpdateAsync(branch);
            if (updatedBranch == null)
            {
                return NotFound($"There is no branch with ID {id}");
            }
            return Ok(updatedBranch);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _uow.Branches.DeleteAsync(id);
            if (branch == null)
            {
                return NotFound($"There is no branch with ID {id}");
            }
            return NoContent();
        }
        [HttpGet("/api/branches/nearby")]
        public async Task<IActionResult> GetNearBranch(double latitude, double longitude, double radiusKm)
        {

            return Ok(await _uow.Branches.GetNearBranch(latitude, longitude, radiusKm));
        }
    }
}
