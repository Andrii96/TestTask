using Insurance.Api.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Mapper
{
    internal static class Mapper
    {
        public static InsurerDto MapInsurer(this Insurance.Domain.Entities.SystemA.Insurer insurer)
        {
             if (insurer == null)
                {
                    return null;
                }

                return new InsurerDto
                {
                    //Id = insurer.Id,
                    FirstName = insurer.FirstName,
                    LastName = insurer.LastName,
                    Phone = insurer.PhoneNumber
                };
        }

        public static InsurerDto MapInsurer(this Insurance.Domain.Entities.SystemB.Insurer insurer)
        {
            if (insurer == null)
            {
                return null;
            }

            return new InsurerDto
            {
                //Id = insurer.Id,
                FirstName = insurer.FirstName,
                LastName = insurer.LastName,
                Phone = insurer.PhoneNumber
            };
        }

        public static InsurerDto MapInsurer(this Insurance.Domain.Entities.SystemC.Insurer insurer)
        {
            if (insurer == null)
            {
                return null;
            }

            return new InsurerDto
            {
               // Id = insurer.Id,
                FirstName = insurer.FirstName,
                LastName = insurer.LastName
            };
        }

        public static BeneficiaryDto MapBeneficiary(this Insurance.Domain.Entities.SystemA.Beneficiary beneficiary)
        {
            if (beneficiary == null)
            {
                return null;
            }

            return new BeneficiaryDto
            {
                //Id = beneficiary.Id,
                Name = beneficiary.Name
            };
        }

        public static BeneficiaryDto MapBeneficiary(this Insurance.Domain.Entities.SystemC.Beneficiary beneficiary)
        {
            if (beneficiary == null)
            {
                return null;
            }

            return new BeneficiaryDto
            {
                //Id = beneficiary.Id,
                Name = beneficiary.Name
            };
        }

        public static PolicyDto MapInsurancePolicy(this Insurance.Domain.Entities.SystemA.InsurancePolicy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return new PolicyDto
            {
                //Id = policy.Id,
                Agent = new AgentDto
                {
                    Name = policy.AgentName
                },
                IsActive = policy.IsActive,
                Number = policy.Number,
                Beneficiary = policy.Beneficiaries.Select(b => MapBeneficiary(b)),
                Insurer = policy.Insurer.MapInsurer(),
                DateFrom = new DateTime(1, 1, 1, 0, 0, 0),
                DateTill = new DateTime(1, 1, 1, 0, 0, 0)
            };
        }

        public static PolicyDto MapInsurancePolicy(this Insurance.Domain.Entities.SystemB.InsurancePolicy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return new PolicyDto
            {
               // Id = policy.Id,
                DateFrom = policy.DateFrom,
                DateTill = policy.DateTill,
                Number = policy.Number,
                Agent = policy.Agent.MapAgent(),
                Insurer = policy.Insurer.MapInsurer(),
                IsActive = policy.DateFrom < DateTime.Now && policy.DateTill > DateTime.Now
            };
        }

        public static PolicyDto MapInsurancePolicy(this Insurance.Domain.Entities.SystemC.InsurancePolicy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return new PolicyDto
            {
                //Id = policy.Id,
                DateFrom = policy.DateFrom,
                DateTill = policy.DateTill,
                Number = policy.Number,
                Agent = policy.Agent.MapAgent(),
                Insurer = policy.Insurer.MapInsurer(),
                Beneficiary = policy.Beneficiaries.Select(p => MapBeneficiary(p)),
                IsActive = policy.DateFrom < DateTime.Now && policy.DateTill > DateTime.Now
            };
        }

        public static AgentDto MapAgent(this Insurance.Domain.Entities.SystemB.Agent agent)
        {
            if (agent == null)
            {
                return null;
            }

            return new AgentDto
            {
               // Id = agent.Id,
                Name = agent.Name
            };
        }

        public static AgentDto MapAgent(this Insurance.Domain.Entities.SystemC.Agent agent)
        {
            if (agent == null)
            {
                return null;
            }

            return new AgentDto
            {
                //Id = agent.Id,
                Name = agent.Name
            };
        }
    }
}
