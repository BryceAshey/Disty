using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Service;

namespace Disty.Service.Interfaces
{
    public interface IEmailService : IBaseService<EmailAddress>
    {
        Task<IEnumerable<EmailAddress>> GetByListAsync(int listId);
    }
}
