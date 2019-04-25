using ExpensesManager.Data;
using ExpensesManager.Models;
using ExpensesManager.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Services
{
    public class ExpenseTypeService
    {
        private readonly Context _context;

        public ExpenseTypeService(Context context)
        {
            _context = context;
        }

        // Listar todos:
        public async Task<List<ExpenseType>> FindAllAsync()
        {
            return await _context.ExpensesTypes.ToListAsync();
        }

        // Buscar por Id:
        public async Task<ExpenseType> FindByIdAsync(int? id)
        {
            return await _context.ExpensesTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        // INSERIR:
        public async Task InsertAsync(ExpenseType obj)
        {
            _context.ExpensesTypes.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Atualizar:
        public async Task UpdateAsync(ExpenseType obj)
        {
            bool hasAny = await _context.ExpensesTypes.AnyAsync(t => t.Id == obj.Id);
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
            try
            {
                var obj = await _context.ExpensesTypes.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Não é possível exluir pois existem despesas deste tipo cadastradas");
            }

        }

        // Obj Exists
        public async Task<bool> ObjExists(string name)
        {
            if (await _context.ExpensesTypes.AnyAsync(e => e.Name.ToUpper() == name.ToUpper()))
                return true;
            return false;
        }

        // Search
        public async Task<List<ExpenseType>> Search(string s)
        {
            return await _context.ExpensesTypes.Where(e => e.Name.ToUpper().Contains(s.ToUpper())).ToListAsync();
        }
    }
}
