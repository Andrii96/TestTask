using Insurance.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Api.Model.Dtos;
using Insurance.Domain.Entities.SystemA;
using Insurance.Domain.Contexts;
using Insurance.Domain.Mapper;
using Insurance.Api.Model;

namespace Insurance.Domain.Services
{
    public class SystemAService : IAction
    {
        #region Properties
            public IRepository<Insurer> InsurerRepositoty => new Repository<SystemADbContext, Insurer>();
            public IRepository<InsurancePolicy> InsurancePolicyRepositoty => new Repository<SystemADbContext, InsurancePolicy>();
            public IRepository<Beneficiary> BeneficiaryRepositoty => new Repository<SystemADbContext, Beneficiary>();
            public Systems Name => Systems.SystemA;
        #endregion

        #region interface implementation

        public IEnumerable<PolicyDto> GetActualPolicies()
        {
            var actualPolicies = InsurancePolicyRepositoty.Get(p => p.IsActive);
            return actualPolicies.Select(p => p.MapInsurancePolicy()).ToList();
        }

        public IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber)
        {
            var policy = InsurancePolicyRepositoty.Get(p => p.Number == policyNumber).FirstOrDefault();
            if(policy == null)
            {
                return null;
            }
            var beneficiares = BeneficiaryRepositoty.Get(b => b.InsurancePolicyId == policy.Id);

            return beneficiares.Select(b => b.MapBeneficiary()).ToList();
        }

        public InsurerDto GetInsurerByPhone(string phone)
        {
            return InsurerRepositoty.Get(i => i.PhoneNumber == phone).FirstOrDefault().MapInsurer();
        }

        public IEnumerable<long> GetPoliciesNumbersByAgent(string agentName)
        {
            var policies = InsurancePolicyRepositoty.Get(p => p.AgentName == agentName).ToList();
             return policies.Select(p => p.Number).ToList();
        }

        public PolicyDto GetPolicyByInsurerPhone(string phone)
        {
            return  InsurancePolicyRepositoty.Get(p => p.Insurer.PhoneNumber == phone).FirstOrDefault().MapInsurancePolicy();
        }

        public PolicyDto GetPolicyByNumber(long number)
        {
            return InsurancePolicyRepositoty.Get(p => p.Number == number).FirstOrDefault().MapInsurancePolicy();
        }

        #endregion

    }
}
