using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Models.ViewModels
{
    public class GraphicsViewModel
    {
        public ICollection<Income> Incomes { get; set; }

        public ICollection<Expense> Expenses { get; set; }

        public double MonthlyIncome(int id)
        {
            return Incomes.Where(i => i.MonthId == id).Sum(i => i.Value);
        }

        public double MonthlyExpense(int id)
        {
            return Expenses.Where(i => i.MonthId == id).Sum(i => i.Value);
        }

        public double MonthlyTotal(int id)
        {
            return MonthlyIncome(id) + MonthlyExpense(id);
        }
    }
}
