﻿
using System.Collections.Generic;
using System.Linq;


namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>(); 
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
            .Where(p => p.Product.ProductID == product.ProductID)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => //xóa sản phẩm đã chọn 
        lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public virtual decimal ComputeTotalValue() => //tính tổng tiền 
        lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear(); // reset giỏ hàng 
        public virtual IEnumerable<CartLine> Lines => lineCollection; //thuộc tính cho phép truy cập vào nội dung của giỏ hàng <cart line> 
    }
    public class CartLine ////dòng giỏ hàng 
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
