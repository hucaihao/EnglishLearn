using ModelAndBLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishLearn.Controllers
{
    public class SpellingGamesController : Controller
    {
        // GET: SpellingGames
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetWordInfo()
        {
            var word = T_Word.GetRandomWord();
            return Json(word, JsonRequestBehavior.AllowGet);
        }

        public string makeMp3(string word)
        {
            try
            {
                string url = string.Format(@"https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q={0}&tl=en&total=1&idx=0&textlen=6", word);
                //string url = "http://translate.google.com/translate_tts?tl=en&q=" + word;
                string k = word.Substring(0, 1);
                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/wordVolice"), k);
                //string path = HttpContext.Current.Server.MapPath("~/wordVolice/") + "\\" + k;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);//在根目录下建立文件夹   
                }
                string filename = path + "\\" + word + ".mp3";


                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                return "/wordVolice/" + k + "/" + word + ".mp3";
            }
            catch (Exception ex)
            {
                return null;
            }


        }

    }

}