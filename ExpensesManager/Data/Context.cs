﻿using ExpensesManager.Models;
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
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpensesTypes { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}
