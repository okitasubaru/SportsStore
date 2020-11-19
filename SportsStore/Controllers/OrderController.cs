using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        [Authorize] // yeu cau dang nhap moi dung duoc 
        public ViewResult List() =>         // Phương thức List chọn tất cả các đối tượng Đặt hàng trong kho lưu trữ có giá trị Đã vận chuyển là false và
                                                //chuyển chúng sang dạng xem mặc định. và hiện danh sách cho admin 
                View(repository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        [Authorize] // yeu cau dang nhap moi dung duoc 
        public IActionResult MarkShipped(int orderID)   //Phương thức MarkShipped sẽ nhận yêu cầu POST chỉ định ID của một đơn đặt hàng,sử dụng để lấy Order object 
        {                                               //để thuộc tính Đã vận chuyển được lưu và đặt là true 
            Order order = repository.Orders
            .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View(new Order()); //Phương thức Checkout trả về chế độ xem mặc định và
                                                           //chuyển một đối tượng ShippingDetails mới làm chế độ xem mô hình
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
            public ViewResult Completed()
            {
                cart.Clear();
                return View();
            }
        }
    
    }

