using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Model;
using eCommerce.DAL.Data;

namespace eCommerce.DAL.Repositories
{
  public class BasketItemsRepository: RepositoryBase<BasketItem>
    {
        public BasketItemsRepository(DataContext context)
            :base(context)
        {
            if (context== null)
            {
                throw new ArgumentException();
            }
        }

    }
}
