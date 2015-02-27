using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Disty.Common.Contract.Distributions;
using Disty.Service.Interfaces;

namespace Disty.Service.Endpoint.Http
{
    public interface IDistributionListController
    {

        Task<IHttpActionResult> Get();

        Task<IHttpActionResult> Get(int id);

        Task<IHttpActionResult> Post(DistributionList list);
    }

    [RoutePrefix("api/distributionList")]
    public class DistributionListController : ApiController, IDistributionListController
    {
        private IDistributionListService _distributionListService;

        public DistributionListController(IDistributionListService distributionListService)
        {
            _distributionListService = distributionListService;
        }

        [Route("")]
        [ResponseType(typeof(List<DistributionList>))]
        public async Task<IHttpActionResult> Get()
        {
            var list = await Task.Run<List<DistributionList>>(() => _distributionListService.Get());
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // {AE861F4D-52D7-4899-833A-207F23FFE03B}
        [Route("{id:int}")]
        [ResponseType(typeof(DistributionList))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var list = await Task.Run<DistributionList>(() => _distributionListService.Get(id));
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(DistributionList list)
        {
            list = await Task.Run<DistributionList>(() => _distributionListService.Save(list));
            if (list == null)
            {
                return new System.Web.Http.Results.ExceptionResult(new Exception("Unable to create distribution list."), this);
            }

            return new System.Web.Http.Results.CreatedNegotiatedContentResult<DistributionList>(new Uri(""), null, this);
        }
    }
}
