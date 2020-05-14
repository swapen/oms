using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Models;



namespace OrderManagementSystem.Controllers
{
    [Authorize (Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly OrderManagementContext _context;
        private readonly ILogger<UsersController> _logger;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public ProductsController(ILogger<UsersController> logger, OrderManagementContext context, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Products> products = _context.Productss.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult UploadProduct()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult UploadProduct(ProductsViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    //if the same image is uploaded twice, the previous image is erased. So, to ensure uniqueness, we append our filename with a guid.
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    //to copy the incoming image from the browser to the Images folder in wwwroot.
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Products product = new Products();
                product.ProductName = model.ProductName;
                product.Category = model.Category;
                product.Supplier = model.Supplier;
                product.ProductDescription = model.ProductDescription;
                product.AvailableUnits = model.AvailableUnits;
                product.Price = model.Price;
                product.ImageData = uniqueFileName;

                _context.Productss.Add(product);
                _context.SaveChanges();
                return View(model);

            }
          
            return View();
        }

        [AllowAnonymous]
        public IActionResult RetrieveProducts()
        {
            List<Products> productdb = _context.Productss.ToList();
            return View(productdb);
        }

        //[Authorize(Roles = "User")]

        [AllowAnonymous]
        public IActionResult ViewProduct(int id)
        {
            Products prod = _context.Productss.Where(x => x.ProductID == id).FirstOrDefault();
            return View(prod);
        }

        public IActionResult RemoveProduct(int id)
        { 
            Products product = _context.Productss.Where(x => x.ProductID == id).FirstOrDefault();
            _context.Remove(product);
            _context.SaveChanges();
            return View("Index");
        }

        public IActionResult UpdateProduct(int id)
        {
            Products product = _context.Productss.Where(x => x.ProductID == id).FirstOrDefault();

            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(int id, Products product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
