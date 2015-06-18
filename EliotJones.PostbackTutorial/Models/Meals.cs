namespace EliotJones.PostbackTutorial.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public int NumberRemaining { get; set; }

        public decimal TimeToServe { get; set; }
    }
}