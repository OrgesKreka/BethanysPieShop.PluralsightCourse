using Newtonsoft.Json;
using System.Collections.Generic;

namespace BethanysPieShop.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public List<Pie> MyProperty { get; set; }
    }
}
