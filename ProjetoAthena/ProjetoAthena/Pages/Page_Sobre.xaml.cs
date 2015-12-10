using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjetoAthena.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Sobre : Page
    {
        public Page_Sobre()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }
        Type lastpage;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            lastpage = Frame.BackStack.Last().SourcePageType;
        }

            private void OnBackRequested(object sender, BackRequestedEventArgs e)
            {
                if ( lastpage == typeof(MainPage))
                    this.Frame.Navigate(typeof(MainPage));
                else if (lastpage == typeof(Pages.Page_Livros))
                    this.Frame.Navigate(typeof(Pages.Page_Livros));
            }

        private void textosobre_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var launcher = await Launcher.LaunchUriAsync(new Uri("http://www.ltia.fc.unesp.br/"));
        }

        private void textoequipe_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
