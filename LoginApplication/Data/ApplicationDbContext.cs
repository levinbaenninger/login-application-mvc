using LoginApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
	}
}
