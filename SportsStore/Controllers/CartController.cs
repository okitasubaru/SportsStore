using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller 
    {
        private IProductRepository repository; // kết nối cơ sở dữ liệu 
        private Cart cart;
        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel //Index lấy đối tượng Cart và sử dụng nó để tạo CartIndexView
            {                                  //cuối cùng để hiển thị nội dung của giỏ hàng
                Cart = cart, 
                ReturnUrl = returnUrl 
            });
        }
        
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
            
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //Cart cart = GetCart();
                cart.AddItem(product, 1);
               // SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl }); // RedirectToActionResult là trả về cái đường dẫn là returnURL là return lại chính giỏ hàng 
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                    .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //Cart cart = GetCart();
                cart.RemoveLine(product);
                //SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl }); // RedirectToActionResult là trả về cái đường dẫn là returnURL là return lại chính giỏ hàng 
        }
        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}
        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}
    }
}
