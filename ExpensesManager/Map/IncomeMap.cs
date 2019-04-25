using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesManager.Map
{
    public class IncomeMap : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("varchar").IsRequired(false);

            builder.HasOne(e => e.Month).WithMany(e => e.Incomes).HasForeignKey(e => e.MonthId);
            builder.HasOne(e => e.IncomeType).WithMany(e => e.Incomes).HasForeignKey(e => e.IncomeTypeId);

            builder.ToTable("Incomes");
        }
    }
}
