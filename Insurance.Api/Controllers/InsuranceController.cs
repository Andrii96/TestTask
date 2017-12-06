using Autofac;
using Insurance.Api.Model.Dtos;
using Insurance.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insurance.Api.Helpers;
using Insurance.Api.Model;

namespace Insurance.Api.Controllers
{
    [RoutePrefix("api/insurance")]
    public class InsuranceController : ApiController
    {
        #region Fields
            private readonly IContainer _providersContainer;
        #endregion

        #region Constructor

        public InsuranceController(IContainer container)
        {
            _providersContainer = container;
        }

        #endregion

        #region Api

        /// <summary>
        /// Searches and aggregates insurer data from all systems in one object 
        /// </summary>
        /// <param name="phoneNumber">Phone number of insurer</param>
        /// <param name="providers">Data providers</param>
        /// <returns>Aggregated insurer data</returns>
        [HttpGet]
        [Route("GetInsurerByPhone")]
        public IHttpActionResult GetInsurerByPhone(string phoneNumber, Systems providers = Systems.All)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Incorrect phone number");
            }

            var providersList = providers == Systems.All ?
                                        _providersContainer.Resolve<IEnumerable<IAction>>() : 
                                        _providersContainer.Resolve<IEnumerable<IAction>>().Where(system => (providers & system.Name) == system.Name);

            var data = providersList.Select(service => service.GetInsurerByPhone(phoneNumber)).ToList()?.MergeObjects();

            return Ok(data);
        }

        /// <summary>
        /// Searches and aggregates policy data from all systems in one object 
        /// </summary>
        /// <param name="phoneNumber">Phone number of insurer</param>
        /// <param name="providers">Data providers</param>
        /// <returns>Aggregated policy data</returns>
        [HttpGet]
        [Route("GetPolicyByInsurerPhone")]
        public IHttpActionResult GetPolicyByInsurerPhone(string phoneNumber, Systems providers = Systems.All)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Incorrect phone number");
            }

            var providersList = providers == Systems.All ?
                                        _providersContainer.Resolve<IEnumerable<IAction>>() :
                                        _providersContainer.Resolve<IEnumerable<IAction>>().Where(system => (providers & system.Name) == system.Name);

            var data = providersList.Select(service => service.GetPolicyByInsurerPhone(phoneNumber)).ToList()?.MergeObjects();
            return Ok(data);
        }

        /// <summary>
        /// Searches and aggregates actual policies from all systems
        /// </summary>
        /// <param name="providers">Data providers</param>
        /// <returns>Aggregated list of actual policies </returns>
        [HttpGet]
        [Route("GetActualPolicies")]
        public IHttpActionResult GetActualPolicies(Systems providers = Systems.All)
        {
            var providersList = providers == Systems.All ?
                                        _providersContainer.Resolve<IEnumerable<IAction>>() :
                                        _providersContainer.Resolve<IEnumerable<IAction>>().Where(system => (providers & system.Name) == system.Name);

            var data = providersList.Select(service => service.GetActualPolicies()).ToList();

            var result = new List<PolicyDto>();

            for (int i = 0; i < data.First().Count(); i++)
            {
                List<PolicyDto> policies = new List<PolicyDto>();
                foreach (var item in data)
                {
                    policies.Add(item.ElementAt(i));
                }
                result.Add(policies.MergeObjects());
            }
            return Ok(result);
        }

        /// <summary>
        /// Searches and aggregates beneficiaries data from all systems
        /// </summary>
        /// <param name="policyNumber">Particular policy number</param>
        /// <param name="providers">Data providers</param>
        /// <returns>List of aggregated beneficiary data</returns>
        [HttpGet]
        [Route("GetBeneficiariesByPolicy")]
        public IHttpActionResult GetBeneficiariesByPolicy(long policyNumber, Systems providers = Systems.All)
        {
            if (string.IsNullOrWhiteSpace(policyNumber.ToString()))
            {
                return BadRequest("Incorrect policy number");
            }

            var providersList = providers == Systems.All ?
                                        _providersContainer.Resolve<IEnumerable<IAction>>() :
                                        _providersContainer.Resolve<IEnumerable<IAction>>().Where(system => (providers & system.Name) == system.Name);

            var data = providersList.Select(service => service.GetBeneficiariesByPolicy(policyNumber)).ToList();

            var list = new List<BeneficiaryDto>();

            foreach (var item in data)
            {
                if (item != null)
                {
                    list = list.Union(item).ToList();
                }
            }
            return Ok(list);
        }

        /// <summary>
        /// Searches and aggregates policies data from all systems 
        /// </summary>
        /// <param name="agentName">Name of agent</param>
        /// <param name="providers">Data providers</param>
        /// <returns>List of aggregated beneficiary data</returns>
        [HttpGet]
        [Route("GetPolicyByAgent")]
        public IHttpActionResult GetPolicyByAgent(string agentName, Systems providers = Systems.All)
        {
            if (string.IsNullOrWhiteSpace(agentName))
            {
                return BadRequest("Incorrect agent name");
            }
            var list = new List<long>();
            var result = new List<PolicyDto>();

            var providersList = providers == Systems.All ?
                                       _providersContainer.Resolve<IEnumerable<IAction>>() :
                                       _providersContainer.Resolve<IEnumerable<IAction>>().Where(system => (providers & system.Name) == system.Name);

            var data = providersList.Select(service => service.GetPoliciesNumbersByAgent(agentName)).ToList();

            data.ForEach(item => list = list.Union(item).ToList());

            foreach (var number in list)
            {
                var policy = _providersContainer.Resolve<IEnumerable<IAction>>().Select(service => service.GetPolicyByNumber(number))
                                                                                .ToList()
                                                                                .MergeObjects();
                result.Add(policy);
            }

            return Ok(result);
        }

        #endregion

    }
}
