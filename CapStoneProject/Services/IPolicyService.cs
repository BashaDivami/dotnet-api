using CapStoneProject.Entities;
using CapStoneProject.DTOs;

namespace CapStoneProject.Services
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetPolicies();
        Task<Policy?> GetPolicyById(int id);
        Task<IEnumerable<Policy>> SearchPoliciesByAmountRange(decimal minAmount, decimal maxAmount);
        Task<IEnumerable<Policy>> GetPoliciesByStatus(bool isActive);
        Task<Policy> CreatePolicy(CreatePolicyDto policyDto);
        Task<Policy> UpdatePolicy(Policy policy);
        Task<bool> DeletePolicy(int id);
    }
}
