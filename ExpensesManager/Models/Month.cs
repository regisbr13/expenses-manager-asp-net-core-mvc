using System.Collections.Generic;

namespace ExpensesManager.Models
{
    public class Month
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }

        public Salary Salary { get; set; }
    }
}
