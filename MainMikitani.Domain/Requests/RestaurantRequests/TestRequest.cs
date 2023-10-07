using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.RestaurantRequests {
    public class Menu { 
        public List<MenuCategory> Menus { get; set; }
    }
    public class MenuCategory {
        public string CategoryName { get; set; }
        public List<Dish> Dishes { get; set; }

    }
    public class Dish {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public IFormFile FormFile { get; set; }
        public bool IsActiove { get; set; }
    }

    public class Menu1 {
        public MenuCategory1 Menus { get; set; }
    }
    public class MenuCategory1 {
        public string CategoryName { get; set; }
        public Dish1 CategoryDishes { get; set; }
       

    }            
    public class Category {
        public Dish1 Dishes { get; set; }
    }
    public class Dish1 {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
