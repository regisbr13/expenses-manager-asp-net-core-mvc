using ExpensesManager.Models.ViewModels;
using ExpensesManager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesManager.Models.ViewComponents
{
    public class StatisticsViewComponent : ViewComponent
    {
        private readonly ExpenseService _expenseService;
        private readonly IncomeService _incomeService;

        public StatisticsViewComponent(ExpenseService expenseService, IncomeService incomeService)
        {
            _expenseService = expenseService;
            _incomeService = incomeService;
        }

        public IViewComponentResult Invoke()
        {
            StatisticsViewModel viewModel = new StatisticsViewModel();

            viewModel.StatisticsExpenses = _expenseService.StatsExpenses();

            viewModel.StatisticsIncomes = _incomeService.StatsIncomes();

            return View(viewModel);
        }
    }
}
