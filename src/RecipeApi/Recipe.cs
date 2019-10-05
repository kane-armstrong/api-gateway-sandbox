using System;
using System.Collections.Generic;

namespace RecipeApi
{
    public class Recipe
    {
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public string BestWhenTheWeatherIs { get; set; }
    }
}
