﻿using System.ComponentModel.DataAnnotations;

namespace ExpensesManager.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public int MonthId { get; set; }
        public Month Month { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(250, ErrorMessage = "utilize até {1} caracteres")]
        public string Description { get; set; }

        public int ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Valor")]
        [Range(0.0, double.MaxValue, ErrorMessage = "valor inválido")]
        public double Value { get; set; }
    }
}
