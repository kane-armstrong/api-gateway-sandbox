using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RecipeApi.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipeController : ControllerBase
    {
        private static readonly Random Random = new Random(123543);

        private readonly IEnumerable<Recipe> _recipes = new List<Recipe>
        {
            new Recipe
            {
                CreatedDate = DateTime.Now.AddHours(Random.Next(50, 500)),
                Title = "Eggs on toast",
                Ingredients = new List<string>
                {
                    "Egg",
                    "Bread",
                    "Cooking spray",
                    "Butter"
                },
                BestWhenTheWeatherIs = "Mild"
            },
            new Recipe
            {
                CreatedDate = DateTime.Now.AddHours(Random.Next(50, 500)),
                Title = "Carrot cake",
                Ingredients = new List<string>
                {
                    "Carrot",
                    "Cake"
                },
                BestWhenTheWeatherIs = "Warm"
            },
            new Recipe
            {
                CreatedDate = DateTime.Now.AddHours(Random.Next(50, 500)),
                Title = "A drink",
                Ingredients = new List<string>
                {
                    "Vodka"
                },
                BestWhenTheWeatherIs = "Freezing"
            }
        };

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _recipes;
        }
    }
}
