﻿using ExpensesManager.Data;
using ExpensesManager.Models;
using ExpensesManager.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Services
{
    public class ExpenseService
    {
        private readonly Context _context;

        public ExpenseService(Context context)
        {
            _context = context;
        }

        // Listar todos:
        public async Task<List<Expense>> FindAllAsync()
        {
            return await _context.Expenses.Include(i => i.Month).Include(i => i.ExpenseType).OrderBy(i => i.MonthId).ToListAsync();
        }

        public async Task<List<Expense>> ExpensesByMonth(int id)
        {
            return await _context.Expenses.Include(e => e.ExpenseType).Where(e => e.MonthId == id).ToListAsync();
        }

        public async Task<List<Income>> IncomesByMonth(int id)
        {
            return await _context.Incomes.Include(e => e.IncomeType).Where(e => e.MonthId == id).ToListAsync();
        }

        // Listar meses:
        public async Task<List<Month>> FindAllMonths()
        {
            return await _context.Months.ToListAsync();
        }

        public async Task<List<ExpenseType>> FindAllExpenseType()
        {
            return await _context.ExpensesTypes.ToListAsync();
        }

        // Buscar por Id:
        public async Task<Expense> FindByIdAsync(int? id)
        {
            return await _context.Expenses.Include(i => i.Month).Include(i => i.ExpenseType).FirstOrDefaultAsync(t => t.Id == id);
        }

        // INSERIR:
        public async Task InsertAsync(Expense obj)
        {
            _context.Expenses.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Atualizar:
        public async Task UpdateAsync(Expense obj)
        {
            bool hasAny = await _context.Expenses.AnyAsync(t => t.Id == obj.Id);
            if (!hasAny)
            {
                throw new Exception("not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // REMOVER:
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Expenses.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();

        }

        // Buscar despesas
        public async Task<List<Expense>> Search(string s)
        {
            return await _context.Expenses.Include(i => i.Month).Include(i => i.ExpenseType).Where(i => i.Description.ToUpper().Contains(s.ToUpper())).ToListAsync();
        }

        // Meses com despesas ou receitas:
        public async Task<List<Month>> ExpenseIncomeMonths()
        {
            var expensesMonths = _context.Expenses.Select(e => e.MonthId);
            var incomesMonths = _context.Incomes.Select(i => i.MonthId);
            var list = expensesMonths.Union(incomesMonths);
            return await _context.Months.Where(m => list.Contains(m.Id)).ToListAsync();
        }

        // Dados para o grafico Receitas x Despesas:
        public double MonthlyIncome(int id)
        {
            return _context.Incomes.Where(i => i.MonthId == id).Sum(i => i.Value);
        }

        public double MonthlyExpenses(int id)
        {
            return _context.Expenses.Where(i => i.MonthId == id).Sum(i => i.Value);
        }

        // Despesas por mês:
        public List<string> ExpensesTypeByMonthId(int id)
        {
            var grouping = _context.Expenses.Where(e => e.MonthId == id).GroupBy(x => x.ExpenseType.Name);
            var list = new List<string>();

            foreach (IGrouping<string, Expense> group in grouping)
            {
                list.Add(group.Key.ToString());
            }
            return list;
        }

        public List<double> ValuesExpensesTypeByMonthId(int id)
        {
            var grouping = _context.Expenses.Where(e => e.MonthId == id).GroupBy(x => x.ExpenseType.Name);
            var list = new List<double>();

            foreach (IGrouping<string, Expense> group in grouping)
            {
                list.Add(group.Sum(e => e.Value));
            }
            return list;
        }

        public double[] StatsExpenses()
        {
            var statisticsExpenses = new double[3];
            statisticsExpenses[0] = _context.Expenses.Count();
            statisticsExpenses[1] = _context.Expenses.Max(e => e.Value);
            statisticsExpenses[2] = _context.Expenses.Min(e => e.Value);
            return statisticsExpenses;
        }

        public Month CurrentMonth()
        {
            int month = DateTime.Now.Month;
            return  _context.Months.Where(m => m.Id == month).FirstOrDefault();
        }
    }
}

