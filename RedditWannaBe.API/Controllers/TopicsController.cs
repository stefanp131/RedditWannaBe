using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditWannaBe.API.Models;
using RedditWannaBe.DAL.Entities;
using RedditWannaBe.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace RedditWannaBe.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/topic")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsRepository repository;

        public TopicsController(ITopicsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await repository.Get(id);
            var dto = new TopicForRetrieval
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DateCreated = entity.DateCreated,
                CreatedBy = entity.CreatedBy.Username,
                CreatedById = entity.CreatedById
            };

            return Ok(dto);
        }

        [HttpGet()]
        public IActionResult GetAll([FromQuery] QueryForTopic query)
        {
            int totalPages;

            var entities = repository.GetAll(new DAL.Models.QueryForTopic
            {
                CurrentPage = query.CurrentPage,
                PageSize = query.PageSize,
                SearchItemQuery = query.SearchItemQuery
            }, out totalPages);

            var dtos = new PagedTopics();
            dtos.TotalPages = totalPages;

            foreach (var entity in entities)
            {
                var dto = new TopicForRetrieval
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    DateCreated = entity.DateCreated,
                    CreatedBy = entity.CreatedBy.Username,
                    CreatedById = entity.CreatedById
                };

                dtos.TopicsForRetrieval.Add(dto);
            }

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicForCreation topicForCreation)
        {
            var entity = new Topic
            {
                Title = topicForCreation.Title,
                Description = topicForCreation.Description,
                DateCreated = DateTime.Now,
                CreatedById = topicForCreation.CreatedById               
            };

            await repository.Create(entity);
            await repository.Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TopicForUpdate topicforUpdate)
        {
            var entity = await repository.Get(id);

            entity = new Topic
            {
                Title = topicforUpdate.Title,
                Description = topicforUpdate.Description,
            };

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
