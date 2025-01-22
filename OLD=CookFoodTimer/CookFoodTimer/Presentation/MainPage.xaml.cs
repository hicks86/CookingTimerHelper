

namespace CookFoodTimer.Presentation;

public partial class MainPage : Page
{
    public MainViewModel? ViewModel => DataContext as MainViewModel;

    public MainPage()
    {
        this.InitializeComponent();
        this.DataContextChanged += (s, e) => ViewModel?.LoadViewModel();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel?.LoadViewModel();
        }
    }

    private async Task EditItem_Click(object sender, RoutedEventArgs e)
    {
        var id = (int)((HyperlinkButton)sender).Tag;

        await ViewModel.EditItem(id);
    }

    private void ViewItem_Click(object sender, RoutedEventArgs e)
    {
        var id = (int)((HyperlinkButton)sender).Tag;


    }
}
