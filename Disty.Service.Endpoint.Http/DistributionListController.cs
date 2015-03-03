using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Disty.Common.Contract.Distributions;
using Disty.Common.Net.Http;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service.Endpoint.Http
{
    public interface IDistributionListController
    {

        Task<IHttpActionResult> Get();

        Task<IHttpActionResult> Get(string id);

        Task<IHttpActionResult> Post(DistributionList list);
    }

    [RoutePrefix("api/distributionList")]
    public class DistributionListController : ApiController, IDistributionListController
    {
        private ILog _log;
        private IDistributionListService _distributionListService;

        public DistributionListController(ILog log, IDistributionListService distributionListService)
        {
            _log = log;
            _distributionListService = distributionListService;
        }

        [Route("")]
        [ResponseType(typeof(List<DistributionList>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var list = await Task.Run<List<DistributionList>>(() => _distributionListService.GetAsync());
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

        // {AE861F4D-52D7-4899-833A-207F23FFE03B}
        [Route("{id:int}", Name="GetDistributionList")]
        [ResponseType(typeof(DistributionList))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var list = await Task.Run<DistributionList>(() => _distributionListService.GetAsync(id));
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
                list = await Task.Run<DistributionList>(() => _distributionListService.SaveAsync(list));
                if (list == null)
                {
                    return InternalServerError(new Exception("Unable to create distribution list."));
                }

                return CreatedAtRoute<DistributionList>("GetDistributionList", new { Id = list.Id }, null);
            }
            catch(Exception ex)
            {
                _log.Error("Error creating distribution list.", ex);
                return InternalServerError(new Exception("Unable to create distribution list."));
            }            
        }
    }
}
