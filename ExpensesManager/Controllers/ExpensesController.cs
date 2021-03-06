﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesManager.Models;
using ExpensesManager.Models.ViewModels;
using ExpensesManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ExpensesManager.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseService _expenseService;

        public ExpensesController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET:
        [HttpGet("/Despesas")]
        public async Task<IActionResult> Index(int? page)
        {
            const int pageItems = 10;
            int pageNumber = (page ?? 1);
            var list = await _expenseService.FindAllAsync();
            return View(await list.ToPagedListAsync(pageNumber, pageItems));
        }

        [HttpPost("/Despesas")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string txtSearch)
        {
            if (!String.IsNullOrEmpty(txtSearch))
            {
                var list = await _expenseService.Search(txtSearch);
                return View(await list.ToPagedListAsync(1, 1));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET:
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _expenseService.FindByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // CREATE GET:
        public async Task<IActionResult> Create()
        {
            ViewBag.MonthId = new SelectList(await _expenseService.FindAllMonths(), "Id", "Name");
            ViewBag.ExpenseTypeId = new SelectList(await _expenseService.FindAllExpenseType(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> CreateCurrent()
        {
            ViewBag.ExpenseTypeId = new SelectList(await _expenseService.FindAllExpenseType(), "Id", "Name");
            return View();
        }

        // CREATE POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Despesa cadastrada com sucesso.";
                await _expenseService.InsertAsync(obj);
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCurrent(Expense obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Despesa cadastrada com sucesso.";
                await _expenseService.InsertAsync(obj);
                return RedirectToAction(nameof(CurrentStats));
            }
            return View(obj);
        }



        // EDIT GET:
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseService.FindByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewBag.MonthId = new SelectList(await _expenseService.FindAllMonths(), "Id", "Name");
            ViewBag.ExpenseTypeId = new SelectList(await _expenseService.FindAllExpenseType(), "Id", "Name");
            return View(expense);
        }

        // Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Despesa atualizada com sucesso.";
                await _expenseService.UpdateAsync(obj);
                if (obj.MonthId == DateTime.Now.Month)
                    return RedirectToAction(nameof(CurrentStats));
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // DELETE GET:
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _expenseService.FindByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // DELETE POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            TempData["confirm"] = "Despesa exluída com sucesso.";
            Expense obj = await _expenseService.FindByIdAsync(id);
            await _expenseService.RemoveAsync(id);
            if (obj.MonthId == DateTime.Now.Month)
                return RedirectToAction(nameof(CurrentStats));
            return RedirectToAction(nameof(Index));
        }

        // Graphics:
        [HttpGet("/Estatisticas")]
        public async Task<IActionResult> Graphics()
        {
            ViewBag.months = new SelectList(await _expenseService.ExpenseIncomeMonths(), "Id", "Name");
            return View();
        }

        public JsonResult MonthlyIncomesExpenses(int id)
        {
            var expenses = _expenseService.MonthlyExpenses(id);
            var incomes = _expenseService.MonthlyIncome(id);
            var balance = incomes - expenses;

            var obj = new { expenses, incomes, balance};
            return Json(obj);   
        }

        public JsonResult MonthlyExpensesType(int id)
        {
            var types = _expenseService.ExpensesTypeByMonthId(id);
            var values = _expenseService.ValuesExpensesTypeByMonthId(id);
            return Json(new {types, values});
        }

        public async Task<JsonResult> IncomesExpensesTotal()
        {
            var list = await _expenseService.ExpenseIncomeMonths();
            var months = list.Select(m => m.Name);
            double[] expenses = new double[months.Count()];
            double[] incomes = new double[months.Count()];

            for(int i = 1; i <= months.Count(); i++)
            {
                expenses[i - 1] = _expenseService.MonthlyExpenses(i);
                incomes[i - 1] = _expenseService.MonthlyIncome(i);
            }

            return Json(new { months, expenses, incomes});
        }

        public async Task<IActionResult> CurrentStats()
        {
            var month = DateTime.Now.Month;
            ViewBag.month = _expenseService.CurrentMonth().Name;
            ViewBag.incomes = await _expenseService.IncomesByMonth(month);
            ViewBag.expenses = await _expenseService.ExpensesByMonth(month);
            return View();
        }
    }
}
