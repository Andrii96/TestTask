using Insurance.Api.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurance.Api.Helpers
{
    public static class Merge
    {
        public static InsurerDto MergeObjects(this IEnumerable<InsurerDto> insurers)
        {
            var mergedInsurer = new InsurerDto();

            foreach(var insurer in insurers.Where(i => i != null))
            {
                if(insurer.FirstName != null)
                {
                    mergedInsurer.FirstName = insurer.FirstName;
                }
                if (insurer.LastName != null)
                {
                    mergedInsurer.LastName = insurer.LastName;
                }
                if (insurer.Phone != null)
                {
                    mergedInsurer.Phone = insurer.Phone;
                }
            }
            return mergedInsurer;
        }

        public static PolicyDto MergeObjects(this IEnumerable<PolicyDto> policies)
        {
            var mergedPolicies = new PolicyDto() {Beneficiary = new List<BeneficiaryDto>() };

            foreach (var policy in policies.Where(p => p != null))
            {
                mergedPolicies.Agent = policy.Agent;
                if (policy.DateFrom != default(DateTime))
                {
                    mergedPolicies.DateFrom = policy.DateFrom;
                }
                if (policy.DateTill != default(DateTime))
                {
                    mergedPolicies.DateTill = policy.DateTill;
                }
                if(policy.Beneficiary != null)
                {
                    mergedPolicies.Beneficiary = mergedPolicies.Beneficiary
                                                        .Union(policy.Beneficiary)
                                                        .ToList();
                }
             
                if(policy.Insurer != null)
                {
                    mergedPolicies.Insurer = policy.Insurer;
                }
                
                if (policy.Number != default(long))
                {
                    mergedPolicies.Number = policy.Number;
                }
                mergedPolicies.IsActive = policy.IsActive;
            }

            return mergedPolicies;
        }
    }
}