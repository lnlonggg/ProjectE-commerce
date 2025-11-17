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
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGroupService _groupService;
        private const int PageSize = 10;

        public ProductsController(IProductService productService, IGroupService groupService)
        {
            _productService = productService;
            _groupService = groupService;
        }

        public async Task<IActionResult> Index(string? search = null, int page = 1)
        {
            var pagedResult = await _productService.GetProductsAsync(page, PageSize, search);
            ViewBag.CurrentSearch = search;
            return View(pagedResult);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateGroupsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGroupsDropDownList();
                return View(productDto);
            }

            await _productService.CreateProductAsync(productDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var productDto = await _productService.GetProductByIdAsync(id);
            if (productDto == null)
            {
                return NotFound();
            }

            var productForEdit = new ProductCreateDto
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                IsActive = productDto.IsActive,
                GroupId = productDto.GroupId
            };

            await PopulateGroupsDropDownList(productDto.GroupId);
            return View(productForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGroupsDropDownList(productDto.GroupId);
                return View(productDto);
            }

            var success = await _productService.UpdateProductAsync(id, productDto);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateGroupsDropDownList(object? selectedGroup = null)
        {
            var groups = await _groupService.GetAllGroupsAsync();
            ViewBag.GroupList = new SelectList(groups, "Id", "Name", selectedGroup);
        }
    }
}