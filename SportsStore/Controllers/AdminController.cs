using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    [Authorize] // dang nhap moi dung duoc va bao ve lop AdminController
    public class AdminController : Controller
    {
        private IProductRepository repository;
         
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Products); //load san pham 

        public ViewResult Edit(int ProductID) =>
            View(repository.Products.FirstOrDefault(p => p.ProductID == ProductID)); //tìm sản phẩm có ID tương ứng với tham số productId và
                                                                                     //chuyển nó dưới dạng một đối tượng mô hình khung nhìn cho phương thức View.

        [HttpPost]
        public IActionResult Edit(Product product)  // IActionResult dùng để giữ lại dữ liệu khi thay đổi trong product 
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved"; 
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }
        public ViewResult Create() => View("Edit", new Product());  //load product moi(neu co) va load lai trang  

        [HttpPost]
        public IActionResult Delete (int ProductID)
        {
            Product deleteProduct = repository.DeleteProduct(ProductID);
            if(deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was deleted";
;            }
            return RedirectToAction("Index");
        }
    }
}
