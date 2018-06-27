using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Services;

namespace ImageDownload
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConvertHtmlToImage(string html)
        {
            try
            {
                var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
                var jpegBytes = htmlToImageConv.GenerateImage(html, ImageFormat.Png.ToString());
                MemoryStream memstr = new MemoryStream(jpegBytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(memstr, true, true);
                img.Save(Server.MapPath("~/Images/") + Path.GetRandomFileName() + ".jpeg", ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ZipFileDownload()
        {
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                try
                {
                    string[] fileArray = Directory.GetFiles(Server.MapPath("~/Images/"));
                    foreach (string filepath in fileArray)
                    {
                        zip.AddFile(filepath, "Files");
                    }
                    zip.Save(Server.MapPath("~/ZipFiles/" + Path.GetRandomFileName() + ".zip"));
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
        [WebMethod]
        public static string GetHtmlValuesFromUI(List<HtmlValues> DivValues)
        {
            try
            {
                string htmlString = string.Empty;
                MainPage CallMainPage = new MainPage();
                for (var i = 0; i < DivValues.Count; i++)
                {
                    htmlString = DivValues[i].HtmlDetails.ToString();
                    if (htmlString != string.Empty)
                        CallMainPage.ConvertHtmlToImage(htmlString);
                }
                CallMainPage.ZipFileDownload();
                string success = "Sucessfully Downloaded";
                return success;
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