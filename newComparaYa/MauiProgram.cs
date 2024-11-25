using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using newComparaYa.Service;
using newComparaYa.Services;
using newComparaYa.ViewModels;
using newComparaYa.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace newComparaYa
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureMopups()
                .UseSkiaSharp()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<AuthViewModel>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainService>();
            builder.Services.AddSingleton<ProductsView>();
            builder.Services.AddSingleton<FiltersModal>();
            builder.Services.AddSingleton<UserView>();
            builder.Services.AddSingleton<RegisterView>();
            builder.Services.AddSingleton<FavsView>();

            builder.Services.AddSingleton<ComparationService>();
            builder.Services.AddSingleton<ComparationView>();
            builder.Services.AddSingleton<CartView>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
