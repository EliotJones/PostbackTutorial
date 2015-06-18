namespace EliotJones.PostbackTutorial.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using ViewModels.Menu;

    public class MenuController : Controller
    {
        private const string MenuSessionKey = "menu";
        private const string SubmitValue = "submit";
        private const string AddValue = "add";

        private Menu Menu
        {
            get
            {
                if (Session[MenuSessionKey] == null)
                {
                    Session[MenuSessionKey] = new Menu();
                }

                return (Menu) Session[MenuSessionKey];
            }
        }

        [HttpGet]
        public ActionResult Order()
        {
            var model = new OrderViewModel
            {
                MenuItems = GetSelectListOfMenu()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Order(OrderViewModel model, string submit)
        {
            model.MenuItems = GetSelectListOfMenu();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Meal meal = Menu.Single(m => m.Id == model.SelectedMenuItem);

            Menu.Order(meal);

            return RedirectToAction("Summary", new { name = meal.Name, time = meal.TimeToServe });
        }

        [HttpGet]
        public ActionResult Summary(string name, string time)
        {
            ViewBag.Message = "You ordered " + name + " successfully. "
                              + "The meal will be ready in " + time + " minutes.";
            return View();
        }

        protected virtual SelectList GetSelectListOfMenu()
        {
            return new SelectList(Menu, "Id", "Name");
        }

        [HttpGet]
        public ActionResult Clear()
        {
            Session[MenuSessionKey] = null;

            return RedirectToAction("Order");
        }
    }
}