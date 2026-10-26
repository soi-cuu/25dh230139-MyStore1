using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _25dh230139_MyStore1.Models;

namespace _25dh230139_MyStore1.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private MyStore1Entities db = new MyStore1Entities();

        // GET: Admin/Categories
        // GET: lấy dữ liệu từ bảng Categories và hiển thị ra view Index
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        // Details: lấy chi tiết một bản ghi có CategoryID = id từ bảng Categories và hiển thị ra view Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);// nếu id = null thì trả về lỗi 400 : thiếu giá trị chuyền vào
            }
            Category category = db.Categories.Find(id);
            if (category == null) // nếu không tìm thấy bản ghi có CategoryID = id lỗi 404
            {
                return HttpNotFound(); //lỗi 404
            }
            {

                return View(category);
            }
        }

        // GET: Admin/Categories/Create
        // lấy 1 bản ghi từ databasse và hiển thị from Create
        //  [HttpGet] : là phương thức mặc định lên ko cần khai báo [HttpGet] cũng được
        public ActionResult Create()
        { 
            return View();
        }

        // POST: Admin/Categories/Create
        // POST: nhận dữ liệu từ form Create và lưu vào database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
