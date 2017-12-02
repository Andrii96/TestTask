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
       InsurerDto GetInsurerByPhone(string phone);
       PolicyDto  GetPolicyByInsurerPhone(string phone);
       IEnumerable<PolicyDto> GetActualPolicies();
       IEnumerable<BeneficiaryDto> GetBeneficiariesByPolicy(Guid id);
       IEnumerable<PolicyDto> GetPoliciesByAgent(string agentName);
    }
}
