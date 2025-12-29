namespace DataAccess.Data.Configuration
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            #region [1 - 1] Member & HealtRecord

            builder.ToTable("Members");

            builder.HasOne(H => H.MemberHealth)
                .WithOne(H => H.HealthRecord)
                .HasForeignKey<HealthRecord>(H => H.Id);

            #endregion

            builder.Ignore(P => P.CreatedAt);
            builder.Ignore(P => P.UpdatedAt);
        }
    }
}
