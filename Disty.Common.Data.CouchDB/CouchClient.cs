using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MyCouch;
using MyCouch.Requests;

namespace Disty.Common.Data.CouchDB
{
    public class CouchClient<T> : IDataClient<T> where T : class, new()
    {
        private ILog _log;
        private IMyCouchStore _couchStore;

        public CouchClient(ILog log, IMyCouchStore couchStore)
        {
            _log = log;
            _couchStore = couchStore;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Task.Factory.StartNew(() =>
                {
                    T[] list = new T[0];

                    try
                    {
                        var query = new Query("optum", "all");
                        var queryInfo = _couchStore.QueryAsync<T>(query);
                        var result = queryInfo.Result;
                        
                        if(result != null && result.Any())
                            list = result.Select(q => q.Value).ToArray<T>();
                        
                        return list;
                    }
                    catch(Exception ex)
                    {
                        _log.Error("Error querying distribution lists.", ex);
                        return list;
                    }
                });
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _couchStore.GetByIdAsync<T>(id);
        }

        public async Task<T> SaveAsync(T obj)
        {
            return await _couchStore.StoreAsync<T>(obj);
        }
                
        public async Task<bool> DeleteAsync(T obj)
        {
            return await _couchStore.DeleteAsync<T>(obj);
        }
    }
}
