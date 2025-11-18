using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Core.DTOs;
using MyShop.Core.Entities;
using MyShop.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace MyShop.Web.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ICategoryService _categoryService;

        public GroupsController(IGroupService groupService, ICategoryService categoryService)
        {
            _groupService = groupService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return View(groups);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupCreateDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropDownList();
                return View(groupDto);
            }

            await _groupService.CreateGroupAsync(groupDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var groupDto = await _groupService.GetGroupByIdAsync(id);
            if (groupDto == null)
            {
                return NotFound();
            }

            var groupForEdit = new GroupCreateDto
            {
                Name = groupDto.Name,
                Description = groupDto.Description,
                CategoryId = groupDto.CategoryId
            };

            await PopulateCategoriesDropDownList(groupDto.CategoryId);
            return View(groupForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupCreateDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropDownList(groupDto.CategoryId);
                return View(groupDto);
            }

            var success = await _groupService.UpdateGroupAsync(id, groupDto);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _groupService.DeleteGroupAsync(id);
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