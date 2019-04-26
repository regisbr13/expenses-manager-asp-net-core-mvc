using ExpensesManager.Data;
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

        // Dados para o grafico:
        public async Task<GraphicsViewModel> FindGraphic()
        {
            GraphicsViewModel graphic = new GraphicsViewModel()
            {
                Expenses = await _context.Expenses.ToListAsync(),
                Incomes = await _context.Incomes.ToListAsync()
            };
            return graphic;
        }
    }
}

