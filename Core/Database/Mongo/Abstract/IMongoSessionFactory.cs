using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Mongo.Abstract
{
    public interface IMongoSessionFactory
    {
        public Task<IClientSessionHandle> CreateTaskAsync();
    }
}
