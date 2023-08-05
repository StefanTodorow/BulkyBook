﻿using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
            => _db = db;

        public IActionResult Index()
        {
            var categoryList = _db.Categories.ToList();

            return View(categoryList);
        }

        // GET
        public IActionResult Create() 
            => View();

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                TempData["success"] = "Category created successfuly.";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();

                TempData["success"] = "Category updated successfuly.";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
                return NotFound();

            _db.Categories.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Category deleted successfuly.";

            return RedirectToAction("Index");
        }
    }
}
