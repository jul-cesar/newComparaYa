using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace newComparaYa.Models
{
    public partial class Product : ObservableObject
    {
        public string id { get; set; } // UUID
        public string name { get; set; } // VarChar(255)
        public string image_url { get; set; }
        public decimal? price_d1 { get; set; } // Decimal(10, 3)
        public decimal? price_olim { get; set; } // Decimal(10, 3)
        public decimal? price_exito { get; set; } // Decimal(10, 3)
        public string category_id { get; set; }
        public DateTime created_at { get; set; } 
        public DateTime updated_at { get; set; }
        [ObservableProperty]
        private int cantidad = 1; // Valor inicial predeterminado

    }
}
