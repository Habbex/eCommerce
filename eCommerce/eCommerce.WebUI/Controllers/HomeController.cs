using eCommerce.Contracts.Repositories;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<Basket> baskets;
        IRepositoryBase<Voucher> vouchers;
        IRepositoryBase<VoucherType> voucherTypes;
        IRepositoryBase<BasketVoucher> basketVouchers;
        IRepositoryBase<BasketItem> basketItems;
        IRepositoryBase<BasketVoucher> BasketVoucherId;


        BasketService basketService;

        public HomeController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products,
            IRepositoryBase<Basket> baskets, IRepositoryBase<Voucher> vouchers,
            IRepositoryBase<BasketVoucher> basketVouchers, IRepositoryBase<VoucherType> voucherTypes,
            IRepositoryBase<BasketItem> basketItems, IRepositoryBase<BasketVoucher> BasketVoucherId)

        {
            this.customers = customers;
            this.products = products;
            this.baskets = baskets;
            this.vouchers = vouchers;
            this.basketVouchers = basketVouchers;
            this.voucherTypes = voucherTypes;
            this.basketItems = basketItems;
            this.BasketVoucherId = BasketVoucherId; //<-


            basketService = new BasketService(this.baskets, this.vouchers,
                this.basketVouchers, this.voucherTypes, this.basketItems, this.BasketVoucherId);
        }
        public ActionResult BasketSummary() {
            var model = basketService.GetBasket(this.HttpContext);

            return View(model);
        }


        public ActionResult AddToBasket(int id) {
            basketService.AddToBasket(this.HttpContext, id, 1);//always add one to the basket

            return RedirectToAction("Index");
        }

        public ActionResult DeleteItem(int id)

        {

            basketService.DeleteItem(this.HttpContext, id, 1);
            basketService.DeleteToBasket(this.HttpContext, id);

            return RedirectToAction("BasketSummary");

        }

        public ActionResult AddBasketVoucher(string voucherCode) {
            basketService.AddVoucher(voucherCode, this.HttpContext);

            return RedirectToAction("BasketSummary");
        }

        public ActionResult DeleteVoucherFromBasket(int id)
        {
            basketService.DeleteToVoucherBasket(this.HttpContext, id);

            return RedirectToAction("BasketSummary");
        }



        public ActionResult Index()
        {
            var productList = products.GetAll();

            return View(productList);
        }

        public ActionResult Details(int id) {
            var product = products.GetById(id);

            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
<<<<<<< HEAD
<<<<<<< HEAD

        public ActionResult Portfolio()
        {
            ViewBag.Message = "Portfolio";
            return View();
        }


=======
>>>>>>> parent of fd6f70f... Added sketchfab embad link to my 3d model, good for the portfolio later on. Also renamed a page to portfolio to later on become the main page of my website.
=======
>>>>>>> parent of fd6f70f... Added sketchfab embad link to my 3d model, good for the portfolio later on. Also renamed a page to portfolio to later on become the main page of my website.
    }
}