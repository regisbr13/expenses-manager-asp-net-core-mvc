using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpensesManager.Map
{
    public class IncomeTypeMap : IEntityTypeConfiguration<IncomeType>
    {
        public void Configure(EntityTypeBuilder<IncomeType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(e => e.Incomes).WithOne(e => e.IncomeType).HasForeignKey(e => e.IncomeTypeId);

            builder.ToTable("IncomeType");
        }
    }
}

