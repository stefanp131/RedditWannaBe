using System.Collections.Generic;

namespace RedditWannaBe.API.Models
{
    public class PagedTopics
    {
        public int TotalPages { get; set; }
        public List<TopicForRetrieval> TopicsForRetrieval { get; set; } = new List<TopicForRetrieval>();
    }
}
