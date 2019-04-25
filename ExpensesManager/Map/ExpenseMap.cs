using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesManager.Map
{
    public class ExpenseMap : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);

            builder.HasOne(e => e.Month).WithMany(e => e.Expenses).HasForeignKey(e => e.MonthId);
            builder.HasOne(e => e.ExpenseType).WithMany(e => e.Expenses).HasForeignKey(e => e.ExpenseTypeId);

            builder.ToTable("Expenses");
        }
    }
}
