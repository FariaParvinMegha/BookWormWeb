
using BookWorm.DataAccess;
using BookWorm.DataAccess.Repository.IRepository;
using BookWorm.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWormWeb.Areas.Admin.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CatagoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Catagory> objCatagoryList = _UnitOfWork.Catagory.GetAll();
            return View(objCatagoryList);
        }


        //CREAT
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Catagory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Catagory.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Catagory created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //EDIT
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var catagoryFromDb = _db.Catagories.Find(id);
            var catagoryFromDbFirst = _UnitOfWork.Catagory.GetFirstOrDefault(u => u.Id == id);
            //var catagoryFromSingle = _db.Catagories.SingleOrDefault(u => u.Id == id);

            if (catagoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(catagoryFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Catagory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Catagory.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Catagory edited successfully";
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
            //var catagoryFromDb = _db.Catagories.Find(id);
            var catagoryFromDbFirst = _UnitOfWork.Catagory.GetFirstOrDefault(u => u.Id == id);
            //var catagoryFromSingle = _db.Catagories.SingleOrDefault(u => u.Id == id);

            if (catagoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(catagoryFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _UnitOfWork.Catagory.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Catagory.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Catagory deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
