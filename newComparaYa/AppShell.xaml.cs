using Microsoft.Win32;
using newComparaYa.Views;

namespace newComparaYa
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("register", typeof(RegisterView));
            Routing.RegisterRoute("products", typeof(ProductsView));
            Routing.RegisterRoute("comparation", typeof(ComparationView));
            Routing.RegisterRoute("login", typeof(LoginView));

        }
    }
}
