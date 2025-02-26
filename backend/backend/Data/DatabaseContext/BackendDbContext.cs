using backend.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.DatabaseContext
{
    public class BackendDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackendDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) { }


        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the Passwords table in the database.
        /// </summary>
        public DbSet<Password> Passwords { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> representing the Applications table in the database.
        /// </summary>
        public DbSet<Application> Applications { get; set; }

        /// <summary>
        /// Configures the entity relationships and seed data for the context.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure the model for the database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Password>()
            .HasOne(p => p.Application)
            .WithMany(a => a.Passwords)
            .HasForeignKey(p => p.ApplicationId)
            .IsRequired();


            modelBuilder.Entity<Application>()
                .HasIndex(a => a.ApplicationName)
            .IsUnique();
        }
    }
}
