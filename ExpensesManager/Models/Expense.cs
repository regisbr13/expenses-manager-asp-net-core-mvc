using System.ComponentModel.DataAnnotations;

namespace ExpensesManager.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Display(Name = "Mês")]
        public int MonthId { get; set; }

        [Display(Name = "Mês")]
        public Month Month { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(250, ErrorMessage = "utilize até {1} caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "é preciso cadastrar um tipo de despesa")]
        [Display(Name = "Tipo")]
        public int ExpenseTypeId { get; set; }

        [Display(Name = "Tipo")]
        public ExpenseType ExpenseType { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Valor")]
        [Range(0.1, double.MaxValue, ErrorMessage = "valor inválido")]
        [DataType(DataType.Currency)]
        public double Value { get; set; }
    }
}
