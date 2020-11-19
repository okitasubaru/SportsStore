using System.Collections.Generic;
using System.Threading.Tasks;
using SportsStore.Models;

namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel 
    {
        public IEnumerable<Product> Products { get; set; } // danh sách sản phẩm lấy từ database lên 
        public PagingInfo PagingInfo { get; set; } // thông tin phân trang 
        public string CurrentCategory { get; set; }  // danh mục hiện tại đang chọn 
    }
}
