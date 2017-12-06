using Insurance.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Api.Model.Dtos;
using Insurance.Domain.Contexts;
using Insurance.Domain.Entities.SystemB;
using Insurance.Domain.Mapper;

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
            return actualPolicies.Select(p => p.MapInsurancePolicy()).ToList();
        }

        public IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber)
        {
            return null;
        }

        public InsurerDto GetInsurerByPhone(string phone)
        {
            return InsurerRepositoty.Get(i => i.PhoneNumber == phone).FirstOrDefault().MapInsurer();
        }

        public IEnumerable<long> GetPoliciesNumbersByAgent(string agentName)
        {
            var policies = InsurancePolicyRepositoty.Get(p => p.Agent.Name == agentName);
            return policies.Select(p => p.Number).ToList();
        }

        public PolicyDto GetPolicyByInsurerPhone(string phone)
        {
            return InsurancePolicyRepositoty.Get(p => p.Insurer.PhoneNumber == phone).FirstOrDefault().MapInsurancePolicy();
        }

        public PolicyDto GetPolicyByNumber(long number)
        {
            return InsurancePolicyRepositoty.Get(p => p.Number == number).FirstOrDefault().MapInsurancePolicy();
        }

        #endregion

    }
}
