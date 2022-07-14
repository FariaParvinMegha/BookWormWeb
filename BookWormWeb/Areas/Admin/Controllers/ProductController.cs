
using BookWorm.DataAccess;
using BookWorm.DataAccess.Repository.IRepository;
using BookWorm.Models;
using BookWorm.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWormWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objCoverTypeList = _UnitOfWork.Product.GetAll();
            return View(objCoverTypeList);
        }



        //EDIT
        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CatagoryList = _UnitOfWork.Catagory.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)  
            {
                //Create Product
                //ViewBag.CatagoryList = CatagoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                //Update Product
            }

            return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_UnitOfWork.Product.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Product edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //DELETE
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var ProductFromDbFirst = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (ProductFromDbFirst == null)
            {
                return NotFound();
            }
            return View(ProductFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
