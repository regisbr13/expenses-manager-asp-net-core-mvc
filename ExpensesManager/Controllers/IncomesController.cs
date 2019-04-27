using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesManager.Models;
using ExpensesManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ExpensesManager.Controllers
{
    public class IncomesController : Controller
    {
        private readonly IncomeService _incomeService;

        public IncomesController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // GET:
        [HttpGet("/Receitas")]
        public async Task<IActionResult> Index(int? page)
        {
            const int pageItems = 10;
            int pageNumber = (page ?? 1);
            var list = await _incomeService.FindAllAsync();
            return View(await list.ToPagedListAsync(pageNumber, pageItems));
        }

        [HttpPost("/Receitas")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string txtSearch)
        {
            if (!String.IsNullOrEmpty(txtSearch))
            {
                var list = await _incomeService.Search(txtSearch);
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

            var obj = await _incomeService.FindByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // CREATE GET:
        public async Task<IActionResult> Create()
        {
            ViewBag.MonthId = new SelectList(await _incomeService.FindAllMonths(), "Id", "Name");
            ViewBag.IncomeTypeId = new SelectList(await _incomeService.FindAllIncomeType(), "Id", "Name");
            return View();
        }

        // CREATE POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Income obj)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Receita cadastrada com sucesso.";
                await _incomeService.InsertAsync(obj);
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

            var income = await _incomeService.FindByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            ViewBag.MonthId = new SelectList(await _incomeService.FindAllMonths(), "Id", "Name");
            ViewBag.IncomeTypeId = new SelectList(await _incomeService.FindAllIncomeType(), "Id", "Name");
            return View(income);
        }

        // Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Income obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Receita atualizada com sucesso.";
                await _incomeService.UpdateAsync(obj);
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

            var obj = await _incomeService.FindByIdAsync(id);
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
            TempData["confirm"] = "Receita exluída com sucesso.";
            await _incomeService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public JsonResult MonthlyIncomesType(int id)
        {
            var types = _incomeService.IncomesTypeByMonthId(id);
            var values = _incomeService.ValuesIncomesTypeByMonthId(id);
            return Json(new {types, values});
        }
    }
}