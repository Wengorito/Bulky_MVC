using BulkyWeb.Razor.Data;
using BulkyWeb.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BulkyWeb.Razor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Category Category { get; set; }

        private readonly ApplicationDbContext _dbContext;

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _dbContext.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            _dbContext.Categories.Remove(Category);
            _dbContext.SaveChanges();

            TempData["success"] = "Category deleted successfully";

            return RedirectToPage("Index");
        }
    }
}
