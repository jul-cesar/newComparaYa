

namespace newComparaYa.Models;

    public class RegistroResponse
    {
        public string message { get; set; }
        public UsuarioData? data { get; set; }
    }

    public class UsuarioData
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }

