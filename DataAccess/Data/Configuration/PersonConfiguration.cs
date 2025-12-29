namespace DataAccess.Data.Configuration
{
    internal class PersonConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(P => P.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(P => P.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(P => P.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("CK_ValidEmailCheck", "Email Like '_%@_%._%'");
                Tb.HasCheckConstraint("CK_ValidPhoneCheck", "Phone Like '01%' and Phone Not Like '%[^0-9]%'");
            }
            );

            builder.HasIndex(I => I.Email).IsUnique();
            builder.HasIndex(I => I.Phone).IsUnique();

            builder.OwnsOne(O => O.Address, AddressBuilder => 
            {
                AddressBuilder.Property(P => P.Street)
                              .HasColumnName("Street")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);

                AddressBuilder.Property(P => P.City)
                              .HasColumnName("City")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);
            });
        }
    }
}
