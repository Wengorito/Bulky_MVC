using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var objCategoryList = _repository.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "Display Order cannot exactly match Name.");
            }

            if (ModelState.IsValid)
            {
                _repository.Add(obj);
                _repository.Save();

                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _repository.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(objFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(obj);
                _repository.Save();

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? objFromDb = _repository.Get(u => u.Id == id);
            //Category? objFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); 
            //Category? objFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            // 3 ways the same yield, but more filtering options than just id

            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(objFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _repository.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _repository.Remove(obj);
            _repository.Save();

            TempData["success"] = "Category removed successfully";

            return RedirectToAction("Index");
        }
    }
}
