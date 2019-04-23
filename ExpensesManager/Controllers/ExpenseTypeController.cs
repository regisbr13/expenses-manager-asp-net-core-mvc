using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesManager.Models;
using ExpensesManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesManager.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ExpenseTypeService _expenseTypeService;

        public ExpenseTypeController(ExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        // GET:
        public async Task<IActionResult> Index()
        {
            return View(await _expenseTypeService.FindAllAsync());
        }

        // Remote Validation
        public async Task<JsonResult> ExpenseTypeExist(string Name)
        {
            if (await _expenseTypeService.ObjExists(Name))
                return Json("Tipo de despesa já cadastrado.");
            return Json(true);
        }

        // GET:
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _expenseTypeService.FindByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // CREATE GET:
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Tipo de despesa " + expenseType.Name + " criado com sucesso.";
                await _expenseTypeService.InsertAsync(expenseType);
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

            var expenseType = await _expenseTypeService.FindByIdAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // Post:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseType obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["confirm"] = "Tipo de despesa " + obj.Name + " atualizado com sucesso.";
                await _expenseTypeService.UpdateAsync(obj);
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

            var obj = await _expenseTypeService.FindByIdAsync(id);
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
            ExpenseType obj = await _expenseTypeService.FindByIdAsync(id);
            TempData["confirm"] = "Tipo de despesa " + obj.Name + " excluído com sucesso.";
            await _expenseTypeService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}