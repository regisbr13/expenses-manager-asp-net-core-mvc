using ExpensesManager.Map;
using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Data
{
    public class Context : DbContext
    {
        public DbSet<Month> Months { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpensesTypes { get; set; }
        public DbSet<IncomeType> IncomesTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseTypeMap());
            modelBuilder.ApplyConfiguration(new MonthMap());
            modelBuilder.ApplyConfiguration(new ExpenseMap());
            modelBuilder.ApplyConfiguration(new IncomeTypeMap());
            modelBuilder.ApplyConfiguration(new IncomeMap());
        }
    }
}
