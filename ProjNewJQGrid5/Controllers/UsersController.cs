using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using ProjNewJQGrid5.Models;
using System.Web.Security;

namespace ProjNewJQGrid5.Controllers
{
    public class UsersController : Controller
    {
        private MainDbContext db = new MainDbContext();
        //private CommFuncs CF = new CommFuncs();

        [AllowAnonymous]
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            var chkUser = CommFuncs.IsValidUser(user.EmailId, user.Password);
            

            if (chkUser==true)
            {
                FormsAuthentication.SetAuthCookie(user.EmailId, false);
                //return RedirectToAction("Index", "Users");
                return RedirectToAction("Index", "NewUser");
            }

            return View();
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            var fullPath = Server.MapPath("~/Images/UPImage.png");

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                ViewBag.deleteSuccess = "true";
            }

            Session["LoginSuccess"] = "false";
            return RedirectToAction("Index", "Users");

        }


        [Authorize]
        // GET: Users
        public async Task<ActionResult> Index()
        {

            return View(await db.Users.ToListAsync());
        }

        [Authorize]
        // GET: Users/Details/5
        public async  Task<ActionResult> Details(string id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = await db.Users.FindAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        

        [Authorize]
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try

            {

                if (ModelState.IsValid)
                {

                    db.CreateUser(user.EmailId, user.Password, CommFuncs.EncodeTo64(user.Password));
                    await db.SaveChangesAsync();

                }

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize]
        // POST: Users/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(User user  )
        {
            try
            {
                // TODO: Add update logic here

                if (db.Users.Find(user.EmailId)!=null)
                {
                    //db.DeleteUser(user.EmailId);
                    db.UpdateUser(user.EmailId, user.Password, CommFuncs.EncodeTo64(user.Password));
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
