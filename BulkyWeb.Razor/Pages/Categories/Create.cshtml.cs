using BulkyWeb.Razor.Data;
using BulkyWeb.Razor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        public Category Category { get; set; }

        private readonly ApplicationDbContext _dbContext;

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
        }
    }
}
