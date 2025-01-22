
namespace CookSync.Presentation;

public sealed partial class MainPage : Page
{
    public MainViewModel? ViewModel => DataContext as MainViewModel;

    public MainPage()
    {
        this.InitializeComponent();
       this.DataContextChanged += (s, e) => ViewModel?.RefreshData();

    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ViewModel?.RefreshData();
    }

}
