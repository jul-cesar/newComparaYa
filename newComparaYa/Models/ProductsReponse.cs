

namespace newComparaYa.Models;

    public class ProductsReponse
    {
        public List<Product> data { get; set; }
         public Meta meta { get; set; }  
    }
public class Meta
{
    public int totalProducts { get; set; }
    public int totalPages { get; set; }
    public int currentPage { get; set; }
    public int limit { get; set; }
}


