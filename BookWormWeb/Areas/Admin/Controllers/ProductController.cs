
using BookWorm.DataAccess;
using BookWorm.DataAccess.Repository;
using BookWorm.DataAccess.Repository.IRepository;
using BookWorm.Models;
using BookWorm.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWormWeb.Areas.Admin.Controllers;

public class ProductController : Controller
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    public ProductController(IUnitOfWork UnitOfWork , IWebHostEnvironment hostEnvironment)
    {
        _UnitOfWork = UnitOfWork;
        _hostEnvironment = hostEnvironment;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _UnitOfWork.CoverType.GetAll();
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

            productVM.Product = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
            //Update Product
        }

        
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"Images\Products");
                var extension = Path.GetExtension(file.FileName);

                if(obj.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                obj.Product.ImageUrl = @"\Images\Products\" + fileName + extension;
            }

            if(obj.Product.Id == 0)
            {
                _UnitOfWork.Product.Add(obj.Product);
            }
            else
            {
                _UnitOfWork.Product.Update(obj.Product);

            }

            _UnitOfWork.Save();
            TempData["success"] = "Product Added successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }



    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _UnitOfWork.Product.GetAll(includeProperties: "Catagory,CoverType");
        return Json(new {data = productList});
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new { success= false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _UnitOfWork.Product.Remove(obj);
        _UnitOfWork.Save();
        return Json(new { success= true, message = "Delete Successful" });
    }
    #endregion
}



