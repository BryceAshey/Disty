using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Disty.Common.Contract.Distributions;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service.Endpoint.Http
{
    public interface IDistributionListController
    {
        Task<IHttpActionResult> Get();
        Task<IHttpActionResult> Get(int id);
        Task<IHttpActionResult> Post(DistributionList list);
    }

    [AllowAnonymous]
    [RoutePrefix("api/distributionList")]
    public class DistributionListController : ApiController, IDistributionListController
    {
        private readonly IDistributionListService _distributionListService;
        private readonly ILog _log;

        public DistributionListController(ILog log, IDistributionListService distributionListService)
        {
            _log = log;
            _distributionListService = distributionListService;
        }





        [Route("")]
        [ResponseType(typeof (IEnumerable<DistributionList>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var list = await Task.Run(() => _distributionListService.GetAsync());
                if (list == null)
                {
                    return NotFound();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                _log.Error("Error finding distribution list.", ex);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [Route("{id:int}", Name = "GetDistributionList")]
        [ResponseType(typeof (DistributionList))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var list = await Task.Run(() => _distributionListService.GetAsync(id));
                if (list == null)
                {
                    return NotFound();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                _log.Error("Error finding distribution list.", ex);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(DistributionList list)
        {
            try
            {
                var id = await Task.Run(() => _distributionListService.SaveAsync(list));
                if (id == 0)
                {
                    return InternalServerError(new Exception("Unable to create distribution list."));
                }

                return CreatedAtRoute<DistributionList>("GetDistributionList", new {id}, null);
            }
            catch (Exception ex)
            {
                _log.Error("Error creating distribution list.", ex);
                return InternalServerError(new Exception("Unable to create distribution list."));
            }
        }
    }
}