namespace DataAccess.Data.Configuration
{
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Ignore(MC => MC.Id);
            builder.Ignore(MC => MC.UpdatedAt);

            builder.HasKey(MC => new
            {
                MC.MemberId, MC.SessionId
            });

            builder.Property(MC => MC.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
