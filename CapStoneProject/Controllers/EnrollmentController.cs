using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapStoneProject.DTOs;
using CapStoneProject.Services;
using System.Security.Claims;
using CapStoneProject.Filters;
namespace CapStoneProject.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(GlobalResponseFilter))]
    [ServiceFilter(typeof(GlobalActionFilter))]
    [Route("api")]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // User APIs
        [HttpPost("policies/{policyId}/enroll")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EnrollPolicy(int policyId)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var enrollment = await _enrollmentService.EnrollPolicy(userId, policyId);
                return Ok(enrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("my/enrollments")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyEnrollments()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var enrollments = await _enrollmentService.GetMyEnrollments(userId);
            
            if (!enrollments.Any())
            {
                return Ok(new { message = "No enrollments found" });
            }
            
            return Ok(enrollments);
        }

        // Admin APIs
        [HttpGet("admin/enrollments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEnrollments([FromQuery] string? status = null)
        {
            var enrollments = await _enrollmentService.GetAllEnrollments(status);
            
            if (!enrollments.Any())
            {
                return Ok(new { message = "No enrollments found" });
            }
            
            return Ok(enrollments);
        }

        [HttpPost("admin/enrollments/{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveEnrollment(int id)
        {
            try
            {
                var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var enrollment = await _enrollmentService.ApproveEnrollment(id, adminId);
                return Ok(enrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("admin/enrollments/{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectEnrollment(int id, [FromBody] ApprovalDto? approvalDto = null)
        {
            try
            {
                var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var enrollment = await _enrollmentService.RejectEnrollment(id, adminId, approvalDto?.RejectionReason);
                return Ok(enrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
