using BulkyWeb.Razor.Data;
using BulkyWeb.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Razor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public Category Category { get; set; }

        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
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
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(Category);
                _dbContext.SaveChanges();

                //TempData["success"] = "Category updated successfully";

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
