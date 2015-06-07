using System.Threading.Tasks;
using System.Web.Http;
using Disty.Common.Contract;

namespace Disty.Common.Net.Http
{
    public interface IController<T> where T : DistyEntity
    {
        Task<IHttpActionResult> Get();
        Task<IHttpActionResult> Get(int id);
        Task<IHttpActionResult> Post(T item);
    }
}
