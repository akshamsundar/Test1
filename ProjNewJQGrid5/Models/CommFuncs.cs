using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.IO;
using System.Web.Security;


namespace ProjNewJQGrid5.Models
{
    public static class CommFuncs
    {
        private static MainDbContext Db = new MainDbContext();

        public static Byte[] UProfileImage;

        public static bool  IsValidUser(string FEmailId, string FPassword)
        {
            bool IsValidUser = false;

            //using (Db = new MainDbContext())
            //{
            //    var user = Db.Users.FirstOrDefault(u => u.EmailId == FEmailId);

            //    if (user != null)
            //    {
            //        if (FPassword == DecodeFrom64(user.PasswordSalt))
            //        {


            //            IsValidUser = true;
            //        }

            //    }
            //    else
            //    {
            //        IsValidUser = false;
            //    }
            //}


            using (Db = new MainDbContext())
            {
                var user = Db.UsersNews.FirstOrDefault(u => u.UserId == FEmailId && u.Password==FPassword);

                if (user != null)
                {

                    //Saving the User Profile Image
                    TypeConverter tpconv = TypeDescriptor.GetConverter(typeof(Bitmap));
                    Bitmap bmp = (Bitmap)tpconv.ConvertFrom(user.ProfileImg);

                    //var FS = new FileStream(HostingEnvironment.MapPath("~/Images/UPImage.png", FileMode.Create));

                    var FS = new FileStream(HostingEnvironment.MapPath("~/Images/UPImage.png"), FileMode.Create);

                    bmp.Save(FS, ImageFormat.Png);
                    bmp.Dispose();

                    UProfileImage = user.ProfileImg;

                    IsValidUser = true;
                }
                else
                {
                    IsValidUser = false;
                }
            }


            return IsValidUser;

        }


        public static bool NIsValidUser(string FEmailId, string FPassword)
        {
            bool IsValidUser = false;
            
            using (Db = new MainDbContext())
            {
                var user = Db.UsersNews.FirstOrDefault(u => u.UserId == FEmailId);

                //user = Db.UsersNews.FirstOrDefault(u => u.UserId == FEmailId);
                //user = Db.UsersNews.Where(x => x.UserId == FEmailId);

                if (user == null)
                {

                   //Saving the User Profile Image
                   TypeConverter tpconv = TypeDescriptor.GetConverter(typeof(Bitmap));
                   Bitmap bmp = (Bitmap)tpconv.ConvertFrom(user.ProfileImg);

                   //var FS = new FileStream(HostingEnvironment.MapPath("~/Images") + @"\" + "UPImage.png", FileMode.Create);
                   
                    var FS = new FileStream(HostingEnvironment.MapPath("~/Images/UPImage.png"), FileMode.Create);

                   bmp.Save(FS, ImageFormat.Png);
                   bmp.Dispose();

                    UProfileImage = user.ProfileImg;

                    IsValidUser = true;
                }
                else
                {
                    IsValidUser = false;
                }
            }

            return IsValidUser;

        }

        //Function To Encrypt
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string encodedValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return encodedValue;
        }

        //Function to Decrypt
        public static string DecodeFrom64(string toDecode)
        {
            var toDecodeAsString = System.Convert.FromBase64String(toDecode);
            string decodedValue = System.Text.ASCIIEncoding.ASCII.GetString(toDecodeAsString);
            return decodedValue;
        }


    }
}