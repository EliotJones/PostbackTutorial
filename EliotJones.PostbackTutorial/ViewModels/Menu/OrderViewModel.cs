namespace EliotJones.PostbackTutorial.ViewModels.Menu
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class OrderViewModel
    {
        public SelectList CurrentMenuItems { get; set; }

        public IList<SelectList> PreviousMenuItems { get; set; }

        [Required(ErrorMessage = "Please select a menu item.")]
        public int? SelectedMenuItem { get; set; }

        public IList<int> SelectedItems { get; set; }

        public OrderViewModel()
        {
            this.SelectedItems = new List<int>();
            this.PreviousMenuItems = new List<SelectList>();
        }
    }
}