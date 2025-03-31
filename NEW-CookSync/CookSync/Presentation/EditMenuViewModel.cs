using CookSync.Biz;
using Csla;

namespace CookSync.Presentation;

public partial class EditMenuViewModel : ObservableObject
{
    private readonly INavigator navigator;

    private readonly IDataPortalFactory dataPortalFactory;

    private readonly MenuId? menuId;

    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private MenuEdit menuEdit;

    public ICommand BackToHomeCommand { get; }
    
    public ICommand SaveMenuCommand { get; }


    public EditMenuViewModel(MenuId? menuId, INavigator navigator, IDataPortalFactory dataPortalFactory)
    {
        this.menuId = menuId;
        this.navigator = navigator;
        this.dataPortalFactory = dataPortalFactory;
        Title = "Hello";
        BackToHomeCommand = new AsyncRelayCommand(BackToHomeView);

        SaveMenuCommand = new AsyncRelayCommand(SaveMenuView);
    }

    private async Task SaveMenuView()
    {
        try
        {
            if (MenuEdit.IsSavable)
            {
                MenuEdit = await MenuEdit.SaveAsync(); 

                await navigator.NavigateViewModelAsync<EditMenuFoodViewModel>(this, data: new MenuId(MenuEdit.Id));

            }
        }
        catch (Exception ex)
        {
            var t = ex;
        }
        
    }

    private async Task BackToHomeView()
    {
        await navigator.NavigateBackAsync(this);
    }

    internal async Task RefreshData()
    {
        if ((menuId?.Identifier) == null)
        {
            MenuEdit = await dataPortalFactory.GetPortal<MenuEdit>().CreateAsync();
            Title = "Create Menu Page";
        }
        else
        {
            MenuEdit = await dataPortalFactory.GetPortal<MenuEdit>().FetchAsync(menuId.Identifier);
            Title = "Edit Menu Page";
        }
    }
}
