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
    public interface IEmailController : IController<EmailAddress>
    {
    }

    [AllowAnonymous]
    [RoutePrefix("api/distributionList/{listId:int}/email")]
    public class EmailController : ApiController, IEmailController
    {
        private readonly IEmailService _service;
        private readonly ILog _log;

        public EmailController(ILog log, IEmailService service)
        {
            _log = log;
            _service = service;
        }

        [Route("")]
        [ResponseType(typeof(IEnumerable<DistributionList>))]
        public async Task<IHttpActionResult> Get(int listId)
        {
            try
            {
                var list = await Task.Run(() => _service.GetAsync(listId));

                if (list == null)
                {
                    return NotFound();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                _log.Error("Error finding email.", ex);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [Route("{id:int}", Name = "GetEmailAddress")]
        [ResponseType(typeof(EmailAddress))]
        public async Task<IHttpActionResult> Get(int listId, int id)
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
                _log.Error("Error finding email.", ex);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(int listId, EmailAddress item)
        {
            try
            {
                item.ListId = listId;
                var id = await Task.Run(() => _service.SaveAsync(item));
                if (id == 0)
                {
                    return InternalServerError(new Exception("Unable to create email."));
                }

                return CreatedAtRoute<EmailAddress>("GetEmailAddress", new { id }, null);
            }
            catch (Exception ex)
            {
                _log.Error("Error creating email.", ex);
                return InternalServerError(new Exception("Unable to create email."));
            }
        }
    }
}
