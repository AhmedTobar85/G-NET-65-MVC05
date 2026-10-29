using GymProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Data.Configurations
{
    public class UserConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : User
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .HasColumnType("Varchar"); 
            builder.Property(t => t.Email)
                .HasMaxLength(100)
                .HasColumnType("Varchar");

            builder.HasIndex(t => t.Email).IsUnique();
            builder.HasIndex(t => t.Name).IsUnique();


            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK-User_Email", "Email like '_%@_%._%'");
                t.HasCheckConstraint(
                    "CK-User_Phone",
                    "LEN([Phone]) = 11 AND [Phone] LIKE '01[0125]%'"
                );
            });
            builder.OwnsOne(t => t.Address, address => {

                address.Property(t => t.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(50);
                address.Property(t => t.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(50);
                address.Property(t => t.BuildingNumber).HasColumnName("BuildingNumber");
                });

               
        }

    }
}
