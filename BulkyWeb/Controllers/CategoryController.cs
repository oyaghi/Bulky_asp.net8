using Bulky.Models;
using BulkyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
          
        {
            if (obj.Name.ToLower() == "hentai")
            {
                ModelState.AddModelError("name", "You can't add that here");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"]="Category Created Successfuly";

                return RedirectToAction("Index", "Category");
            }
           return View();
            
        }

      
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            Category? singleCategory = _db.Categories.Find(id);
            //Category? singleCategory2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? singleCategory3 = _db.Categories.Where(u=> u.Id==id).FirstOrDefault();

            if (singleCategory == null) {
                return NotFound();
            }
           
            
            return View(singleCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid) { 
            
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfuly";

                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            Category? singleCategory = _db.Categories.Find(id);
            

            if (singleCategory == null)
            {
                return NotFound();
            }


            return View(singleCategory);
        }
        [HttpPost,ActionName("Delete")] //by doing this you can call this method by just calling the name "Delete"
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null) {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index","Category");
            
           
           
        }
    }
}

