using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CookSync.Presentation;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditMenuPage : Page
{
    public EditMenuViewModel? ViewModel => DataContext as EditMenuViewModel;

    public EditMenuPage()
    {
        this.InitializeComponent();
        this.DataContextChanged += (s, e) => ViewModel?.RefreshData();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ViewModel?.RefreshData();
    }
}
