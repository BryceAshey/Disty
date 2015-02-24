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

        Task<IHttpActionResult> Get(Guid id);

    }

    [RoutePrefix("api/distributionList")]
    public class DistributionListController : ApiController, IDistributionListController
    {
        private IDistributionListService _distributionListService;

        public DistributionListController(IDistributionListService distributionListService)
        {
            _distributionListService = distributionListService;
        }

        // {AE861F4D-52D7-4899-833A-207F23FFE03B}
        [Route("{id:guid}")]
        [ResponseType(typeof(DistributionList))]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var list = await Task.Run<DistributionList>(() => _distributionListService.Get(id));
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
    }
}
