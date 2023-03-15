using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        MyContext mgr=new MyContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Insert(Product c)
        {
            mgr.Products.Add(c);
            mgr.SaveChanges();
            return RedirectToAction("index", "Category");
        }

        public ActionResult UpdateProduct()
        {
            return View();
        }

        // invoked when post method is called
        [HttpPost]
        public ActionResult UpdateProduct(int ProductId, Product model)
        {
            using (var context = new MyContext())
            {

                var data = context.Products.FirstOrDefault(x => x.ProductId == ProductId);

                if (data != null)
                {
                    data.ProductId = model.ProductId;
                    data.ProductName = model.ProductName;

                    context.SaveChanges();
                    return RedirectToAction("index", "Category");
                }
                else
                    return RedirectToAction("index", "Category");
            }
        }

        public ActionResult DeleteProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteProduct(int ProductId)
        {
            using (var context = new MyContext())
            {
                var data = context.Products.FirstOrDefault(x => x.ProductId == ProductId);
                if (data != null)
                {
                    context.Products.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("index", "Category");
                }
                else
                    return RedirectToAction("index", "Category");
            }
        }
    }
}