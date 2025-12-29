namespace DataAccess.Data.Configuration
{
    internal class TrainerConfiguration : PersonConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(P => P.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(P => P.UpdatedAt);

            base.Configure(builder);
        }
    }
}
