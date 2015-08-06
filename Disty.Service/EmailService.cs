using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data.Repositories;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service
{
    public class EmailService : IEmailService
    {
        private readonly ILog _log;
        private readonly IEmailRepository _repository;

        public EmailService(ILog log, IEmailRepository repository)
        {
            _log = log;
            _repository = repository;
        }

        public void DeleteAsync(int id)
        {
            _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmailAddress>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<EmailAddress> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<EmailAddress>> GetByListAsync(int listId)
        {
            return await _repository.GetByListAsync(listId);
        }

        public async Task<int> SaveAsync(EmailAddress item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (string.IsNullOrEmpty(item.Address) || string.IsNullOrEmpty(item.Name))
                throw new ArgumentNullException("item",
                    "Neither Address nor Name may be null or an empty string.  Both are required.");

            //TODO when there is security add check that the user can update this list.

            return await _repository.SaveAsync(item);
        }
    }
}