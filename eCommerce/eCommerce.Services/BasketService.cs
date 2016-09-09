using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Contracts;
using eCommerce.Contracts.Modules;

namespace eCommerce.Services
{
    public class BasketService
    {
        IRepositoryBase<Basket> baskets;
        private IRepositoryBase<Voucher> vouchers;
        private IRepositoryBase<VoucherType> voucherTypes;
        private IRepositoryBase<BasketVoucher> basketVouchers;
        private IRepositoryBase<BasketItem> basketItems;


        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepositoryBase<Basket> baskets, IRepositoryBase<Voucher> vouchers,
            IRepositoryBase<BasketVoucher> basketVouchers, IRepositoryBase<VoucherType> voucherTypes,
            IRepositoryBase<BasketItem> basketItems)

        {
            this.baskets = baskets;
            this.vouchers = vouchers;
            this.basketVouchers = basketVouchers;
            this.voucherTypes = voucherTypes;
            this.basketItems = basketItems;

        }

        private Basket createNewBasket(HttpContextBase httpContext)
        {
            //create a new basket.

            //first create a new cookie.
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            //now create a new basket and set the creation date.
            Basket basket = new Basket();
            basket.date = DateTime.Now;
            basket.BasketId = Guid.NewGuid();

            //add and persist in the database.
            baskets.Insert(basket);
            baskets.Commit();

            //add the basket id to a cookie
            cookie.Value = basket.BasketId.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public bool AddToBasket(HttpContextBase httpContext, int productId, int quantity)
        {
            bool success = true;

            Basket basket = GetBasket(httpContext);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.BasketId,
                    ProductId = productId,
                    Quantity = quantity
                };
                basket.AddBasketItem(item);
            }
            else
            {
                item.Quantity = item.Quantity + quantity;
            }
            baskets.Commit();

            return success;
        }

        public Basket GetBasket(HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket;

            Guid basketId;

            if (cookie != null)
            {

                if(Guid.TryParse(cookie.Value, out basketId))
                {
                    basket = baskets.GetById(basketId);
                }
                else{
                    basket = createNewBasket(httpContext);
                }
            }
            else
            {
                basket = createNewBasket(httpContext);
            }

            return basket;
        }

        public void AddVoucher(string voucherCode, HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext);
            Voucher voucher = vouchers.GetAll().FirstOrDefault(v => v.VoucherCode == voucherCode);

            if (voucher != null)
            {
                VoucherType voucherType = voucherTypes.GetById(voucher.VoucherTypeId);
                if (voucher !=null)
                {
                    BasketVoucher basketVoucher = new BasketVoucher();

                    try
                    {
                        // modulen måste aktiveras igenom att använda de fullständiga namnet av classen o innhållande projekt
                        // alltså : fullständiga namnet av klassen ska innehålla namespace ->
                        // alltså : eCommerce.Modules.Vouchers.MoneyOff.eVoucher,eCommerce.Modules.Vouchers.MoneyOff

                        IeVoucher voucherProcessor = Activator.CreateInstance(Type.GetType(voucherType.VoucherModule)) as IeVoucher;
                        voucherProcessor.ProcessVoucher(voucher, basket, basketVoucher);
                    }
                    catch (Exception ex)
                    {
                        basketVoucher.VoucherDescription = ex.ToString();
                    }

                    baskets.Commit();
                }
            }

        }

        public bool DeleteToBasket(HttpContextBase httpContext, int basketItemId)

        {

            bool success = true;

            basketItems.Delete(basketItemId);

            basketItems.Commit();

            return success;

        }

        public bool DeleteItem(HttpContextBase httpContext, int basketItemId, int quantity)

        {

            bool success = true;

            BasketItem item = basketItems.GetById(basketItemId);

            if (item != null)

            {

                item.Quantity = item.Quantity - quantity;

            }

            basketItems.Commit();

            return success;

        }

        //public void MoneyOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        //{
        //    decimal basketTotal = basket.BasketTotal();
        //    if (voucher.MinSpend < basketTotal )
        //    {
        //        basketVoucher.Value = voucher.Value *-1;
        //        basketVoucher.VoucherCode = voucher.VoucherCode;
        //        basketVoucher.VoucherDescription = voucher.VoucherDescription;
        //        basketVoucher.VoucherId = voucher.VoucherId;
        //        basket.AddBasketVoucher(basketVoucher);
        //    }

        //}


        //public void PercentOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        //{
        //    if (voucher.MinSpend > basket.BasketTotal())
        //    {
        //        basketVoucher.Value = (voucher.Value * (basket.BasketTotal() / 100)) * -1;
        //        basketVoucher.VoucherCode = voucher.VoucherCode;
        //        basketVoucher.VoucherDescription = voucher.VoucherDescription;
        //        basketVoucher.VoucherId = voucher.VoucherId;
        //        basket.AddBasketVoucher(basketVoucher);
        //    }


        //}
    }
}
