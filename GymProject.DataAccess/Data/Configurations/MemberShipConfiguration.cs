using GymProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymProject.DataAccess.Data.Configurations
{
    public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {

            builder.Property(m => m.CreatedAt)
             .HasColumnName("StartDate")
             .HasDefaultValueSql("GETDATE()");


        }
    }
}
