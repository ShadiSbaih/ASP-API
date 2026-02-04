namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Model;

    /// <summary>
    /// Defines the <see cref="ProductController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Defines the products
        /// </summary>
        public static List<Product> products = new List<Product>
        {
            new Product { Name = "Product A", Price = 10.0m },
            new Product { Name = "Product B", Price = 20.0m },
            new Product { Name = "Product C", Price = 30.0m }
        };

        //Get
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>The <see cref="List{Product}"/></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        // Post
        /// <summary>
        /// The AddProduct
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <param name="price">The price<see cref="decimal"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPost]
        public IActionResult AddProduct(string name, decimal price)
        {
            if (string.IsNullOrEmpty(name) || price <= 0)
            {
                return BadRequest("Invalid product data.");
            }

            var product = new Product
            {
                Name = name,
                Price = price
            };
            products.Add(product);

            return Created();
        }

        // Put
        /// <summary>
        /// The UpdateProduct
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <param name="newName">The newName<see cref="string"/></param>
        /// <param name="newPrice">The newPrice<see cref="decimal"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpPut]
        public IActionResult UpdateProduct(Guid id, string newName, decimal newPrice)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            product.Name = newName;
            product.Price = newPrice;
            return Ok(products);
        }

        // Delete
        /// <summary>
        /// The DeleteProduct
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            products.Remove(product);
            return Ok(products);
        }
    }
}
