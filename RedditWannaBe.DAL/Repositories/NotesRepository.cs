using Microsoft.EntityFrameworkCore;
using RedditWannaBe.DAL.Context;
using RedditWannaBe.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditWannaBe.DAL.Repositories
{
    public class NotesRepository: INotesRepository
    {

        private readonly RedditWannaBeContext context;

        public NotesRepository(RedditWannaBeContext context)
        {
            this.context = context;
        }

        public async Task Create(Note entity)
        {
            await context.Notes.AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await context.Notes.FirstOrDefaultAsync(e => e.Id == id);
            context.Remove(entity);
        }

        public async Task<Note> Get(Guid id)
        {
            var entity = await context.Notes.Include(e => e.CreatedBy).Include(e => e.Topic).FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<List<Note>> GetAll()
        {
            return await context.Notes.Include(e => e.CreatedBy).Include(e => e.Topic).ToListAsync();
        }

        public async Task<List<Note>> GetForTopic(Guid topicId)
        {
            return await context.Notes.Include(e => e.CreatedBy).Include(e => e.Topic).Where(e => e.TopicId == topicId).ToListAsync();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
