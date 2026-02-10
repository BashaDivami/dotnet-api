using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.DTOs;
using CapStoneProject.Services;

namespace CapStoneProject.Controllers
{
    [ApiController]
    [Route("api/admin/policies")]
    [Authorize(Roles = "Admin")]
    public class AdminPolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public AdminPolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyDto policyDto)
        {
            var policy = await _policyService.CreatePolicy(policyDto);
            return CreatedAtAction(nameof(GetPolicy), new { id = policy.Id }, policy);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy(int id)
        {
            var policy = await _policyService.GetPolicyById(id);
            if (policy == null)
                return NotFound(new { message = "Policy not found" });

            return Ok(policy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(int id, [FromBody] CreatePolicyDto policyDto)
        {
            var policy = await _policyService.GetPolicyById(id);
            if (policy == null)
                return NotFound(new { message = "Policy not found" });

            policy.Name = policyDto.Name;
            policy.PremiumAmount = policyDto.PremiumAmount;
            policy.Description = policyDto.Description;
            policy.IsActive = policyDto.IsActive;
            policy.UpdatedAt = DateTime.UtcNow;

            await _policyService.UpdatePolicy(policy);

            return Ok(policy);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdatePolicyStatus(int id, [FromBody] PolicyStatusDto statusDto)
        {
            var policy = await _policyService.GetPolicyById(id);
            if (policy == null)
                return NotFound(new { message = "Policy not found" });

            policy.IsActive = statusDto.IsActive;
            policy.UpdatedAt = DateTime.UtcNow;

            await _policyService.UpdatePolicy(policy);

            return Ok(policy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var deleted = await _policyService.DeletePolicy(id);
            if (!deleted)
                return NotFound(new { message = "Policy not found" });

            return Ok(new { message = "Policy deleted successfully" });
        }
    }

    public class PolicyStatusDto
    {
        public bool IsActive { get; set; }
    }
}
