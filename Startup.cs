
using ExampleMvcXtl.Services;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ExampleMvcXtl
{
    public class Startup
    {
        public static string? ContentRootPath { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "appmvc";                 // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            // services.AddTransient(typeof(ILogger<>), typeof(Logger<>)); //Serilog
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // /View/Controller/Action.cshtml
                // /MyView/Controller/Action.cshtml

                // {0} -> ten Action
                // {1} -> ten Controller
                // {2} -> ten Area
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

                options.AreaViewLocationFormats.Add("/MyAreas/{2}/Views/{1}/{0}.cshtml");
            });
            /*services.AddSingleton<ProductService>();*/   // tạo ra 1 đối tượng dịch vụ
            /*services.AddTransient<ProductService>();*/   // mỗi lần truy vấn để lấy ra dịch vụ thì 1 đối tượng mới được tạo ra 
            /*services.AddScoped<ProductService>();  */    // mỗi phiên truy vập nếu mà lấy dịch vụ này ra thì 1 đối tượng mới được tạo ra 
            // có 4 cách viết để add 1 service
            //services.AddSingleton<ProductService>();
            //services.AddSingleton<ProductService , ProductService> ();
            //services.AddSingleton(typeof(ProductService));
            services.AddSingleton(typeof(ProductService), typeof(ProductService));
            // cách viết 2 và 4 khi lấy ra đối tượng product service có thể tạo ta productService hoặc là 
            // những đối tượng triển khai kể thừa của productService

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

             //contents/1.jpg => Uploads/1.jpg
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
            //    ),
            //    RequestPath = "/contents"
            //});

            app.UseSession();

            app.UseRouting();        // EndpointRoutingMiddleware

            app.UseAuthentication(); // xac dinh danh tinh 
            app.UseAuthorization();  // xac thuc  quyen truy  cap

            app.UseEndpoints(endpoints =>
            {
                // /sayhi
                endpoints.MapGet("/sayhi", async (context) => {
                    await context.Response.WriteAsync($"Hello ASP.NET MVC {DateTime.Now}");
                });

                // endpoints.MapControllers
                // endpoints.MapControllerRoute
                // endpoints.MapDefaultControllerRoute
                // endpoints.MapAreaControllerRoute

                // [AcceptVerbs]

                // [Route]

                // [HttpGet]
                // [HttpPost]
                // [HttpPut]
                // [HttpDelete]
                // [HttpHead]
                // [HttpPatch]

                // Area

                //endpoints.MapControllers();

                //endpoints.MapControllerRoute(
                //    name: "first",
                //    pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(2,4)}",
                //    defaults: new
                //    {
                //        controller = "First",
                //        action = "ViewProduct"
                //    }

                //);

                //endpoints.MapAreaControllerRoute(
                //    name: "product",
                //    pattern: "/{controller}/{action=Index}/{id?}",
                //    areaName: "ProductManage"
                //);

                // Controller khong co Area
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapRazorPages();
            });
        }
    }
}
