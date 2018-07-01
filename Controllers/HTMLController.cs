using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ImageDownload.Controllers
{
    public class HTMLController : ApiController
    {
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        //[HttpPost]
        public HtmlValues PostHtml(HttpRequestMessage DivValues)
        {
            var htmlString = DivValues.Content.ReadAsStringAsync().Result;
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(htmlString);
            //obj.DivValues.Count
            string htmlStrVal = string.Empty;
            MainPage CallMainPage = new MainPage();
            for (var i = 0; i < obj.DivValues.Count; i++)
            {
                htmlStrVal = obj.DivValues[i].HtmlDetails.ToString();
                if (htmlString != string.Empty)
                    ConvertHtmlToImage(htmlStrVal);
            }
            ZipFileDownload();
            return null;
        }

        private void ZipFileDownload()
        {
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                try
                {
                    string[] fileArray = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Images/"));
                    foreach (string filepath in fileArray)
                    {
                        zip.AddFile(filepath, "Files");
                    }
                    zip.Save(HttpContext.Current.Server.MapPath("~/ZipFiles/" + Path.GetRandomFileName() + ".zip"));
                    foreach (string filepath in fileArray)
                    {
                        File.Delete(filepath);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void ConvertHtmlToImage(string htmlString)
        {
            try
            {
                var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
                var jpegBytes = htmlToImageConv.GenerateImage(htmlString, ImageFormat.Png.ToString());
                MemoryStream memstr = new MemoryStream(jpegBytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(memstr, true, true);
                img.Save(HttpContext.Current.Server.MapPath("~/Images/") + Path.GetRandomFileName() + ".jpeg", ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
public class HtmlValues
{
    public string Id;
    public string HtmlDetails;
}
public class RootObject
{
    public List<HtmlValues> DivValues { get; set; }
}