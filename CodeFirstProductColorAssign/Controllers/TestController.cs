using CodeFirstProductColorAssign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstProductColorAssign.Controllers
{
    public class TestController : Controller
    {
      CompanyContext cc=new CompanyContext();
        public ActionResult Index()
        {
            //    //return View(this.cc.Products.ToList());
            //    var v = from t in cc.Products
            //            join t1 in cc.ProductColors
            //            on t.ProductID equals t1.ProductID
            //            join t2 in cc.Colors on
            //            t1.ColorID equals t2.ColorID
            //            group t by t.ProductName into g
            //            select new ColorVM
            //            {

            //                ProductName=g.Key,
            //                ColorCount=g.Count(),
            //            };
            var v = from t in cc.Products
                    select new ColorVM
                    {
                        ProductID = t.ProductID,
                        ProductName = t.ProductName,
                        ColorCount = t.ProductColors.Count()
                    };
            return View(v.ToList());
        }
        [HttpGet]
        public ActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product rec, Int64[] chk)
        {
            this.cc.Products.Add(rec);
            this.cc.SaveChanges();
            foreach(Int64 cid in chk)
            {
                ProductColor pc = new ProductColor();
                pc.ColorID=cid;
                pc.ProductID = rec.ProductID;
                this.cc.ProductColors.Add(pc);
            }
            this.cc.SaveChanges();
            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public ActionResult GetCheckBox()
        {
            var v = from t in cc.Colors
                    select new CheckBoxVM
                    {
                        Value = t.ColorID,
                        Text = t.ColorName,
                        IsSelected = false
                    };
            return View("_CheckBoxView", v.ToList());
        }
        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            var rec = this.cc.Products.Find(id);
            return View(rec);
        }
        [ChildActionOnly]
        public ActionResult GetChecked(Int64 id)
        {
            var rec = this.cc.Products.Find(id);
            var c = rec.ProductColors.Select(a => a.ColorID).ToList();
            var v = from t in cc.Colors
                    select new CheckBoxVM
                    {
                        Value = t.ColorID,
                        Text = t.ColorName,
                        IsSelected = c.Contains(t.ColorID)
                    };
            ViewBag.Chk = v.ToList();
            return View("_CheckBoxView", v.ToList());
        }
        [HttpPost]
        public ActionResult Edit(Product rec, Int64[]chk)
        {
            // Product srec = this.cc.Products.Find(rec.ProductID);
            this.cc.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this.cc.SaveChanges();
            var v=this.cc.ProductColors.Where(p=>p.ProductID==rec.ProductID).ToList();
            foreach(var temp in v)
            {
                this.cc.ProductColors.Remove(temp);
            }
            foreach(var cid in chk)
            {
                ProductColor pp = new ProductColor();
                pp.ColorID=cid;
                pp.ProductID= rec.ProductID;
                this.cc.ProductColors.Add(pp);
            }
            //this.cc.Products.Add(rec);
            this.cc.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(Int64 id)
        {
            var rec = this.cc.Products.Find(id);
            return View(rec);
        }
        [HttpGet]
        public ActionResult Delete(Int64 id)
        {
            var rec= this.cc.Products.Find(id);
            this.cc.Products.Remove(rec);
            this.cc.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}