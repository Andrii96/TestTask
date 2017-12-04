using Insurance.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Api.Model.Dtos;
using Insurance.Domain.Entities.SystemA;
using Insurance.Domain.Contexts;

namespace Insurance.Domain.Services
{
    public class SystemAService : IAction
    {
        #region Properties
            public IRepository<Insurer> InsurerRepositoty => new Repository<SystemADbContext, Insurer>();
            public IRepository<InsurancePolicy> InsurancePolicyRepositoty => new Repository<SystemADbContext, InsurancePolicy>();
            public IRepository<Beneficiary> BeneficiaryRepositoty => new Repository<SystemADbContext, Beneficiary>();
        #endregion

        #region interface implementation

        public IEnumerable<PolicyDto> GetActualPolicies()
        {
            var actualPolicies = InsurancePolicyRepositoty.Get(p => p.IsActive);
            return actualPolicies.Select(p => MapInsurancePolicy(p)).ToList();
        }

        public IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber)
        {
            var policy = InsurancePolicyRepositoty.Get(p => p.Number == policyNumber).FirstOrDefault();
            if(policy == null)
            {
                return null;
            }
            var beneficiares = BeneficiaryRepositoty.Get(b => b.InsurancePolicyId == policy.Id);

            return beneficiares.Select(b => MapBeneficiary(b)).ToList();
        }

        public InsurerDto GetInsurerByPhone(string phone)
        {
            return MapInsurer(InsurerRepositoty.Get(i => i.PhoneNumber == phone).FirstOrDefault());
        }

        public IEnumerable<long> GetPoliciesNumbersByAgent(string agentName)
        {
            var policies = InsurancePolicyRepositoty.Get(p => p.AgentName == agentName).ToList();
             return policies.Select(p => p.Number).ToList();
        }

        public PolicyDto GetPolicyByInsurerPhone(string phone)
        {
            return  MapInsurancePolicy(InsurancePolicyRepositoty.Get(p => p.Insurer.PhoneNumber == phone).FirstOrDefault());
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
            if(policy == null)
            {
                return null;
            }

            return new PolicyDto
            {
                Id = policy.Id,
                Agent = new AgentDto
                {
                    Name = policy.AgentName
                },
                IsActive = policy.IsActive,
                Number = policy.Number,
                Beneficiary = policy.Beneficiaries.Select(b => MapBeneficiary(b)),
                Insurer = MapInsurer(policy.Insurer),
                DateFrom = new DateTime(1,1,1,0,0,0),
                DateTill = new DateTime(1,1,1,0,0,0)
            };
        }
    
        #endregion

    }
}
