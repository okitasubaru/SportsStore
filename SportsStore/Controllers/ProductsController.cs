using System;
using System.Collections.Generic;
using System.Linq; // xử lí csdl úp lên hoặc trên csdl
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;


namespace SportsStore.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository repository; // su dung nhung ham tuong tac ben duoi 
        public int PageSize = 4; // mỗi trang có 4 sản phẩm 
        public ProductsController(IProductRepository repo) // khoi  tao bien repo
        {
            repository = repo;
        }
        //public ViewResult List() => View(repository.Products); // tra ve kieu ViewResult tra ve 1 cai View ten la list 
        // tu reposity Products từ class  IProductRepository

        public ViewResult List(string category, int productPage = 1)
          // => View(repository.Products
          // .OrderBy(p => p.ProductID)
          // .Skip((productPage - 1) * PageSize)
          // .Take(PageSize));

          => View(new ProductsListViewModel {
            Products = repository.Products // danh sách product 
                .Where(p => category == null || p.Category == category) 
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo {  // thông tin phân trang 
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
                TotalItems = category == null ? // kiểm tra tổng số trang 
                repository.Products.Count() :
                repository.Products.Where(e =>   //Nếu một danh mục đã được chọn, tôi trả lại số lượng mục trong danh mục đó; nếu không, tôi trả lại tổng số                
                e.Category == category).Count()   //số lượng sản phẩm.
           // TotalItems = repository.Products.Count() tổng số trang và kiểm tra đếm số trang 
            },
              CurrentCategory = category
          });
    }
}

