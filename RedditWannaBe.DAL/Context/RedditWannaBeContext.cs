﻿using Microsoft.EntityFrameworkCore;
using RedditWannaBe.DAL.Entities;

namespace RedditWannaBe.DAL.Context
{
    public class RedditWannaBeContext: DbContext
    {
        public RedditWannaBeContext(DbContextOptions<RedditWannaBeContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}
