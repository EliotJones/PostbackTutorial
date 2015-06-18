namespace EliotJones.PostbackTutorial.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Menu : IEnumerable<Meal>
    {
        private List<Meal> meals = new List<Meal>
        {
            new Meal
            {
                Id = 1,
                Name = "Chicken Kiev",
                NumberRemaining = 10,
                Price = 10.00m,
                TimeToServe = 10.00m
            },
            new Meal
            {
                Id = 5,
                Name = "Chicken Schnitzel",
                NumberRemaining = 7,
                Price = 9.50m,
                TimeToServe = 11.10m
            },
            new Meal
            {
                Id = 3,
                Name = "Paneer Curry",
                NumberRemaining = 3,
                Price = 7.30m,
                TimeToServe = 5.00m
            },
            new Meal
            {
                Id = 10,
                Name = "5 Bean Chili",
                NumberRemaining = 2,
                Price = 5.20m,
                TimeToServe = 6.10m
            },
            new Meal
            {
                Id = 25,
                Name = "Pulled Pork Burger",
                NumberRemaining = 7,
                Price = 12.00m,
                TimeToServe = 9.40m
            }
        };  

        public IEnumerator<Meal> GetEnumerator()
        {
            return meals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Order(Meal orderedMeal)
        {
            var meal = meals.Single(m => m.Id == orderedMeal.Id);

            if (meal.NumberRemaining <= 0)
            {
                throw new InvalidOperationException("Cannot order this meal, it is sold out.");
            }

            meal.NumberRemaining--;

            var mealInOrderedList = OrderedMeals.SingleOrDefault(m => m.Id == meal.Id);

            if (mealInOrderedList != null)
            {
                mealInOrderedList.NumberRemaining++;
            }
            else
            {
                OrderedMeals.Add(new Meal
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    NumberRemaining = 1,
                    TimeToServe = meal.TimeToServe,
                    Price = meal.Price
                });
            }
        }

        public IList<Meal> OrderedMeals = new List<Meal>(); 
    }
}