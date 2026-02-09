using CapStoneProject.Entities;
namespace CapStoneProject.Services
{
       public interface IEnrollmentService
    {
        Task<EnrollmentResponseDto> EnrollPolicy(int userId, int policyId);
        Task<IEnumerable<EnrollmentResponseDto>> GetMyEnrollments(int userId);
        Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollments(string? status = null);
        Task<EnrollmentResponseDto> ApproveEnrollment(int enrollmentId, int adminId);
        Task<EnrollmentResponseDto> RejectEnrollment(int enrollmentId, int adminId, string? rejectionReason);
    }
}