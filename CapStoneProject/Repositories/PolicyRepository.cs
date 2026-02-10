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
            // Check if entity is already being tracked
            var trackedEntity = dbContext.ChangeTracker.Entries<Policy>()
                .FirstOrDefault(e => e.Entity.Id == policy.Id);
            if (trackedEntity == null)
            {
                // Entity not tracked, attach and update
                dbContext.Policies.Update(policy);
            }
            // If already tracked, changes are already being tracked by EF Core
            await dbContext.SaveChangesAsync();
            return policy;
        }

        public async Task<bool> DeletePolicy(int id)
        {
            var policy = await dbContext.Policies.FirstOrDefaultAsync(p => p.Id == id);
            if (policy == null)
                return false;

            dbContext.Policies.Remove(policy);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
