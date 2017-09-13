using System.Web.Mvc;


namespace NorthwindWeb.Controllers
{
    
    /// <summary>
    /// Controller with other Information about our site
    /// </summary>
    public class OtherController : Controller
    {
        /// <summary>
        /// returns  View
        /// </summary>
        /// <returns></returns>
        public ActionResult FullWidth()
        {
            return View();
        }
        /// <summary>
        /// returns  View
        /// </summary>
        /// <returns></returns>
        public ActionResult SideBar()
        {
            return View();
        }
        /// <summary>
        /// returns  View
        /// </summary>
        /// <returns></returns>
        public ActionResult Faq()
        {
            return View();
        }
        /// <summary>
        /// returns  View
        /// </summary>
        /// <returns></returns>
        public ActionResult FourOhFour()
        {
            return View();
        }
    }
}
