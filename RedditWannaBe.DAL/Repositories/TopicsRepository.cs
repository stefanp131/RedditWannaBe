using Microsoft.EntityFrameworkCore;
using RedditWannaBe.DAL.Context;
using RedditWannaBe.DAL.Entities;
using RedditWannaBe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditWannaBe.DAL.Repositories
{
    public class TopicsRepository: ITopicsRepository
    {
        private readonly RedditWannaBeContext context;

        public TopicsRepository(RedditWannaBeContext context)
        {
            this.context = context;
        }

        public async Task Create(Topic entity)
        {
            await context.Topics.AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await context.Topics.FirstOrDefaultAsync(e => e.Id == id);
            context.Remove(entity);
        }

        public async Task<Topic> Get(Guid id)
        {
            var entity = await context.Topics.Include(e => e.CreatedBy).FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public List<Topic> GetAll(QueryForTopic filter, out int totalPages)
        {
            var query = context.Topics
                .Include(e => e.CreatedBy)
                .Where(e => e.Title.Contains(filter.SearchItemQuery) || e.Description.Contains(filter.SearchItemQuery));

            totalPages = (int)(Math.Ceiling((double)query.Count() / filter.PageSize));

            var list = query
                .Skip((filter.CurrentPage - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
            return list;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
