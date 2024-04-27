using eAppointmentServer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Infrastructure.Configurations
{
    internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(50);
            builder.Property(p => p.LastName).HasMaxLength(50);
            builder.Property(p => p.City).HasMaxLength(50);
            builder.Property(p => p.Town).HasMaxLength(50);
            builder.Property(p => p.FullAddress).HasMaxLength(250);
            builder.Property(p => p.IdentityNumber).HasMaxLength(11);
            builder.HasIndex(p => p.IdentityNumber).IsUnique();
        }
    }
}
