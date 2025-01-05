using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        
        {
            
        }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Basket> Baskets { get; set; }
		public DbSet<BasketProduct> BasketProducts { get; set; }
		public DbSet<ContactMessage> ContactMessages { get; set; }
		public DbSet<News> AllNews { get; set; }
		public DbSet<TeamMember> TeamMembers { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }



	}
}
