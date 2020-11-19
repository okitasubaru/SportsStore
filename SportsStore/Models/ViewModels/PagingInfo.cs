using System;

namespace SportsStore.Models.ViewModels
{
    public class PagingInfo // cấu trúc trang 
    {
        public int TotalItems { get; set; } 
        public int ItemsPerPage { get; set; }  
        public int CurrentPage { get; set; }  // điều khiẻn khi người dùng bấm vào để ghi nhận 
        public int TotalPages =>
        (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); 
    }
}
