using GroceryStore.Data;
using GroceryStore.Models;
using GroceryStore.Models.other;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly GroceryStoreContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(GroceryStoreContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            webHostEnvironment = hostingEnvironment;
        }


        //Get Home Index
        public IActionResult Index()
        {

            ViewBag.categories = _context.Category
                                        .ToList();

            ViewBag.products = _context.Products.ToList();

            return View();
        }


        //Get login page
        public IActionResult Login()
        {
            return View();
        }

        //Post login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("userName", "password")] Admin log)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.Admin
                            .Where(x => x.userName == log.userName)
                            .SingleOrDefault();
                if (admin == null)
                {
                    ViewBag.ErrorMessage = "Login Failed: Username and/or Password did not match.";
                    return PartialView();
                }

                if (log.userName == admin.userName && log.password == admin.password)
                {
                    UserLogin.IsLogin = true;
                    UserLogin.UserId = admin.Id;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Login Failed: Username and/or Password did not match.";
                    return PartialView();
                }
            }
            return PartialView(log);
        }

        // Get Category
        public IActionResult Category()
        {
            var category = _context.Category.ToList();
            return View(category.ToList());
        }

        // get create catagory
        public IActionResult CreateCategory()
        {
            return View();
        }


        //post create category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("CategoryTitle,CategoryPicture,Offer")] CreateCategory model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = CategoryUploadPicture(model);
                Category category = new Category
                {
                    CategoryTitle = model.CategoryTitle,
                    Offer = model.Offer ?? 0,
                    CategoryPicture = uniqueFileName
                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Category", "Home");

            }
            return View(model);

        }



        // edit category
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Category
                                    .Where(c=> c.Id == id)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
            if (category == null)
                return NotFound();

            EditCategory eCategory = new EditCategory
            {
                Id = category.Id,
                CategoryTitle = category.CategoryTitle,
                ExistPath = category.CategoryPicture,
                Offer = category.Offer
            };
            return View(eCategory);
        }

        //post edit category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [Bind("Id, CategoryTitle, ExistPath, Offer, CategoryPicture")] EditCategory model)
        {
            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Id = model.Id,
                    CategoryTitle = model.CategoryTitle,
                    Offer = model.Offer ?? 0
                };

                string uniqueFileName;

                if (model.CategoryPicture != null)
                {
                    string existpath = Path.Combine(webHostEnvironment.WebRootPath, "images/category", model.ExistPath);
                    System.IO.File.Delete(existpath);

                    uniqueFileName = CategoryUploadPicture(model);
                }
                else
                    uniqueFileName = model.ExistPath;

                category.CategoryPicture = uniqueFileName;

                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. ");
                }
                return RedirectToAction("Category", "Home");
            }
            return View(model);
        }


        // save category picture
        private string CategoryUploadPicture(CreateCategory model)
        {
            string uniqueFileName = null;

            if (model.CategoryPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/category");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CategoryPicture.FileName;

                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filepath, FileMode.Create);
                model.CategoryPicture.CopyTo(fileStream);
            }
            return uniqueFileName;
        }


        /*====================================           Products        ============================================   */

        // GET products
        public async Task<IActionResult> Products(int? id)
        {
            var products = await _context.Products
                            .AsNoTracking()
                            .ToListAsync();

            if(id != null) 
            {
                var category = await _context.Category
                                   .Where(c => c.Id == id)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync();
                if (category == null)
                    return NotFound();

                ViewBag.Title = category.CategoryTitle;

                products = await _context.Products
                            .AsNoTracking()
                            .Where(x => x.Id == id)
                            .ToListAsync();
                
            }
            return View(products.ToList());
        }

        public IActionResult AddProducts()
        {
            PopulateMenuDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProducts([Bind("ProductNumber, Title, Picture, Price, Details, Offer, CategoryId")] CreateProducts model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadfile(model);

                Products NewProducts = new Products
                {
                    ProductNumber = model.ProductNumber,
                    Title = model.Title,
                    Picture = uniqueFileName,
                    Price = model.Price,
                    Details = model.Details,
                    CategoryId = model.CategoryId,
                    Offer = model.Offer ?? 0
                };
                _context.Add(NewProducts);
                await _context.SaveChangesAsync();

                return RedirectToAction("Products", "Home");

            }
            PopulateMenuDropDownList(model.CategoryId);
            return View(model);
        }

        private string ProcessUploadfile(CreateProducts model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/products");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;

                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filepath, FileMode.Create);
                model.Picture.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        private void PopulateMenuDropDownList(object selectedCategory = null)
        {
            var categories = from d in _context.Category
                             orderby d.CategoryTitle
                             select d;
            ViewBag.Categorielist = new SelectList(categories.AsNoTracking(), "Id", "CategoryTitle", selectedCategory);
        }

    }
}
