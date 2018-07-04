using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesV1.Models;

namespace SalesV1.Controllers
{
    interface IStore
    {
        ActionResult Index();
        ActionResult Edit(int id);
        ActionResult Edit(StoreViewModel model);
        ActionResult Create();
        ActionResult Create(StoreViewModel model);
        ActionResult Delete(int id);
    }

    public class StoreController : Controller, IStore
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Store
        public ActionResult Index()
        {
            var stores = db.Stores.Select(x => new StoreViewModel()
            {
                Id = x.Id,
                StoreName = x.StoreName,
                StoreAddress = x.StoreAddress
            });
            return View(stores);
        }

        public ActionResult Edit(int id)
        {
            var store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            } else
            {
                return View(new StoreViewModel()
                    { Id = store.Id,
                    StoreName = store.StoreName,
                    StoreAddress = store.StoreAddress
               });
            }
        }

        [HttpPost]
        public ActionResult Edit(StoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var store = db.Stores.Find(model.Id);
                store.StoreName = model.StoreName;
                store.StoreAddress = model.StoreAddress;
                db.SaveChanges();
                TempData["Message"] = "Changes have been saved.";
                return RedirectToAction("Index");
            } else
            {
                TempData["Message"] = "Unable to save changes. Contact the administrator.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (StoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var store = new Store();
                store.StoreName = model.StoreName;
                store.StoreAddress = model.StoreAddress;
                db.Stores.Add(store);
                db.SaveChanges();
                TempData["Message"] = "New record has been created.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Unable to create Store. Contact the administrator.";
                return RedirectToAction("Index");
            }
  
        }

        public ActionResult Delete(int id)
        {
            var store = db.Stores.Find(id);
            if (store.Sales.Any())
            {
                TempData["Message"] = "Unable to delete record. Check this Store is not used by a Sale record.";
                return RedirectToAction("Index");
            }
            else
            {
                db.Stores.Remove(store);
                db.SaveChanges();
                TempData["Message"] = "Record has been deleted";
                return RedirectToAction("Index");
            }
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

 