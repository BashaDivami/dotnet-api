using CapStoneProject.Entities;
using CapStoneProject.Repositories;
using CapStoneProject.DTOs;

namespace CapStoneProject.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository policyRepository;

        public PolicyService(IPolicyRepository repository)
        {
            policyRepository = repository;
        }

        public Task<IEnumerable<Policy>> GetPolicies()
        {
            return policyRepository.GetPolicies();
        }

        public async Task<Policy?> GetPolicyById(int id)
        {
            // var policy = policies.FirstOrDefault(p => p.Id == id);
            // return Task.FromResult(policy);
            return await policyRepository.GetPolicyById(id);
        }

        public Task<IEnumerable<Policy>> SearchPoliciesByAmountRange(decimal minAmount, decimal maxAmount)
        {
            return policyRepository.SearchPoliciesByAmountRange(minAmount, maxAmount);
        }

        public Task<IEnumerable<Policy>> GetPoliciesByStatus(bool isActive)
        {
            return policyRepository.GetPoliciesByStatus(isActive);
        }

        public async Task<Policy> CreatePolicy(CreatePolicyDto policyDto)
        {
            var policy = new Policy
            {
                Name = policyDto.Name,
                PremiumAmount = policyDto.PremiumAmount,
                Description = policyDto.Description,
                IsActive = policyDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            return await policyRepository.CreatePolicy(policy);
        }

        public async Task<Policy> UpdatePolicy(Policy policy)
        {
            return await policyRepository.UpdatePolicy(policy);
        }
    }
}