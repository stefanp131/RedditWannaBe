using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditWannaBe.API.Models;
using RedditWannaBe.DAL.Entities;
using RedditWannaBe.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditWannaBe.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/topic/{topicId}/note")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository repository;

        public NotesController(INotesRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetForTopic(Guid topicId)
        {
            var entities = await repository.GetForTopic(topicId);
            var dtos = new List<NoteForRetrieval>();

            foreach (var entity in entities)
            {
                var dto = new NoteForRetrieval
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    DateCreated = entity.DateCreated,
                    CreatedBy = entity.CreatedBy.Username,
                    CreatedById = entity.CreatedById,
                    Topic = entity.Topic.Title,
                    TopicId = entity.TopicId
                };

                dtos.Add(dto);
            };

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateForTopic(Guid topicId, NoteForCreation noteForCreation)
        {
            var entity = new Note
            {
                Title = noteForCreation.Title,
                Description = noteForCreation.Description,
                CreatedById = noteForCreation.CreatedById,
                DateCreated = DateTime.Now,
                TopicId = topicId
            };

            await repository.Create(entity);
            await repository.Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await repository.Delete(id);
            await repository.Save();

            return NoContent();
        }
    }
}
