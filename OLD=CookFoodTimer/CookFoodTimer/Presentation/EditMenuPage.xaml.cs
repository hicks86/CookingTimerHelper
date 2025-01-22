
namespace CookFoodTimer.Presentation;

public partial class EditMenuPage : Page
{
    public EditMenuViewModel? ViewModel => this.DataContext as EditMenuViewModel;

    public EditMenuPage()
    {
        this.InitializeComponent();
        this.DataContextChanged += (s, e) => ViewModel?.LoadViewModel();
    }
}
