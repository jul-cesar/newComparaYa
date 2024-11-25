

namespace newComparaYa.Models
{
    public class LoginResponse
    {

        public string message { get; set; }


        public string token { get; set; }


        public Data data { get; set; }
    }
    public class Data
    {

        public string id_Usuario { get; set; }

        public string nombre { get; set; }

        public string email { get; set; }
    }

    public class ErrorLoginResponse
    {
      

        public string message { get; set; }
    }


}
