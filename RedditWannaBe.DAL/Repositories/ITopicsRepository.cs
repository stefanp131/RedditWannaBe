using RedditWannaBe.DAL.Entities;
using RedditWannaBe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditWannaBe.DAL.Repositories
{
    public interface ITopicsRepository
    {
        Task Create(Topic entity);
        Task<Topic> Get(Guid id);
        List<Topic> GetAll(QueryForTopic query, out int totalPages);
        Task Delete(Guid id);
        Task Save();
    }
}
