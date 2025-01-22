
using System.Collections.ObjectModel;
using CookSync.Biz;
using Csla;
using Csla.DataPortalClient;

namespace CookSync.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator navigator;
    
    private IDataPortalFactory dataPortalFactory;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private ListOfMenus menuList;

    public AsyncRelayCommand<int> EditMenuItemCommand { get; }
    public AsyncRelayCommand<int> ViewMenuItemCommand { get; }
    public AsyncRelayCommand AddMenuItemCommand { get; }

    public MainViewModel(INavigator navigator, IDataPortalFactory dataPortalFactory)
    {
        this.navigator = navigator;
        this.dataPortalFactory = dataPortalFactory;
        EditMenuItemCommand = new AsyncRelayCommand<int>(EditMenuItem);
        ViewMenuItemCommand = new AsyncRelayCommand<int>(ViewMenuItem);
        AddMenuItemCommand = new AsyncRelayCommand(AddMenuItem);
        RefreshData();
    }    

    internal void RefreshData()
    {
        Title = "Menu List";
        Description = "List of saved menu's";
        MenuList = dataPortalFactory.GetPortal<ListOfMenus>().Fetch();
    }

    private async Task EditMenuItem(int id)
    {
        await navigator.NavigateViewModelAsync<EditMenuViewModel>(this, data: new MenuId(id));
    }

    private async Task ViewMenuItem(int arg)
    {
        throw new NotImplementedException();
    }

    private async Task AddMenuItem()
    {
        await navigator.NavigateViewModelAsync<EditMenuViewModel>(this, data: new MenuId(null));
    }

}
