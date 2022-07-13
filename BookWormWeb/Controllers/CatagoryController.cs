using BookWorm.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWormWeb.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CatagoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Catagory> objCatagoryList = _db.Catagories;
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
                _db.Catagories.Add(obj);
                _db.SaveChanges();
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
            var catagoryFromDb = _db.Catagories.Find(id);
            //var catagoryFromFirst = _db.Catagories.FirstOrDefault(u => u.Id == id);
            //var catagoryFromSingle = _db.Catagories.SingleOrDefault(u => u.Id == id);

            if (catagoryFromDb == null)
            {
                return NotFound();
            }
            return View(catagoryFromDb);
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
                _db.Catagories.Update(obj);
                _db.SaveChanges();
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
            var catagoryFromDb = _db.Catagories.Find(id);
            //var catagoryFromFirst = _db.Catagories.FirstOrDefault(u => u.Id == id);
            //var catagoryFromSingle = _db.Catagories.SingleOrDefault(u => u.Id == id);

            if (catagoryFromDb == null)
            {
                return NotFound();
            }
            return View(catagoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Catagories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Catagories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Catagory deleted successfully";
            return RedirectToAction("Index");
        }
    } 
}
}
