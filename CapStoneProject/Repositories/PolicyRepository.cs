using CapStoneProject.Entities;
using CapStoneProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStoneProject.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly AppDbContext dbContext;

        public PolicyRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Policy>> GetPolicies()
        {
            return await dbContext.Policies.ToListAsync();
        }

        public async Task<Policy?> GetPolicyById(int id)
        {
            return await dbContext.Policies.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Policy>> SearchPoliciesByAmountRange(decimal minAmount, decimal maxAmount)
        {
            return await dbContext.Policies
                .Where(p => p.PremiumAmount >= minAmount && p.PremiumAmount <= maxAmount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Policy>> GetPoliciesByStatus(bool isActive)
        {
            return await dbContext.Policies
                .Where(p => p.IsActive == isActive)
                .ToListAsync();
        }

        public async Task<Policy> CreatePolicy(Policy policy)
        {
            dbContext.Policies.Add(policy);
            await dbContext.SaveChangesAsync();
            return policy;
        }

        public async Task<Policy> UpdatePolicy(Policy policy)
        {
            dbContext.Policies.Update(policy);
            await dbContext.SaveChangesAsync();
            return policy;
        }
    }
}
