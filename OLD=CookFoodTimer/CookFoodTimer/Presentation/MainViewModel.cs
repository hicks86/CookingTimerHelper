using CookingTimerHelper;
using Csla;
using Windows.UI;

namespace CookFoodTimer.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;
    private readonly IDataPortalFactory dataPortalFactory;

    public string? Title { get; private set; }

    public string? Description { get; private set; }

    public ListOfMenus MenuList { get; private set; }

    public bool IsEmpty => MenuList == null || MenuList.Count == 0;

    public bool IsNotEmpty => !IsEmpty;

    public MainViewModel(INavigator navigator, IDataPortalFactory dataPortalFactory)
    {
        _navigator = navigator;
        this.dataPortalFactory = dataPortalFactory;
    }

    public void LoadViewModel()
    {
        Title = "Menu List";
        Description = "List of saved menu's";
        MenuList = dataPortalFactory.GetPortal<ListOfMenus>().Fetch();
    }

    public async Task AddMenu()
    {
        await _navigator.NavigateViewModelAsync<EditMenuViewModel>(this);
    }

    public async Task EditItem(int id)
    {
        await _navigator.NavigateViewModelAsync<EditMenuViewModel>(this, data: new Widget(id));
    }

    public record Widget(double Weight);

}
