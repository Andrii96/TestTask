using Insurance.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Api.Model.Dtos;
using Insurance.Domain.Contexts;
using Insurance.Domain.Entities.SystemB;

namespace Insurance.Domain.Services
{
    public class SystemBService : IAction
    {
        #region Properties
        public IRepository<Insurer> InsurerRepositoty => new Repository<SystemBDbContext, Insurer>();
        public IRepository<InsurancePolicy> InsurancePolicyRepositoty => new Repository<SystemBDbContext, InsurancePolicy>();
        public IRepository<Agent> AgentRepositoty => new Repository<SystemBDbContext, Agent>();
              
        #endregion

        #region Implementation

        public IEnumerable<PolicyDto> GetActualPolicies()
        {
            var actualPolicies = InsurancePolicyRepositoty.Get(p => p.DateFrom < DateTime.Now && p.DateTill > DateTime.Now);
            return actualPolicies.Select(p => MapInsurancePolicy(p)).ToList();
        }

        public IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber)
        {
            return null;
        }

        public InsurerDto GetInsurerByPhone(string phone)
        {
            return MapInsurer(InsurerRepositoty.Get(i => i.PhoneNumber == phone).FirstOrDefault());
        }

        public IEnumerable<long> GetPoliciesNumbersByAgent(string agentName)
        {
            var policies = InsurancePolicyRepositoty.Get(p => p.Agent.Name == agentName);
            return policies.Select(p => p.Number).ToList();
        }

        public PolicyDto GetPolicyByInsurerPhone(string phone)
        {
            return MapInsurancePolicy(InsurancePolicyRepositoty.Get(p => p.Insurer.PhoneNumber == phone).FirstOrDefault());
        }

        public PolicyDto GetPolicyByNumber(long number)
        {
            return MapInsurancePolicy(InsurancePolicyRepositoty.Get(p => p.Number == number).FirstOrDefault());
        }

        #endregion

        #region Helpers

        private static InsurerDto MapInsurer(Insurer insurer)
        {
            if (insurer == null)
            {
                return null;
            }

            return new InsurerDto
            {
                Id = insurer.Id,
                FirstName = insurer.FirstName,
                LastName = insurer.LastName,
                Phone = insurer.PhoneNumber
            };
        }

        private static AgentDto MapAgent(Agent agent)
        {
            if (agent == null)
            {
                return null;
            }

            return new AgentDto
            {
                Id = agent.Id,
                Name = agent.Name
            };
        }

        private static PolicyDto MapInsurancePolicy(InsurancePolicy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return new PolicyDto
            {
                Id = policy.Id,
                DateFrom = policy.DateFrom,
                DateTill = policy.DateTill,
                Number = policy.Number,
                Agent = MapAgent(policy.Agent),
                Insurer = MapInsurer(policy.Insurer),
                IsActive = policy.DateFrom < DateTime.Now && policy.DateTill > DateTime.Now
            };
        }

        #endregion

    }
}
