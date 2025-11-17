using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Core.DTOs;
using MyShop.Core.Entities;
using MyShop.Core.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Web.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropDownList();
                return View(categoryDto);
            }

            await _categoryService.CreateCategoryAsync(categoryDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryDto == null)
            {
                return NotFound();
            }

            var categoryForEdit = new CategoryCreateDto
            {
                Name = categoryDto.Name,
                ParentId = categoryDto.ParentId,
                OrderIndex = categoryDto.OrderIndex
            };

            await PopulateCategoriesDropDownList(categoryDto.ParentId);
            return View(categoryForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryCreateDto categoryDto)
        {
            if (id == categoryDto.ParentId)
            {
                ModelState.AddModelError("ParentId", "Danh mục không thể tự làm cha của chính nó.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropDownList(categoryDto.ParentId);
                return View(categoryDto);
            }

            var success = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCategoriesDropDownList(object? selectedCategory = null)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", selectedCategory);
        }
    }
}