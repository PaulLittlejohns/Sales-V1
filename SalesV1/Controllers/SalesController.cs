using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesV1.Models;

namespace SalesV1.Controllers
{
    interface ISales
    {
        ActionResult Index();
        ActionResult Edit(int id);
        ActionResult Edit(Sale model);
        ActionResult Create();
        ActionResult Create(SalesViewModel model);
        ActionResult Delete(int id);
    }

    public class SalesController : Controller, ISales
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Sales
        public ActionResult Index()
        {
            var totalsales = db.SalesViewModel.Select(x => new TotalSaleViewModel
            {
                Id = x.Id,
                ProductName = x.Product.ProductName,
                CustomerName = x.Customer.CustomerName,
                StoreName = x.Store.StoreName,
                SaleDate = x.SaleDate
            });
            return View(totalsales);
        }

        public ActionResult Edit(int id)
        {
            var getProductList = db.Products.ToList();
            var getCustomerList = db.Customers.ToList();
            var getStoreList = db.Stores.ToList();

            int productid, customerid, storeid;
            DateTime saledate;

            var sales = db.SalesViewModel.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            else
            {
                productid = sales.ProductId;
                customerid = sales.CustomerId;
                storeid = sales.StoreId;
                saledate = sales.SaleDate;

                SelectList productlist = new SelectList(getProductList, "Id", "ProductName");
                SelectList customerlist = new SelectList(getCustomerList, "Id", "CustomerName");
                SelectList storelist = new SelectList(getStoreList, "Id", "StoreName");
                return View(new SalesViewModel
                {
                    ProductList = productlist,
                    ProductId = productid,
                    CustomerList = customerlist,
                    CustomerId = customerid,
                    StoreList = storelist,
                    StoreId = storeid,
                    SaleDate = saledate
                });
            }  
        }

        [HttpPost]
        public ActionResult Edit(Sale model)
        {
            var sale = db.SalesViewModel.Find(model.Id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            else
            {
                sale.ProductId = model.ProductId;
                sale.CustomerId = model.CustomerId;
                sale.StoreId = model.StoreId;
                sale.SaleDate = model.SaleDate;
                db.SaveChanges();
                TempData["Message"] = "Saved changes";
                return RedirectToAction("Index");
            } 
        }

        public ActionResult Create()
        {
            var getProductList = db.Products.ToList();
            var getCustomerList = db.Customers.ToList();
            var getStoreList = db.Stores.ToList();
            int productid = 0, customerid = 0, storeid = 0;
            string firstsel = "--- Please Select ---";
            getProductList.Add(new Product() { Id = productid, ProductName = firstsel });
            getCustomerList.Add(new Customer() { Id = customerid, CustomerName = firstsel });
            getStoreList.Add(new Store() { Id = storeid, StoreName = firstsel });
            SelectList productlist = new SelectList(getProductList, "Id", "ProductName");
            SelectList customerlist = new SelectList(getCustomerList, "Id", "CustomerName");
            SelectList storelist = new SelectList(getStoreList, "Id", "StoreName");
            return View(new SalesViewModel
            {
                ProductList = productlist,
                CustomerList = customerlist,
                StoreList = storelist,
                SaleDate = DateTime.Today
            });
        }

        [HttpPost]
        public ActionResult Create(SalesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sale = new Sale
                {
                    ProductId = model.ProductId,
                    CustomerId = (int)model.CustomerId,
                    StoreId = (int)model.StoreId,
                    SaleDate = model.SaleDate
                };
                db.SalesViewModel.Add(sale);
                db.SaveChanges();
                TempData["Message"] = "New record has been created.";
                return RedirectToAction("Index");
            } else
            {
                TempData["Message"] = "Unable to create Sale. Contact the administrator.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            var sale = db.SalesViewModel.Find(id);
            db.SalesViewModel.Remove(sale);
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
