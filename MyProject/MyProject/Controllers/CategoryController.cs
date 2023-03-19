using MyProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class CategoryController : Controller
    {
        MyContext mgr=new MyContext();
       // private int? page;

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Category c)
        {
            mgr.Categories.Add(c);
            mgr.SaveChanges();
            return RedirectToAction("index", "Category");
        }

        //Pagination on server side
        public ActionResult ProductList(int? page)
        {
            ArrayList list = new ArrayList();
           // var pageNumber = page;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var products = (from p in mgr.Products
                            join c in mgr.Categories on p.CategoryId equals c.CategoryId
                            select new
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                CategoryId = p.CategoryId,
                                CategoryName = c.CategoryName
                            })
                            .OrderBy(p => p.CategoryId)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            ViewBag.TotalPages = Math.Ceiling((double)mgr.Products.Count() / pageSize);
            ViewBag.PageNumber = pageNumber;
            list.Add(products);
            ViewData["list"] = products;
            ViewBag.data = products;
            return View(); 
        }

        public ActionResult Details()
        {
            List<Category> list = mgr.Categories.ToList();
            List<Product> products = mgr.Products.ToList();
            ViewData["list"] = list;
            ViewData["products"] = products;
            return View();

            // for retriving the records for current page.
          /* int pageSize = 10;
            int pageNumber = (page ?? 1);
            var products = (from p in mgr.Products
                            join c in mgr.Categories on p.CategoryId equals c.CategoryId
                            select new
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                CategoryId = p.CategoryId,
                                CategoryName = c.CategoryName
                            })
                            .OrderBy(p => p.ProductId)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            ViewBag.TotalPages = Math.Ceiling((double)mgr.Products.Count() / pageSize);
            ViewBag.PageNumber = pageNumber;
            return View(products);*/
        }

        public ActionResult Update()
        {
            return View();
        }

        // invoked when post method is called
        [HttpPost]
        public ActionResult Update(int CategoryId, Category model)
        {
            using (var context = new MyContext())
            {

                var data = context.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);

                if (data != null)
                {
                    data.CategoryId = model.CategoryId;
                    data.CategoryName = model.CategoryName;
                   
                    context.SaveChanges();
                    return RedirectToAction("index", "Category");
                }
                else
                    return RedirectToAction("index", "Category");
            }
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int CategoryId)
        {
            using (var context = new MyContext())
            {
                var data = context.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);
                if (data != null)
                {
                    context.Categories.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("index", "Category");
                }
                else
                    return RedirectToAction("index", "Category");
            }
        }

    }
}