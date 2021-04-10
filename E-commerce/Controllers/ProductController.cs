using E_commerce.Data;
using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_commerce.ViewModels;

namespace E_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext db)
        {
            _context = db;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public ActionResult SelectedCategory(int? id)
        {
            var products = _context.Products.Where(p => p.CategoryId == id).ToList();

            return View("Index", products);
        }

        public JsonResult GetCategory()
        {
            var categories = _context.Categories.ToList();

            return Json(new SelectList(categories, "Id", "Name"));
        }

        public ActionResult Create()
        {
            var _categories = _context.Categories.ToList();

            var viewModel = new ProductViewModel
            {
                Product = new Product(),
                Categories = _categories
            };

            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (product.Id > 0)
                _context.Entry(product).State = EntityState.Modified;
            else
                _context.Products.Add(product);
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return View("Create", product);
        }

        public ActionResult Delete(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            _context.Products.Remove(product);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
