using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesManager.Map
{
    public class ExpenseTypeMap : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(e => e.Expenses).WithOne(e => e.ExpenseType).HasForeignKey(e => e.ExpenseTypeId).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ExpenseType");
        }
    }
}
