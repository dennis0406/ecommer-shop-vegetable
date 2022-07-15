using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanNongSan.Models
{
    public class CartItem
    {
        public San_pham shoppingProduct { get; set; }
        public int shopping_quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(San_pham pro, int quantity = 1)
        {
            var item = items.FirstOrDefault(s => s.shoppingProduct.ma_san_pham == pro.ma_san_pham);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    shoppingProduct = pro,
                    shopping_quantity = quantity

                });
            }
            else
            {
                item.shopping_quantity += 1;
            }
        }

        public void Update_Quantity (int id, int quantity)
        {
            var item = items.Find(s => s.shoppingProduct.ma_san_pham == id);
            if(item != null)
            {
                item.shopping_quantity = quantity;
            }
        }
        public double Total()
        {
            var total = items.Sum(s => s.shoppingProduct.gia * s.shopping_quantity);
            return (double)total;
        }

        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s.shoppingProduct.ma_san_pham == id);
        }


        public int Return_Quantity()
        {
            return items.Count;
        }
    }
}