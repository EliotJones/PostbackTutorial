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

                return (Menu)Session[MenuSessionKey];
            }
        }

        [HttpGet]
        public ActionResult Order()
        {
            var model = new OrderViewModel();

            BindSelectLists(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Order(OrderViewModel model, string submit)
        {
            if (model.SelectedMenuItem.HasValue)
            {
                if (MealIsAvailable(model))
                {
                    model.SelectedItems.Add(model.SelectedMenuItem.Value);
                }
                else
                {
                    ModelState.AddModelError("SelectedMenuItem", "This menu item is out of stock.");
                }
            }

            BindSelectLists(model);
            
            if (!ModelState.IsValid 
                || string.IsNullOrWhiteSpace(submit))
            {
                return View(model);
            }

            switch (submit)
            {
                case AddValue:
                    return AddPostback(model);
                default:
                    return SubmitPostback(model);
            }
        }

        private ActionResult AddPostback(OrderViewModel model)
        {
            ModelState.Remove("SelectedMenuItem");
            model.SelectedMenuItem = null;

            return View("Order", model);
        }

        private ActionResult SubmitPostback(OrderViewModel model)
        {
            foreach (var id in model.SelectedItems)
            {
                Meal meal = Menu.Single(m => m.Id == id);

                Menu.Order(meal);
            }

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public ActionResult Summary()
        {
            var model = Menu.OrderedMeals;
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Clear()
        {
            Session[MenuSessionKey] = null;

            return RedirectToAction("Order");
        }

        protected virtual void BindSelectLists(OrderViewModel model)
        {
            model.CurrentMenuItems = new SelectList(Menu, "Id", "Name");

            for (int i = 0; i < model.SelectedItems.Count; i++)
            {
                model.PreviousMenuItems.Add(new SelectList(Menu, "Id", "Name", model.SelectedItems[i]));
            }
        }

        private bool MealIsAvailable(OrderViewModel model)
        {
            return Menu.Any(m => m.Id == model.SelectedMenuItem.Value && m.NumberRemaining > model.SelectedItems.Count(i => i == model.SelectedMenuItem.Value));
        }
    }
}