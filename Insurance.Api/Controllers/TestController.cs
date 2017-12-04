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

namespace Insurance.Api.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly IContainer _providersContainer;
        public TestController(IContainer container)
        {
            _providersContainer = container;
        }

        /// <summary>
        /// Searches and aggregates insurer data from all systems in one object 
        /// </summary>
        /// <param name="phoneNumber">Phone number of insurer</param>
        /// <returns>Aggregated insurer data</returns>
        [HttpGet]
        [Route("GetInsurerByPhone")]
        public IHttpActionResult GetInsurerByPhone(string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Incorrect phone number");
            }

            var data = _providersContainer.Resolve<IEnumerable<IAction>>()
                                 .Select(service => service.GetInsurerByPhone(phoneNumber))?.MergeObjects();
            
            return Ok(data);
        }

        /// <summary>
        /// Searches and aggregates policy data from all systems in one object 
        /// </summary>
        /// <param name="phoneNumber">Phone number of insurer</param>
        /// <returns>Aggregated policy data</returns>
        [HttpGet]
        [Route("GetPolicyByInsurerPhone")]
        public IHttpActionResult GetPolicyByInsurerPhone(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Incorrect phone number");
            }

            var data = _providersContainer.Resolve<IEnumerable<IAction>>()
                                 .Select(service => service.GetPolicyByInsurerPhone(phoneNumber))?.MergeObjects();
            return Ok(data);
        }

        /// <summary>
        /// Searches and aggregates actual policies from all systems
        /// </summary>
        /// <returns>Aggregated list of actual policies </returns>
        [HttpGet]
        [Route("GetActualPolicies")]
        public IHttpActionResult GetActualPolicies()
        {
            var data = _providersContainer.Resolve<IEnumerable<IAction>>()
                                 .Select(service => service.GetActualPolicies()?.MergeObjects());
            return Ok(data);
        }

        /// <summary>
        /// Searches and aggregates beneficiaries data from all systems
        /// </summary>
        /// <param name="policyId">Particular policy identifier</param>
        /// <returns>List of aggregated beneficiary data</returns>
        [HttpGet]
        [Route("GetBeneficiariesByPolicy")]
        public IHttpActionResult GetBeneficiariesByPolicy(Guid policyId)
        {
            if (string.IsNullOrWhiteSpace(policyId.ToString()))
            {
                return BadRequest("Incorrect policy id");
            }

            var data = _providersContainer.Resolve<IEnumerable<IAction>>()
                                 .Select(service => service.GetBeneficiariesByPolicy(policyId)?.MergeObjects());
            return Ok(data);
        }

        /// <summary>
        /// Searches and aggregates policies data from all systems 
        /// </summary>
        /// <param name="agentName">Name of agent</param>
        /// <returns>List of aggregated beneficiary data</returns>
        [HttpGet]
        [Route("GetPolicyByAgent")]
        public IHttpActionResult GetPolicyByAgent(string agentName)
        {
            if (string.IsNullOrWhiteSpace(agentName))
            {
                return BadRequest("Incorrect agent name");
            }

            var data = _providersContainer.Resolve<IEnumerable<IAction>>()
                                 .Select(service => service.GetPoliciesByAgent(agentName)?.MergeObjects());
            return Ok(data);
        }
    }
}
