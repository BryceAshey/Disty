using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Disty.Common.Contract.Distributions;

namespace Disty.Model.MySql.Repositories
{
    public class EmailRepository : RepositoryBase<EmailAddress, Email>, IEmailRepository
    {
        public override async Task<IEnumerable<EmailAddress>> GetAsync()
        {
            throw new NotImplementedException("You must use the GetAsync call with the listId parameter.");
        }

        public override async Task<IEnumerable<EmailAddress>> GetAsync(string includes)
        {
            throw new NotImplementedException("You must use the GetAsync call with the listId parameter.");
        }

        public override async Task<EmailAddress> GetAsync(int id)
        {
            throw new NotImplementedException("You must use the GetAsync call with the listId parameter.");
        }

        public virtual async Task<IEnumerable<EmailAddress>> GetAsync(int listId)
        {
            using (var db = new DistyModelContainer())
            {
                return
                    await
                        Task.FromResult<IEnumerable<EmailAddress>>(
                            db.Set<Email>()
                                .Include("List")
                                .AsEnumerable()
                                .Where(e => e.List.Id == listId)
                                .Select(Mapper.Map<Email, EmailAddress>)
                                .ToList());
            }
        }

        public virtual async Task<IEnumerable<EmailAddress>> GetAsync(int listId, string includes)
        {
            if(!includes.Contains("List"))
                throw new ArgumentException("Includes must include 'List' in the values.")

            using (var db = new DistyModelContainer())
            {
                return
                    await
                        Task.FromResult<IEnumerable<EmailAddress>>(
                            db.Set<Email>()
                                .Include(includes)
                                .AsEnumerable()
                                .Where(e => e.List.Id == listId)
                                .Select(Mapper.Map<Email, EmailAddress>)
                                .ToList());
            }
        }

        public virtual async Task<EmailAddress> GetAsync(int listId, int id)
        {
            using (var db = new DistyModelContainer())
            {
                var result = await Task.FromResult<EmailAddress>(
                        Mapper.Map<Email, EmailAddress>(db.Set<Email>()
                                .Find(id))
                    );

                if (result.ListId != listId)
                    return null;

                return result;
            }
        }
    }
}
