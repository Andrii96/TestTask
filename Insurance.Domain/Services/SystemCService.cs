using Insurance.Api.Model.Dtos;
using Insurance.Domain.Abstract;
using Insurance.Domain.Contexts;
using Insurance.Domain.Entities.SystemC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Services
{
    public class SystemCService:IAction
    {
        #region Properties
        public IRepository<Insurer> InsurerRepositoty => new Repository<SystemCDbContext, Insurer>();
        public IRepository<InsurancePolicy> InsurancePolicyRepositoty => new Repository<SystemCDbContext, InsurancePolicy>();
        public IRepository<Agent> AgentRepositoty => new Repository<SystemCDbContext, Agent>();
        public IRepository<Beneficiary> BeneficiaryRepository => new Repository<SystemCDbContext, Beneficiary>();

        #endregion

        #region Implementation

        public IEnumerable<PolicyDto> GetActualPolicies()
        {
            var actualPolicies = InsurancePolicyRepositoty.Get(p => p.DateFrom < DateTime.Now && p.DateTill > DateTime.Now);
            return actualPolicies.Select(p => MapInsurancePolicy(p)).ToList();
        }

        public IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber)
        {
            var policy = InsurancePolicyRepositoty.Get(p => p.Number == policyNumber).FirstOrDefault();
            if (policy == null)
            {
                return null;
            }
            var beneficiares = BeneficiaryRepository.Get(b => b.InsurancePolicyId == policy.Id);
            return beneficiares.Select(b => MapBeneficiary(b)).ToList();
        }

        public InsurerDto GetInsurerByPhone(string phone)
        {
            return null;
        }

        public IEnumerable<long> GetPoliciesNumbersByAgent(string agentName)
        {
            var policies = InsurancePolicyRepositoty.Get(p => p.Agent.Name == agentName);
            return policies.Select(p => p.Number).ToList();
        }

        public PolicyDto GetPolicyByInsurerPhone(string phone)
        {
            return null;
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
                LastName = insurer.LastName
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

        private static BeneficiaryDto MapBeneficiary(Beneficiary beneficiary)
        {
            if(beneficiary == null)
            {
                return null;
            }
            return new BeneficiaryDto
            {
                Id = beneficiary.Id,
                Name = beneficiary.Name
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
                Beneficiary = policy.Beneficiaries.Select(p=>MapBeneficiary(p)),
                IsActive = policy.DateFrom < DateTime.Now && policy.DateTill > DateTime.Now
            };
        }

        #endregion
    }
}
