namespace DataAccess.Data.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(P => P.CategoryName)
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Ignore(P => P.CreatedAt);
            builder.Ignore(P => P.UpdatedAt);

        }
    }
}
