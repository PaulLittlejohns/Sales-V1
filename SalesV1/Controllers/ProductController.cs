using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesV1.Models;

namespace SalesV1.Controllers
{
    interface IProduct
    {
        ActionResult Index();
        ActionResult Edit(int id);
        ActionResult Edit(ProductViewModel model);
        ActionResult Create();
        ActionResult Create(ProductViewModel model);
        ActionResult Delete(int id);
    }

    public class ProductController : Controller, IProduct
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Select(x => new ProductViewModel {
                Id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice
            });
            return View(products);
        }

        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice
            });
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(model.Id);
                product.ProductName = model.ProductName;
                product.ProductPrice = model.ProductPrice;
                db.SaveChanges();
                TempData["Message"] = "Product has been changed";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Unable to edit record. Contact the administrator";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.ProductName = model.ProductName;
                product.ProductPrice = model.ProductPrice;
                db.Products.Add(product);
                db.SaveChanges();
                TempData["Message"] = "New Product has been created";
                return RedirectToAction("Index");
            } else
            {
                TempData["Message"] = "Unable to create record. Contact the administrator";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            if (product.Sales.Any())
            {
                TempData["Message"] = "Unable to delete record. Check this Product is not used by a Sale record.";
                return RedirectToAction("Index");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            TempData["Message"] = "Record has been deleted";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}