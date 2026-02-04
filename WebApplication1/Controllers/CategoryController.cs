namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Model;

    /// <summary>
    /// Defines the <see cref="CategoryController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Defines the categories
        /// </summary>
        public static List<Category> categories = new List<Category>
        {
            new Category(){Name="Electronics"},
            new Category(){  Name="Books"},
            new Category(){  Name="Clothing"},
        };

        //GET

        /// <summary>
        /// The GetCategories
        /// </summary>
        /// <returns>The <see cref="List{Category}"/></returns>
        [HttpGet]
        public List<Category> GetCategories()
        {
            return categories;
        }

        //POST

        /// <summary>
        /// The AddCategory
        /// </summary>
        /// <param name="category">The category<see cref="Category"/></param>
        /// <returns>The <see cref="List{Category}"/></returns>
        [HttpPost]
        public List<Category> AddCategory([FromBody] string categoryName)
        {
            Category newCategory = new Category { Name = categoryName };
            categories.Add(newCategory);
            return categories;
        }

        //Put

        /// <summary>
        /// The UpdateCategory
        /// </summary>
        /// <param name="index">The index<see cref="int"/></param>
        /// <param name="newCategory">The newCategory<see cref="Category"/></param>
        /// <returns>The <see cref="List{Category}"/></returns>
        [HttpPut]
        public List<Category> UpdateCategory([FromQuery] int index, [FromBody] Category newCategory)
        {
            categories[index] = newCategory;
            return categories;
        }

        //Delete

        /// <summary>
        /// The DeleteCategory
        /// </summary>
        /// <param name="category">The category<see cref="Category"/></param>
        /// <returns>The <see cref="List{Category}"/></returns>
        [HttpDelete]
        public List<Category> DeleteCategory([FromBody] Category category)
        {
            categories.RemoveAll(c => c.Name == category.Name);
            return categories;
        }
    }
}
