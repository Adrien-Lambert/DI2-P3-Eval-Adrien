using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Data.DatabaseContext
{
    public class DbContextFactory : IDesignTimeDbContextFactory<BackendDbContext>
    {
        /// <summary>
        /// Creates an instance of <see cref="BackendDbContext"/> using design-time parameters.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time tool (expects the connection string as the first argument).</param>
        /// <returns>An initialized instance of <see cref="BackendDbContext"/>.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when no connection string is provided in the <paramref name="args"/> parameter.
        /// </exception>
        public BackendDbContext CreateDbContext(string[] args)
        {

            var connectionString = args.First();

            var builder = new DbContextOptionsBuilder<BackendDbContext>();
            builder.UseSqlServer(connectionString);

            return new BackendDbContext(builder.Options);
        }
    }
}
