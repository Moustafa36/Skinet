
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;


namespace API.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:BaseApiController
    {
        // private readonly IProductRepository _repo;
        
        // public ProductsController(IProductRepository Repo)
        // {
        //     _repo = Repo;
          
            
        // }
        private readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandsRepo,
        IGenericRepository<ProductType> productTypesRepo , IMapper mapper)
        {
            _mapper = mapper;
            _productBrandsRepo = productBrandsRepo;
            _productsRepo = productsRepo;
            _productTypesRepo = productTypesRepo;
            
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> Getproducts()
        {
             var spec = new ProductsWithTypesAndBrandsSpecification();
            var productList = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(productList)) ;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> Getproduct(int id)
        {
           // var product = await _productsRepo.GetByIdAsync(id);
           var spec = new ProductsWithTypesAndBrandsSpecification(id);
           var product = await _productsRepo.GetEntityWithSpec(spec);
           if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

         [HttpGet("Brands")]
         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands ()
         {
            return Ok( await _productBrandsRepo.ListAllAsync());
         }
         [HttpGet("Types")]
         public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes ()
         {
            return Ok( await _productTypesRepo.ListAllAsync());
         }

        
       
    }
    
}