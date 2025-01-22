using System.Globalization;
using CookingTimerHelper;
using CookingTimerHelper.Dal;
using CookingTimerHelper.Dal.DataAccessLayer;
using Csla.Configuration;
using Uno.Resizetizer;
using static CookFoodTimer.Presentation.MainViewModel;

namespace CookFoodTimer;
public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected Window? MainWindow { get; private set; }
    public IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {

        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .ConfigureServices((context, services) => services
                    .AddTransient<IFoodMenuDal, InMemoryFoodMenuRepository>()
                    .AddCsla())
                .UseNavigation(RegisterRoutes));
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        Host = await builder.NavigateAsync<Shell>();
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(ShellViewModel)),
            new ViewMap<MainPage, MainViewModel>(),
            new DataViewMap<EditMenuPage, EditMenuViewModel, Widget>()

        );

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
                Nested:
                [
                    new ("Main", View: views.FindByViewModel<MainViewModel>(), IsDefault:true),
                    new ("EditMenu", View: views.FindByViewModel<EditMenuViewModel>()),
                ]
            )
        );
    }
}
