using Business.Models.Product;
using Business.Services.Abstract;
using Common.Entities;
using Data.Repositories.Abstract;
using Data.UnitOfWork;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete;


public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly ModelStateDictionary _modelState;

    public ProductService(IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IActionContextAccessor actionContextAccessor
        )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _modelState = actionContextAccessor.ActionContext.ModelState;
    }
    public async Task<ProductIndexVM> GetAllAsync(ProductIndexVM model)
    {
        var products = await _productRepository.FilterByName(model.Name).ToListAsync(); // Fetch filtered products
        return new ProductIndexVM
        {
            Name = model.Name,
            Products = products
        };
    }
    public async Task<ProductIndexVM> GetAllAsync()
    {
        return new ProductIndexVM
        {
            Products = await _productRepository.GetAllAsync(),
            ProductTypes = await _productRepository.GetAllTypesAsync()

        };

    }
    public async Task<ProductIndexVM> GetLastThreeAsync()
    {
        return new ProductIndexVM
        {
            Products = await _productRepository.GetLastThreeAsync()
        };
    }
    public async Task<List<Product>> GetAllByTypeAsync(int typeId)
    {
        return await _productRepository.GetAllByTypeAsync(typeId);
    }

    public async Task<bool> CreateAsync(ProductCreateVM model)
    {
        var product = await _productRepository.GetByNameAsync(model.Name);
        if (product != null)
        {
            return false;
        }

        var productTypesList = await _productRepository.GetAllTypesAsync();
        model.ProductTypes = productTypesList.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        }).ToList();

        product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            StockQuantity = model.StockQuantity,
            Price = model.Price,
            PhotoName = model.PhotoName,
            ProductTypeId = model.ProductTypeId,
            CreatedDate = DateTime.Now,
        };

        await _productRepository.CreateAsync(product);
        await _unitOfWork.CommitAsync();
        return true;
    }
    public async Task<Product> GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return false; 
        }

        await _productRepository.DeleteAsync(product); 
        await _unitOfWork.CommitAsync();

        return true; 
    }
    public async Task<bool> UpdateAsync(Product product)
    {
       
        if (product == null)
        {
            return false;
        }
        await _productRepository.UpdateAsync(product);
        await _unitOfWork.CommitAsync();
        return true;

    }
    



}
