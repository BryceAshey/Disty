using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data;

namespace Disty.Model.MySql.Repositories
{
    public interface IEmailRepository : IRepository<EmailAddress, Email>
    {
        Task<IEnumerable<EmailAddress>> GetAsync(int listId);
        Task<EmailAddress> GetAsync(int listId, int id);        
    }
}
