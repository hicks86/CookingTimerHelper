namespace CookSync.Presentation;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditMenuFoodPage : Page
{
    public EditMenuFoodViewModel? ViewModel => DataContext as EditMenuFoodViewModel;

    public EditMenuFoodPage()
    {
        this.InitializeComponent();
        this.DataContextChanged += (s, e) => ViewModel?.RefreshData();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ViewModel?.RefreshData();
    }
    
}
