using Business.Models.Product;
using Business.Services.Abstract;
using Business.Services.Concrete;
using Common.Entities;
using Data.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Areas.Admin.Models.Product;



namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductController(IProductService productService,
            IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index(ProductIndexVM model)
        {
            ProductIndexVM viewModel;

            if (model != null && !string.IsNullOrEmpty(model.Name))
            {
                // If Name is provided, filter products by Name
                viewModel = await _productService.GetAllAsync(model);
            }
            else
            {
                // If no Name is provided, return all products
                viewModel = await _productService.GetAllAsync();
            }

            return View(viewModel); // Pass the ProductIndexVM to the view
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productTypes = await _productRepository.GetAllTypesAsync();

            var model = new ProductCreateVM
            {
                ProductTypes = productTypes.Select(pt => new SelectListItem
                {
                    Value = pt.Id.ToString(),
                    Text = pt.Name
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var productTypes = await _productRepository.GetAllTypesAsync();
            model.ProductTypes = productTypes.Select(pt => new SelectListItem
            {
                Value = pt.Id.ToString(),
                Text = pt.Name
            }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSucceeded = await _productService.CreateAsync(model);
            if (!isSucceeded)
            {
                ModelState.AddModelError("Name", "Product with the same name already exists.");
                return View(model); // Return with error message
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion



        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDetailsVM = new ProductDetailsVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ProductType = product.ProductType.Name,

            };
            return View(productDetailsVM);
        }
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _productService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            var productTypes = await _productService.GetAllAsync();

            var productUpdateVM = new ProductUpdateVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                PhotoName = product.PhotoName,
                ProductTypes = productTypes.ProductTypes.Select(pt => new SelectListItem
                {
                    Value = pt.Id.ToString(),
                    Text = pt.Name
                }).ToList()
            };

            return View(productUpdateVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = await _productService.GetByIdAsync(model.Id);
            if (product == null) return NotFound();

            var productTypes = await _productRepository.GetAllTypesAsync();
            var productType = productTypes.FirstOrDefault(pt => pt.Id == model.ProductTypeId);

            if (productType is null) return NotFound();


            product.Name = model.Name;
            product.Price = model.Price;
            product.PhotoName = model.PhotoName;
            product.Description = model.Description;
            product.ProductTypeId = productType.Id;
            product.StockQuantity = model.StockQuantity;
            product.ModifiedAt = DateTime.Now;

            await _productService.UpdateAsync(product);

            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
