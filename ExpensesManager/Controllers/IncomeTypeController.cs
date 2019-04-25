using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesManager.Models;
using ExpensesManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesManager.Controllers
{
    public class IncomeTypeController : Controller
    {
        private readonly IncomeTypeService _incomeTypeService;

        public IncomeTypeController(IncomeTypeService incomeTypeService)
        {
            _incomeTypeService = incomeTypeService;
        }

        // GET:
        [HttpGet("/Tipos-de-receita")]
        public async Task<IActionResult> Index()
        {
            var list = await _incomeTypeService.FindAllAsync();
            return View(list.OrderBy(obj => obj.Name));
        }

        [HttpPost("/Tipos-de-receita")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string txtSearch)
        {
            if (!String.IsNullOrEmpty(txtSearch))
            {
                return View(await _incomeTypeService.Search(txtSearch));
            }
            return RedirectToAction(nameof(Index));
        }

        // Remote Validation
        public async Task<JsonResult> IncomeTypeExist(string Name)
        {
            if (await _incomeTypeService.ObjExists(Name))
                return Json("Tipo de receita já cadastrado.");
            return Json(true);
        }

        // CREATE GET:
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeType expenseType)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Tipo de receita " + expenseType.Name + " cadastrado com sucesso.";
                await _incomeTypeService.InsertAsync(expenseType);
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        // EDIT GET:
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _incomeTypeService.FindByIdAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IncomeType obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Tipo de receita " + obj.Name + " atualizado com sucesso.";
                await _incomeTypeService.UpdateAsync(obj);
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

            var obj = await _incomeTypeService.FindByIdAsync(id);
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
            IncomeType obj = await _incomeTypeService.FindByIdAsync(id);
            TempData["confirm"] = "Tipo de receita " + obj.Name + " excluído com sucesso.";
            await _incomeTypeService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
