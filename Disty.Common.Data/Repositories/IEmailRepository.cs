using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data;

namespace Disty.Common.Data.Repositories
{
    public interface IEmailRepository : IRepository<EmailAddress>
    {
        Task<IEnumerable<EmailAddress>> GetByListAsync(int listId);
    }
}
