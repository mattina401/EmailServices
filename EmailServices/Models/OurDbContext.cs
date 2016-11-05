using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace EmailServices.Models
{
    public class OurDbContext : DbContext
    {
        public DbSet<UserAccount> userAccount { get; set; }
    }

    public class MessageDb : DbContext
    {
        public DbSet<Message> message { get; set; }
    }
}