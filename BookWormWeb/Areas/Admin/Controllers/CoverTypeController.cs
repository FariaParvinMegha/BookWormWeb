
using BookWorm.DataAccess;
using BookWorm.DataAccess.Repository.IRepository;
using BookWorm.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWormWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CoverTypeController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _UnitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
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
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "CoverType created successfully";
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

            var CoverTypeFromDbFirst = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "CoverType edited successfully";
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
            
            var CoverTypeFromDbFirst = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.CoverType.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
