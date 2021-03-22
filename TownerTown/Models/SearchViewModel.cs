using TownerTown.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TownerTown.Web.Models
{
    public class SearchViewModel
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public string SearchInput { get; set; }
        public string selectedCity { get; set; }
        [NotMapped]
        public IEnumerable<City> Cities { get; set; }
        public string selectedCategory { get; set; }

        public string[] selectedCategories { get; set; }
        [NotMapped]
        public List<Category> Categories { get; set; }
        
    }

    public class City
    {
        public string CityID { get; set; }
        public string CityName { get; set; }
    }
}
