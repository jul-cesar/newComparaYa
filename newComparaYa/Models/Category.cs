﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newComparaYa.Models
{
    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }    
        public DateTime updated_at { get; set; }
    }
}
