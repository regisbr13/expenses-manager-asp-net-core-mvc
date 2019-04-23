﻿using ExpensesManager.Data;
using ExpensesManager.Models;
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
            var obj = await _context.ExpensesTypes.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();

        }

    }
}
