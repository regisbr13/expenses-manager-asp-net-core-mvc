﻿using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Map
{
    public class MonthMap : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();
            builder.Property(m => m.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(m => m.Expenses).WithOne(m => m.Month).HasForeignKey(m => m.MonthId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(m => m.Incomes).WithOne(m => m.Month).HasForeignKey(m => m.MonthId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Months");
        }
    }
}
