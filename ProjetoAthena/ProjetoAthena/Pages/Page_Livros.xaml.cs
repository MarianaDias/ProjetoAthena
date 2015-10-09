using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Page_Livros : Page
    {        
        
        public Page_Livros()
        {
            this.InitializeComponent();
            Limpar();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parametro = e.Parameter as AthenaData;
            if (parametro.Dados.Titulo[0] != null)            
                livro1.Text = parametro.Dados.Titulo[0];
            if (parametro.Dados.Titulo[1] != null)
                livro2.Text = parametro.Dados.Titulo[1];
            if (parametro.Dados.Titulo[2] != null)                
                livro3.Text = parametro.Dados.Titulo[2];
            if (parametro.Dados.Titulo[3] != null)
                livro4.Text = parametro.Dados.Titulo[3];
        }


        private void voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        public void Limpar()
        {
            livro1.Text = "";
            livro2.Text = "";
            livro3.Text = "";
            livro4.Text = "";
            datadev1.Text = "Data Devolução:";
            datadev2.Text = "Data Devolução:";
            datadev3.Text = "Data Devolução:";
            datadev4.Text = "Data Devolução:";
            temporest1.Text = "dias restantes";
            temporest2.Text = "dias restantes";
            temporest3.Text = "dias restantes";
            temporest4.Text = "dias restantes";
        }
    }
}
