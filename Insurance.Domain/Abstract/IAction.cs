using Insurance.Api.Model;
using Insurance.Api.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Abstract
{
    public interface IAction
    {
       Systems Name { get; }
       InsurerDto GetInsurerByPhone(string phone);
       PolicyDto  GetPolicyByInsurerPhone(string phone);
       IEnumerable<PolicyDto> GetActualPolicies();
       IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(long policyNumber);
       IEnumerable<long> GetPoliciesNumbersByAgent(string agentName);
       PolicyDto GetPolicyByNumber(long number);
    }
}
