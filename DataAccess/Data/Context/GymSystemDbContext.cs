namespace DataAccess.Data.Context
{
    public class GymSystemDbContext(DbContextOptions<GymSystemDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(AU =>
            { 
                AU.Property(a => a.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                AU.Property(a => a.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            });
        }

        #region Tables

        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }

        #endregion
    }
}
