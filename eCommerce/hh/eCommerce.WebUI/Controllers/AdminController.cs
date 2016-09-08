using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace eCommerce.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<VoucherType> voucherTypes;
        IRepositoryBase<Voucher> vouchers;

        public AdminController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products,IRepositoryBase<VoucherType> voucherTypes, IRepositoryBase<Voucher> vouchers)
        {
            this.customers = customers;
            this.products = products;
            this.vouchers = vouchers;
            this.voucherTypes = voucherTypes;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList() {
            var model = products.GetAll();

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = products.GetById(id);

            return View(product);
        }

        public ActionResult CreateProduct() {
            var model = new Product();

            return View(model);
        }

        public ActionResult VoucherList()
        {
            var model = vouchers.GetAll();

            return View(model);
        }

        public ActionResult CreateVoucher()
        {
            var model = new Voucher();
            ViewBag.voucherTypes = voucherTypes.GetAll();
            ViewBag.products = products.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateVoucher(Voucher voucher)
        {
            
            
            vouchers.Insert(voucher);
            vouchers.Commit();

            return RedirectToAction("VoucherList");
        }

        public ActionResult EditVoucher(int id)
        {
            Voucher voucher = vouchers.GetById(id);


            return View(voucher);
        }

        [HttpPost]
        public ActionResult EditVoucher(Voucher voucher)
        {
            vouchers.Update(voucher);
            vouchers.Commit();

            return RedirectToAction("VoucherList");
        }

        
        public ActionResult DeleteVoucher(int id)
        {

            Voucher Voucher = vouchers.GetById(id);
            vouchers.Delete(id);
            vouchers.Commit();
            return RedirectToAction("VoucherList");
        }



        public ActionResult VoucherTypeList() {
            var model = voucherTypes.GetAll();

            return View(model);
        }

        public ActionResult CreateVoucherType() {
            var model = new VoucherType();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateVoucherType(VoucherType voucherType)
        {

            voucherTypes.Insert(voucherType);
            voucherTypes.Commit();

            return RedirectToAction("VoucherTypeList");
        }

        public ActionResult EditVoucherType(int id)
        {
            VoucherType voucherType = voucherTypes.GetById(id);


            return View(voucherType);
        }

        [HttpPost]
        public ActionResult EditVoucherType(VoucherType voucherType)
        {
            voucherTypes.Update(voucherType);
            voucherTypes.Commit();

            return RedirectToAction("VoucherTypeList");
        }

       
        public ActionResult DeleteVoucherType(int id)
        {
            VoucherType voucherType = voucherTypes.GetById(id);
            voucherTypes.Delete(id);
            voucherTypes.Commit();
            return RedirectToAction("VoucherTypeList");
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {

            products.Insert(product);
            products.Commit();

            return RedirectToAction("ProductList");
        }

        public ActionResult EditProduct(int id)
        {
            Product product = products.GetById(id);


            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            products.Update(product);
            products.Commit();

            return RedirectToAction("ProductList");
        }

        
        public ActionResult DeleteProduct( int id) // Get 
        {
            Product product = products.GetById(id);
            products.Delete(id);
            products.Commit();
            return RedirectToAction("ProductList");

        }
        
       


        
    }
}