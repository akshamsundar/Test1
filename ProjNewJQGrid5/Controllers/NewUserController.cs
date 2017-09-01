using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using ProjNewJQGrid5.Models;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.IO;
using System.Web.Security;
using System.Drawing;
namespace ProjNewJQGrid5.Controllers
{
    public class NewUserController : Controller
    {
        private MainDbContext Db = new MainDbContext();

        //private CommFuncs CF = new CommFuncs();


        [AllowAnonymous]

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsersNew user)
        {

            var chkUser = CommFuncs.IsValidUser(user.UserId, user.Password);


            if (chkUser == true)
            {
                FormsAuthentication.SetAuthCookie(user.UserId, false);
                return RedirectToAction("Index", "NewUser");
            }

            return View();

        }

        public ActionResult ExportReport()
        {
            List<UsersNew> lstusers=new List<UsersNew>();

            using(MainDbContext db=new MainDbContext())
            {
                lstusers=db.UsersNews.ToList();
            }


            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Reports"),"CrystalReport1.rpt"));
            //rptDoc.Load(Server.MapPath("~/Reports/CrystalReport1.rpt"));
            //rd.SetDataSource(lstusers);
            
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            try
            {
                //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //stream.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/pdf", "Userlist.pdf");



            }
            catch(Exception ex)
            {
                throw;
            }


            return View(lstusers);
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
        public async Task<ActionResult> Index(UsersNew user)
        {

              return View(await Db.UsersNews.ToListAsync());
        }

        //Function to Get Images on the Index Page

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 

        public ActionResult showImg(String id)
        {
            //var imageData = from m in Db.UsersNews  where m.UserId == id select Image.FromStream(new MemoryStream(m.ProfileImg.ToArray()));

            ////return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");

            //return new FileStreamResult(new System.IO.MemoryStream(imageData), "Image/jpeg");

            var image = (from m in Db.UsersNews
                         where m.UserId == id
                         select m.ProfileImg).FirstOrDefault();

            var stream = new MemoryStream(image.ToArray());

            return new FileStreamResult(stream, "image/jpeg");

        }


        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in Db.UsersNews where temp.Id == Id select temp.ProfileImg;
            byte[] cover = q.First();
            return cover;
        }
        
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        //Function to Get Images on the Index Page Ends

        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(UsersNew user, HttpPostedFileBase file)
        {


            if (CommFuncs.NIsValidUser(user.UserId, user.Password)==false)
            {
                if (file.FileName==null)
                   {
                    return View();
                   }


                user.ProfileImg = ConvertToBytes(file);

                Db.NCreateUser(user.UserId, user.Password, CommFuncs.EncodeTo64(user.Password), user.ProfileImg);

                await Db.SaveChangesAsync();

                return RedirectToAction("Index");


            }


            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(UsersNew user)
        {

            return View();
        }




        [Authorize]
        public ActionResult Update()
        {
            return View();
        }


        [Authorize]
        public ActionResult Delete()
        {

            return View();
        }
    }
}