using Core.Entities;
using Core.Interface;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> repository) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductSpecification(productSpecParams);

            return await CreatePageResult(repository, spec, productSpecParams.PageIndex, productSpecParams.PageSize);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repository.AddAsync(product);
            if( await repository.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id)) return NotFound();

            repository.UpdateAsync(product);
            if(await repository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            repository.DeleteAsync(product);
            if(await repository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
        {
            // return Ok(await repository.GetBrandsAsync());

            var spec = new BrandListSpecification();
            return Ok(await repository.ListAsync(spec));
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
        {
            //return Ok(await repository.GetTypesAsync());
            var spec = new TypeListSpecification();
            return Ok(await repository.ListAsync(spec));
        }

        #region Private Methods
        private bool ProductExists(int id)
        {
            return repository.Exist(id);
        }
        #endregion
    }
}
