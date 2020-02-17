using System.ComponentModel.DataAnnotations;

namespace RedditWannaBe.API.Models
{
    public class UserForRegistration
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
