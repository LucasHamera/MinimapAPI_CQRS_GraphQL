using MinimapAPIDemo.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinimapAPIDemo.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Login);

        builder.Property(x => x.Login).HasColumnName("Login");
        builder.Property(x => x.Password).HasColumnName("Description");
    }
}