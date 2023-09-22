using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll();

            return View(products);
        }

        public IActionResult Create()
        {
            var vm = new ProductVM
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                    new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    })
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ProductVM vm)
        {
            if (vm.Product.Price100 > vm.Product.Price50)
            {
                ModelState.AddModelError("", "Price 50+ should be higher than price 100+.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(vm.Product);
                _unitOfWork.Save();

                TempData["success"] = "Product created successfully";

                return RedirectToAction("Index");
            }
            else
            {
                vm.CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                    new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });

                TempData["error"] = "Create product failed";


                return View(vm);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _unitOfWork.Product.Get(id.Value);
            if (objFromDb == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            ViewData["CategoryList"] = categoryList;

            return View(objFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Product updated successfully";

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

            Product? objFromDb = _unitOfWork.Product.Get(id.Value);
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
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? obj = _unitOfWork.Product.Get(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product removed successfully";

            return RedirectToAction("Index");
        }
    }
}
