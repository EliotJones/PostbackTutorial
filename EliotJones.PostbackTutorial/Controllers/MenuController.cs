namespace EliotJones.PostbackTutorial.Controllers
{
    using System.Web.Mvc;

    public class MenuController : Controller
    {
        [HttpGet]
        public ActionResult Order()
        {
            return View();
        }
    }
}