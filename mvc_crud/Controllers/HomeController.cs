using mvc_crud.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_crud.Controllers
{     
    public class HomeController : Controller
    {    
        NorthwindEntities db = new NorthwindEntities();
       
        public ActionResult Index()
        {
            var model = db.Orders.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Order order)
        {
            if (order.OrderID==0)// insert ucun
            {
                db.Orders.Add(order);
            }
            else
            {
                var updateData=db.Orders.Find(order.OrderID);  
                if (updateData==null)
                {
                    return HttpNotFound();
                }
                updateData.ShipName=order.ShipName;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Update()
        {
            var model = db.Orders.Find();
            if (model!=null) {
                return HttpNotFound();
            }
            return View("Yeni", model);

        }
        public ActionResult Delete(int id)
        {
            var deleteOrder = db.Orders.Find(id);
            if (deleteOrder==null) 
            {
                return HttpNotFound();

            }
            db.Orders.Remove(deleteOrder);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}