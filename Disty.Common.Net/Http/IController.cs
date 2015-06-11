using Disty.Common.Contract;

namespace Disty.Common.Net.Http
{
    public interface IController<T> where T : DistyEntity
    {
    }
}