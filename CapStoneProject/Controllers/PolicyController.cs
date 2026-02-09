using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CapStoneProject.Entities;
using CapStoneProject.Services;
using CapStoneProject.DTOs;
using CapStoneProject.Filters;

namespace CapStoneProject.Controllers
{  
    [ApiController]
    [ServiceFilter(typeof(GlobalResponseFilter))]
    [ServiceFilter(typeof(GlobalActionFilter))]
    [Route("api/policies")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService policyService;

        public PolicyController(IPolicyService service)
        {
            policyService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPolicies()
        {
            var policies = await policyService.GetPolicies();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy(int id)
        {
            var greet = $"Hi, How are you doing with id {id}?";
            return Ok(greet);
            // var policy = await policyService.GetPolicyById(id);
            // if (policy == null)
            //     return NotFound();       
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPolicies([FromQuery] decimal minAmount, [FromQuery]  decimal maxAmount)
        {

            var policies = await policyService.SearchPoliciesByAmountRange(minAmount, maxAmount);
            return Ok(policies);         
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetPoliciesByStatus([FromQuery] bool isActive)
        {
         
            var policies = await policyService.GetPoliciesByStatus(isActive);
            return Ok(policies);         
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyDto policy)
        {
            var createdpolicy = await policyService.CreatePolicy(policy);
            return CreatedAtAction(nameof(GetPolicy), new { id = createdpolicy.Id }, createdpolicy);
        }
    }
}