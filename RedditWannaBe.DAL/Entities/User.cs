using System;
using System.Collections.Generic;

namespace RedditWannaBe.DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Note> Notes { get; set; }
    }
}
