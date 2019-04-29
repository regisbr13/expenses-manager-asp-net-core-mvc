using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public double[] StatisticsExpenses { get; set; }
        public double[] StatisticsIncomes { get; set; }
    }
}
