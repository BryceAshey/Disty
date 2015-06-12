using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Disty.Common.Contract.Distributions;
using Disty.Common.Net.Http;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service.Endpoint.Http
{
    public interface IDistributionListController : IController<DistributionList>
    {
        Task<IHttpActionResult> Delete(int id);
        Task<IHttpActionResult> Get();
        Task<IHttpActionResult> Get(int id);
        Task<IHttpActionResult> Post(DistributionList item);
    }

    [AllowAnonymous]
    [RoutePrefix("api/distributionList")]
    public class DistributionListController : ApiController, IDistributionListController
    {
        private readonly IDistributionListService _service;
        private readonly ILog _log;

        public DistributionListController(ILog log, IDistributionListService service)
        {
            _log = log;
            _service = service;
        }

        [Route("")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await Task.Run(() => _service.DeleteAsync(id));
                return Ok();
            }
            catch (Exception ex)
            {
                _log.Error("Error creating email.", ex);
                return InternalServerError(new Exception("Unable to create email."));
            }
        }

        [Route("")]
        [ResponseType(typeof (IEnumerable<DistributionList>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var list = await Task.Run(() => _service.GetAsync());

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
                var list = await Task.Run(() => _service.GetAsync(id));
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
        public async Task<IHttpActionResult> Post(DistributionList item)
        {
            try
            {
                var id = await Task.Run(() => _service.SaveAsync(item));
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