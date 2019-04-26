using System;
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
            await _expenseService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Graphics:
        public async Task<IActionResult> Graphics()
        {
            ViewBag.months = new SelectList(await _expenseService.ExpenseIncomeMonths(), "Id", "Name");
            return View();
        }

        public async Task<JsonResult> MonthlyTotal(int id)
        {
            GraphicsViewModel graphic = await _expenseService.FindGraphic();
            var obj = new { expenses = graphic.MonthlyExpense(id), incomes = graphic.MonthlyIncome(id), total = graphic.MonthlyTotal(id) };
            return Json(obj);   
        }
    }
}
