using Insurance.Api.Model.Dtos;
using Insurance.Domain.Abstract;
using Insurance.Domain.Contexts;
using Insurance.Domain.Entities.SystemC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Domain.Mapper;
using Insurance.Api.Model;

namespace Insurance.Domain.Services
{
    public class SystemCService:IAction
    {
        #region Properties
        public IRepository<Insurer> InsurerRepositoty => new Repository<SystemCDbContext, Insurer>();
        public IRepository<InsurancePolicy> InsurancePolicyRepositoty => new Repository<SystemCDbContext, InsurancePolicy>();
        public IRepository<Agent> AgentRepositoty => new Repository<SystemCDbContext, Agent>();
        public IRepository<Beneficiary> BeneficiaryRepository => new Repository<SystemCDbContext, Beneficiary>();
        public Systems Name => Systems.SystemC;
        #endregion

        #region Implementation

        public IEnumerable<PolicyDto> GetActualPolicies()
        {
            var actualPolicies = InsurancePolicyRepositoty.Get(p => p.DateFrom < DateTime.Now && p.DateTill > DateTime.Now);
            return actualPolicies.Select(p => p.MapInsurancePolicy()).ToList();
        }

        public IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber)
        {
            var policy = InsurancePolicyRepositoty.Get(p => p.Number == policyNumber).FirstOrDefault();
            if (policy == null)
            {
                return null;
            }
            var beneficiares = BeneficiaryRepository.Get(b => b.InsurancePolicyId == policy.Id);
            return beneficiares.Select(b => b.MapBeneficiary()).ToList();
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
            return InsurancePolicyRepositoty.Get(p => p.Number == number).FirstOrDefault().MapInsurancePolicy();
        }

        #endregion

    }
}
