using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data.Repositories;
using Disty.Common.Log.Exceptions;
using log4net;

namespace Disty.Model.MsSql.Repositories
{
    public class EmailRepository : RepositoryBase<EmailAddress, Email>, IEmailRepository
    {
        public EmailRepository(ILog log)
            : base(log)
        {
        }

        public override async Task<IEnumerable<EmailAddress>> GetAsync()
        {
            throw new NotImplementedException("You must use the GetByListAsync call with the listId parameter in place of this method.");
        }

        public override async Task<IEnumerable<EmailAddress>> GetAsync(string includes)
        {
            throw new NotImplementedException("You must use the GetByListAsync call with the listId parameter in place of this method.");
        }

        public virtual async Task<EmailAddress> GetAsync(int id)
        {
            using (var db = new DistyEntities())
            {
                return await Task.FromResult<EmailAddress>(
                        Mapper.Map<Email, EmailAddress>(db.Set<Email>()
                                .Find(id))
                    );
            }
        }

        public virtual async Task<IEnumerable<EmailAddress>> GetByListAsync(int listId)
        {
            using (var db = new DistyEntities())
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

        public override async Task<int> SaveAsync(EmailAddress item)
        {
            try
            {
                if(item == null)
                    throw new ArgumentNullException("item");

                using (var db = new DistyEntities())
                {
                    var dbItem = Mapper.Map<EmailAddress, Email>(item);
                    if (item.Id == 0)
                    {
                        var list = await db.Set<List>().FindAsync(item.ListId);
                        if (list == null)
                            throw new InvalidOperationException("Unable to find associated list.");

                        list.Emails.Add(dbItem);
                    }
                    else
                    {
                        db.Set<Email>().Attach(dbItem);
                        db.Entry(item).State = EntityState.Modified;
                    }

                    await db.SaveChangesAsync();
                    return dbItem.Id;
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = HandleValidationError(e);
                _log.Error(string.Format("Error saving Disty list:\r\n  {0}", msg), e);
                throw new LoggedException(msg, e);
            }
        }
    }
}
