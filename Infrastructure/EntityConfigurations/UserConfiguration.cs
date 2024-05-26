using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(u => u.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(50);

            builder.Property(u => u.ImageUrl)
                .HasColumnName("image_url");

            builder.Property(u => u.IsReported)
                .HasColumnName("is_reported");

            builder.Property(u => u.IsBlocked)
                .HasColumnName("is_blocked");

            builder.Property(u => u.Description)
                .HasColumnName("description");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasIndex(u => u.Email).HasDatabaseName("IX_Users_Email");
        }
    }
}
