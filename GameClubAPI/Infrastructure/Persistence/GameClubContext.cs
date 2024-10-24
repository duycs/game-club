using Domain.Clubs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences
{
    public class GameClubContext : DbContext, IDatabaseService
    {
        private string ConnectionString { get; set; }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Event> Events { get; set; }


        /// <summary>
        /// Must concreate first for ef migrations
        /// </summary>
        public GameClubContext() {}

        public GameClubContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public GameClubContext(DbContextOptions<GameClubContext> options) : base(options) { }

        DbSet<T> IDatabaseService.GetDbSet<T>()
        {
            return Set<T>();
        }

        Task IDatabaseService.SaveChanges()
        {
            return Task.FromResult(base.SaveChanges());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Hardcode connection string for migration CLI at infrastructure
                optionsBuilder.UseSqlite("Data Source=GameClubDatabase.db");
            }
        }

        /// <summary>
        /// ref: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club>().HasKey(c => c.Id);
            modelBuilder.Entity<Club>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Club>().Property(s => s.Description).IsRequired(false);

            modelBuilder.Entity<Event>().HasKey(c => c.Id);
            modelBuilder.Entity<Event>().Property(s => s.Description).IsRequired(false);
            modelBuilder.Entity<Event>().Property(s => s.Title).IsRequired();
            modelBuilder.Entity<Event>().Property(s => s.Scheduled).IsRequired();
            modelBuilder.Entity<Event>().HasOne(w => w.Club).WithMany(w => w.Events).HasForeignKey(w => w.ClubId);
        }
    }
}
