using CapStoneProject.Entities;

namespace CapStoneProject.Repositories
{
    public interface IPolicyRepository
    {
        Task<IEnumerable<Policy>> GetPolicies(); 
        Task<Policy?> GetPolicyById(int id);
        Task<IEnumerable<Policy>> SearchPoliciesByAmountRange(decimal minAmount, decimal maxAmount);
        Task<IEnumerable<Policy>> GetPoliciesByStatus(bool isActive);
        Task<Policy> CreatePolicy(Policy policy);
        Task<Policy> UpdatePolicy(Policy policy);
    }
}
