using System.ComponentModel.DataAnnotations;

namespace ExpensesManager.Models
{
    public class Income
    {
        public int Id { get; set; }

        [Display(Name = "Mês")]
        public int MonthId { get; set; }

        [Display(Name = "Mês")]
        public Month Month { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(250, ErrorMessage = "utilize até {1} caracteres")]
        public string Description { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "é preciso cadastrar um tipo de receita")]
        public int IncomeTypeId { get; set; }

        [Display(Name = "Tipo")]
        public IncomeType IncomeType { get; set; }

        [Required(ErrorMessage = "campo obrigatório")]
        [Display(Name = "Valor")]
        [Range(0.1, double.MaxValue, ErrorMessage = "valor inválido")]
        [DataType(DataType.Currency)]
        public double Value { get; set; }
    }
}
