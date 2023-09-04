
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductRepository _repo;
        
        public ProductsController(IProductRepository Repo)
        {
            _repo = Repo;
          
            
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Getproducts()
        {
            var productList = await _repo.GetProductsAsync();

            return  Ok(productList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Getproduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            return product;
        }

         [HttpGet("Brands")]
         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands ()
         {
            return Ok( await _repo.GetProductBrandsAsync());
         }
         [HttpGet("Types")]
         public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes ()
         {
            return Ok( await _repo.GetProductTypesAsync());
         }

        
       
    }
    
}