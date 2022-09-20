using ExampleMvcXtl.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleMvcXtl.Controllers
{
    public class FirstController : Controller
    {
        // inject 1 phương thức logger
        // tạo ra biến _logger từ class ILogger
        private readonly ILogger<FirstController> _logger;

        // inject phương thức productService
        private readonly ProductService _productService;
        // tạo ra phương thức khởi tạo
        public FirstController(ILogger<FirstController> logger,ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Xin chào mọi người !");
            return View();
        }

        public void Create() // acion là method có nhiều kiểu trả về và nó không được khai báo static
        {
            Response.Headers.Add("token", "xin chao nguoi ae !");
        }

        public IActionResult Readme()
        {
            var content = @"
             Xin chao cac ban ,
             toi la hai day";
            return Content(content, "text/plain");
        }

        public IActionResult RedirectToLocal()
        {
            var url = Url.Action("", "Home");
            return LocalRedirect(url); // local ~ host
        }

        public IActionResult Redirect()
        {
            var url = "https://www.facebook.com/";
            return Redirect(url);
        }

        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(prod => prod.Id == id).FirstOrDefault();
            if(product == null)
            {
                TempData["StatusMessage"] = "Sản phẩn này không tồn tại";
                return  Redirect(Url.Action("", "Home"));
            }
            //return Content($"Sản phẩm có Id = {id}");
            // truyền dữ liệu kiểu Models
            //return View(product);

            // truyền dữ liệu kiểu ViewData
            //this.ViewData["product"] = product;
            //return View("ViewProduct2");

            // cách 3 dùng viewBag
            this.ViewBag.product = product;
            return View("ViewProduct3");
        }


    }
}
