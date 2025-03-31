using CookSync.Biz;
using Csla;

namespace CookSync.Presentation;
public partial class EditMenuFoodViewModel : ObservableObject
{
    private readonly INavigator navigator;

    private readonly IDataPortalFactory dataPortalFactory;

    private readonly MenuId? menuId;

    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private MenuEdit menuEdit;

    [ObservableProperty]
    private FoodItemEdit foodItemEdit;

    public ICommand AddFoodToMenuCommand { get; }

    public EditMenuFoodViewModel(MenuId menuId, INavigator navigator, IDataPortalFactory dataPortalFactory)
    {
        this.menuId = menuId;
        this.navigator = navigator;
        this.dataPortalFactory = dataPortalFactory;

        AddFoodToMenuCommand = new AsyncRelayCommand(AddFoodToMenu);
    }

    private async Task AddFoodToMenu()
    {
        
    }

    internal async Task RefreshData()
    {
        MenuEdit = await dataPortalFactory.GetPortal<MenuEdit>().FetchAsync(menuId.Identifier);
        Title = "Edit Menu Food Page";
    }
}
