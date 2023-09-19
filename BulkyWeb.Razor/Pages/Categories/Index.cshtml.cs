using BulkyWeb.Razor.Data;
using BulkyWeb.Razor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public List<Category> CategoryList { get; set; }

        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void OnGet()
        {
            CategoryList = _dbContext.Categories.ToList();
        }
    }
}
