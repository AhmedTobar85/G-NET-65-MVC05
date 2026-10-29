using GymProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Data.Configurations
{
    public class TrainerConfiguration : UserConfiguration<Trainer>,IEntityTypeConfiguration<Trainer>
    {
        public new  void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(m => m.CreatedAt)
              .HasColumnName("JoinDate")
              .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
        }
    }
}
