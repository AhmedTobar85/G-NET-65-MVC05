using GymProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Name)
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.HasData(LoadCategories());
        }

        private static List<Category> LoadCategories()
        {
            return new()
            {
                new Category { Id = 1,Name = "Cardio" },
                new Category { Id = 2,Name = "Strength" },
                new Category { Id = 3,Name = "Yoga" },
                new Category { Id = 4,Name = "Boxing" },
                new Category { Id = 5,Name = "CrossFit" }
            };
        }
   
    }
 }

