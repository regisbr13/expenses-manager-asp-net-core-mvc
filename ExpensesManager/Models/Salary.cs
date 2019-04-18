using System.ComponentModel.DataAnnotations;

namespace ExpensesManager.Models
{
    public class Salary
    {
        public int Id { get; set; }

        public int MonthId { get; set; }
        public Month Month { get; set; }

        [Required(ErrorMessage ="campo obrigatório")]
        [Display(Name ="Valor")]
        [Range(0.0, double.MaxValue, ErrorMessage ="valor inválido")]
        public double Value { get; set; }
    }
}
