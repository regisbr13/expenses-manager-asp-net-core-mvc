using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpensesManager.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage ="campo obrigatório")]
        [StringLength(50, ErrorMessage ="use até {1} caracteres")]
        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
