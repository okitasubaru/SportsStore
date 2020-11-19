
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
                        Configuration = configuration;
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) // nhung cai service chay
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>(); // add anh xa

            

            // services.AddTransient<IProductRepository, FakeProductRepository>(); // add anh xa hai file IProsuctRepository va FakeProductRepository vao
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); //Phương thức AddScoped chỉ định rằng cùng một đối tượng được sử dụng để 
                                                                     //đáp ứng các yêu cầu liên quan đến các trường hợp trong cart .
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>(); //add anh xa 
            services.AddMvc(
                opt => opt.EnableEndpointRouting = false
                ); // kich hoat mo hinh MVC

            services.AddMemoryCache(); //phương thức thiết lập kho dữ liệu trong bộ nhớ
            services.AddSession();      //phương pháp đăng ký các dịch vụ được sử dụng
                                        // để truy cập dữ liệu 


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // moi truong service co nhung gi
        {
            app.UseDeveloperExceptionPage(); // trang exception bao loi
            app.UseStatusCodePages();   // code tra ve cua service neu co loi( vi du 404 -not found )
            app.UseStaticFiles();   // support lop tinh wwwroot 
            app.UseSession();   //phương thức cho phép hệ thống 
                                //tự động liên kết các yêu cầu với các session  khi chúng đến từ khách hàng.

            //  routes.MapRoute( // đường dẫn URL 
            //   name: "pagination", 
            //   template: "Products/Page{productPage}", 
            //   defaults: new { Controller = "Products", action = "List" }); // duong dan trang list khi chạy

            //  routes.MapRoute(
            //  name: "default",
            //  template: "{controller=Products}/{action=List}/{id?}");
            // đến controller = Products Tìm action = List 
            app.UseAuthentication();
            app.UseMvc(routes => {  // su dung duong dan MVC co nhỏ lại theo ý người dùng  // route la danh sach cac duong di 
            
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Products", action = "List" }
                    );
                routes.MapRoute(
                    name: null, 
                    template: "Page{productPage:int}", // nó hiểu là null nên load các sản phẩm lên  rồi phân trang 
                    defaults: new { controller = "Products", action = "List", productPage = 1 }
                     );
                routes.MapRoute(
                    name: null,
                    template: "{category}", //truyền mỗi category nên nó hiều là trang 1 nên load trang 1 lên 
                    defaults: new { controller = "Products", action = "List", productPage = 1 }
                     );
                routes.MapRoute(
                    name: null,
                    template: "", //người dùng không gõ gì cả nên nó load vào trang chủ = trang 1 (load toàn bộ sản phẩm lên ) 
                    defaults: new { controller = "Products", action = "List", productPage = 1 });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });

            SeedData.EnsurePopulated(app); // kiểm tra bản database Product rồi show 
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
