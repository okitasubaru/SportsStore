using Microsoft.AspNetCore.Mvc;
using System.Linq; //Trong phương thức Invoke tôi sử dụng LINQ để chọn và sắp xếp bộ danh mục trong kho lưu trữ và chuyển
                   //chúng làm đối số cho phương thức View 
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {


        //public string Invoke() //Phương thức gọi được chèn vào HTML được gửi tới trình duyệt.
        // {
        //     return "Hello from the Nav View Component"; 
        //}
        private IProductRepository repository;
        public NavigationMenuViewComponent(IProductRepository repo)     //gọi danh sách product ra  
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];// nhận mục ngay slidebar đang chọn  và chỉ route để hiển thị đúng nội dung 
            return View(repository.Products  //load danh sách sản phẩm 
            .Select(x => x.Category)           // select gần giống sql  
            .Distinct()                        // loại trừ những cái lặp lại 
            .OrderBy(x => x));                   
        }
    }
}
