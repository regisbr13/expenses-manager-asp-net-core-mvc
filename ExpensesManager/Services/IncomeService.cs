﻿using ExpensesManager.Data;
using ExpensesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Services
{
    public class IncomeService
    {
        private readonly Context _context;

        public IncomeService(Context context)
        {
            _context = context;
        }

        // Listar todos:
        public async Task<List<Income>> FindAllAsync()
        {
            return await _context.Incomes.Include(i => i.Month).Include(i => i.IncomeType).OrderBy(i => i.MonthId).ToListAsync();
        }

        // Listar meses:
        public async Task<List<Month>> FindAllMonths()
        {
            return await _context.Months.ToListAsync();
        }

        public async Task<List<IncomeType>> FindAllIncomeType()
        {
            return await _context.IncomesTypes.ToListAsync();
        }

        // Buscar por Id:
        public async Task<Income> FindByIdAsync(int? id)
        {
            return await _context.Incomes.Include(i => i.Month).Include(i => i.IncomeType).FirstOrDefaultAsync(t => t.Id == id);
        }

        // INSERIR:
        public async Task InsertAsync(Income obj)
        {
            _context.Incomes.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Atualizar:
        public async Task UpdateAsync(Income obj)
        {
            bool hasAny = await _context.Incomes.AnyAsync(t => t.Id == obj.Id);
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
            var obj = await _context.Incomes.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();

        }

        // Search
        public async Task<List<Income>> Search(string s)
        {
            return await _context.Incomes.Include(i => i.Month).Include(i => i.IncomeType).Where(i => i.Description.ToUpper().Contains(s.ToUpper())).ToListAsync();
        }

        // Despesas por mês:
        public List<string> IncomesTypeByMonthId(int id)
        {
            var grouping = _context.Incomes.Where(e => e.MonthId == id).GroupBy(x => x.IncomeType.Name);
            var list = new List<string>();

            foreach (IGrouping<string, Income> group in grouping)
            {
                list.Add(group.Key.ToString());
            }
            return list;
        }

        public List<double> ValuesIncomesTypeByMonthId(int id)
        {
            var grouping = _context.Incomes.Where(e => e.MonthId == id).GroupBy(x => x.IncomeType.Name);
            var list = new List<double>();

            foreach (IGrouping<string, Income> group in grouping)
            {
                list.Add(group.Sum(e => e.Value));
            }
            return list;
        }
        public double[] StatsIncomes()
        {
            var statisticsIncomes = new double[3];
            statisticsIncomes[0] = _context.Incomes.Count();
            statisticsIncomes[1] = _context.Incomes.Max(e => e.Value);
            statisticsIncomes[2] = _context.Incomes.Min(e => e.Value);
            return statisticsIncomes;
        }

    }
}
