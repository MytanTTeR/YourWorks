using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace YourWorks.Core
{
    public static class Download
    {
        static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        public static string SaveFile(HttpPostedFileBase file, string path)
        {
            string fileName = Path.GetFileName(file.FileName);
            string[] fileFormat = fileName.Split('.');
            byte[] byteHash = MD5.ComputeHash(Encoding.Unicode.GetBytes(fileName + DateTime.Now.ToString()));
            fileName = string.Empty;
            foreach (byte b in byteHash)
                fileName += string.Format("{0:x2}", b);
            fileName += "." + fileFormat[fileFormat.Length - 1];
            file.SaveAs(HostingEnvironment.MapPath(path + fileName));

            return fileName;
        }

        public static MvcHtmlString RenderFile(string virtualPath)
        {
            var fileContent = File.ReadAllText(virtualPath);
            return new MvcHtmlString(fileContent);
        }
    }
}