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
    public class IncomeTypeService
    {
        private readonly Context _context;

        public IncomeTypeService(Context context)
        {
            _context = context;
        }

        // Listar todos:
        public async Task<List<IncomeType>> FindAllAsync()
        {
            return await _context.IncomesTypes.ToListAsync();
        }

        // Buscar por Id:
        public async Task<IncomeType> FindByIdAsync(int? id)
        {
            return await _context.IncomesTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        // INSERIR:
        public async Task InsertAsync(IncomeType obj)
        {
            _context.IncomesTypes.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Atualizar:
        public async Task UpdateAsync(IncomeType obj)
        {
            bool hasAny = await _context.IncomesTypes.AnyAsync(t => t.Id == obj.Id);
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
                var obj = await _context.IncomesTypes.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível exluir pois existem receitas deste tipo cadastradas");
            }

        }

        // Obj Exists
        public async Task<bool> ObjExists(string name)
        {
            if (await _context.IncomesTypes.AnyAsync(e => e.Name.ToUpper() == name.ToUpper()))
                return true;
            return false;
        }

        // Search
        public async Task<List<IncomeType>> Search(string s)
        {
            return await _context.IncomesTypes.Where(e => e.Name.ToUpper().Contains(s.ToUpper())).ToListAsync();
        }
    }
}
