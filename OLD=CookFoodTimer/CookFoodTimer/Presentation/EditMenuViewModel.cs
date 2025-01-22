using CookingTimerHelper;
using Csla;
using Uno.Extensions.Navigation;
using static CookFoodTimer.Presentation.MainViewModel;

namespace CookFoodTimer.Presentation;

public partial class EditMenuViewModel : ObservableObject
{
    private readonly INavigator navigator;
    private readonly IDataPortalFactory dataPortalFactory;
    private readonly Widget menuId;

    public string Title { get; set; }

    public MenuEdit MenuEdit { get; private set; }

    public EditMenuViewModel(Widget menuId)
    {
        this.navigator = navigator;
        this.dataPortalFactory = dataPortalFactory;
        menuId = menuId;

    }

    public async Task LoadViewModel()
    {
        //var service = App.Host.Services.GetService<IDataPortalFactory>(); //Reo
        var s = menuId;
        //if (menuId != null)
        //{
        //    MenuEdit = await dataPortalFactory.GetPortal<MenuEdit>().FetchAsync(menuId.Id);
        //    Title = "Edit Menu Page";
        //}
        //else
        //{
        //    MenuEdit = await dataPortalFactory.GetPortal<MenuEdit>().CreateAsync();
        //    Title = "Create Menu Page";
        //}
    }



    public async Task SaveMenu()
    {
        try
        {
            MenuEdit = await MenuEdit.SaveAsync();
            await navigator.NavigateBackAsync(this);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

    }

}
