using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesV1.Models;

namespace SalesV1.Controllers
{
    interface ICustomer
    {
        ActionResult Index();
        ActionResult Edit(int id);
        ActionResult Edit(CustomerViewModel model);
        ActionResult Create();
        ActionResult Create(CustomerViewModel model);
        ActionResult Delete(int id);
    }

    public class CustomerController : Controller, ICustomer
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var customers = db.Customers.Select(x => new CustomerViewModel {
                Id = x.Id,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress
            });
            return View(customers);
        }

        public ActionResult Edit(int id)
        {
            var customer = db.Customers.Find(id);
            if(customer == null)
            {
                return HttpNotFound();
            }

            return View(new CustomerViewModel()
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                CustomerAddress = customer.CustomerAddress
            });                
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(model.Id);                
                customer.CustomerName = model.CustomerName;
                customer.CustomerAddress = model.CustomerAddress;
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
        public ActionResult Create (CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                customer.CustomerName = model.CustomerName;
                customer.CustomerAddress = model.CustomerAddress;
                db.Customers.Add(customer);
                db.SaveChanges();
                TempData["Message"] = "New Customer has been created.";
                return RedirectToAction("Index");
            } else
            {
                TempData["Message"] = "Unable to create new record. Contact the administrator.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer.Sales.Any())
            {
                TempData["Message"] = "Unable to delete record. Check this Customer is not used by a Sale record.";
                return RedirectToAction("Index");
            }
            else
            {
                db.Customers.Remove(customer);
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