namespace DataAccess.Data.Configuration
{
    internal class MemberConfiguration : PersonConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(P => P.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(P => P.UpdatedAt);

            base.Configure(builder);
        }
    }
}
