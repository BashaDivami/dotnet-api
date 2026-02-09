using CapStoneProject.Data;
using CapStoneProject.DTOs;
using CapStoneProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapStoneProject.Services
{

    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EnrollmentResponseDto> EnrollPolicy(int userId, int policyId)
        {
            // Validate policy exists and is active
            var policy = await _context.Policies.FindAsync(policyId);
            if (policy == null)
                throw new InvalidOperationException("Policy not found");

            if (!policy.IsActive)
                throw new InvalidOperationException("Policy is not active");

            // Check if user already enrolled
            var existingEnrollment = await _context.PolicyEnrollments
                .FirstOrDefaultAsync(e => e.UserId == userId && e.PolicyId == policyId);

            if (existingEnrollment != null)
                throw new InvalidOperationException("You are already enrolled in this policy");

            var enrollment = new PolicyEnrollment
            {
                UserId = userId,
                PolicyId = policyId,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow
            };

            _context.PolicyEnrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return new EnrollmentResponseDto
            {
                Id = enrollment.Id,
                UserId = enrollment.UserId,
                PolicyId = enrollment.PolicyId,
                PolicyName = policy.Name,
                Status = enrollment.Status,
                RequestedAt = enrollment.RequestedAt,
                ApprovedAt = enrollment.ApprovedAt
            };
        }

        public async Task<IEnumerable<EnrollmentResponseDto>> GetMyEnrollments(int userId)
        {
            var enrollments = await _context.PolicyEnrollments
                .Include(e => e.Policy)
                .Where(e => e.UserId == userId)
                .Select(e => new EnrollmentResponseDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    PolicyId = e.PolicyId,
                    PolicyName = e.Policy.Name,
                    Status = e.Status,
                    RequestedAt = e.RequestedAt,
                    ApprovedAt = e.ApprovedAt
                })
                .ToListAsync();

            return enrollments;
        }

        public async Task<IEnumerable<EnrollmentResponseDto>> GetAllEnrollments(string? status = null)
        {
            var query = _context.PolicyEnrollments.Include(e => e.Policy).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            var enrollments = await query
                .Select(e => new EnrollmentResponseDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    PolicyId = e.PolicyId,
                    PolicyName = e.Policy.Name,
                    Status = e.Status,
                    RequestedAt = e.RequestedAt,
                    ApprovedAt = e.ApprovedAt
                })
                .ToListAsync();

            return enrollments;
        }

        public async Task<EnrollmentResponseDto> ApproveEnrollment(int enrollmentId, int adminId)
        {
            var enrollment = await _context.PolicyEnrollments
                .Include(e => e.Policy)
                .FirstOrDefaultAsync(e => e.Id == enrollmentId);

            if (enrollment == null)
                throw new InvalidOperationException("Enrollment not found");

            if (enrollment.Status != "Pending")
                throw new InvalidOperationException("Only pending enrollments can be approved");

            enrollment.Status = "Approved";
            enrollment.ApprovedAt = DateTime.UtcNow;
            enrollment.ApprovedBy = adminId;

            await _context.SaveChangesAsync();

            return new EnrollmentResponseDto
            {
                Id = enrollment.Id,
                UserId = enrollment.UserId,
                PolicyId = enrollment.PolicyId,
                PolicyName = enrollment.Policy.Name,
                Status = enrollment.Status,
                RequestedAt = enrollment.RequestedAt,
                ApprovedAt = enrollment.ApprovedAt
            };
        }

        public async Task<EnrollmentResponseDto> RejectEnrollment(int enrollmentId, int adminId, string? rejectionReason)
        {
            var enrollment = await _context.PolicyEnrollments
                .Include(e => e.Policy)
                .FirstOrDefaultAsync(e => e.Id == enrollmentId);

            if (enrollment == null)
                throw new InvalidOperationException("Enrollment not found");

            if (enrollment.Status != "Pending")
                throw new InvalidOperationException("Only pending enrollments can be rejected");

            enrollment.Status = "Rejected";
            enrollment.ApprovedAt = DateTime.UtcNow;
            enrollment.ApprovedBy = adminId;
            enrollment.RejectionReason = rejectionReason;

            await _context.SaveChangesAsync();

            return new EnrollmentResponseDto
            {
                Id = enrollment.Id,
                UserId = enrollment.UserId,
                PolicyId = enrollment.PolicyId,
                PolicyName = enrollment.Policy.Name,
                Status = enrollment.Status,
                RequestedAt = enrollment.RequestedAt,
                ApprovedAt = enrollment.ApprovedAt
            };
        }
    }
}
