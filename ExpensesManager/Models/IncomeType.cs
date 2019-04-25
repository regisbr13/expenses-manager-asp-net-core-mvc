using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Models
{
    public class IncomeType
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "campo obrigatório")]
        [StringLength(50, ErrorMessage = "use até {1} caracteres")]
        [Remote("IncomeTypeExist", "IncomeType")]
        public string Name { get; set; }

        public ICollection<Income> Incomes { get; set; }
    }
}
