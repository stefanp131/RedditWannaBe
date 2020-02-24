using System;
using System.ComponentModel.DataAnnotations;

namespace RedditWannaBe.API.Models
{
    public class TopicForCreation
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public Guid CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
