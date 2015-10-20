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

        private void renovar_Click(object sender, RoutedEventArgs e)
        {
            if (App.ViewModel.Items.Count > 0)
            {
                if (App.NetWorkAvailable)
                {
                    App.DataConexao.RenovarLivros(RenLivrosCallback);
                }
                else
                {
                    livro1.Text = "sem conexão";

                }
            }
            else
            {
                livro1.Text = "sem livros pra renovar";
            }
        }
        void RenLivrosCallback(IAsyncResult resultado)
        {
            if (App.DataConexao.Erro)
            {
                var ignored = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,() => 
                {
                    livro1.Text = "erro";
                });
            }
            else
            {
                var ignored = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,() => 
                {
                    bool reservado = false;
                    foreach (ItemViewModel item in App.ViewModel.Items)
                    {
                        if (item.Reservado)
                        {
                            reservado = true;
                            break;
                        }
                    }
                    if (reservado)
                    {
                        livro1.Text = "RESERVADO";
                    }
                    else
                    {                        
                        List<string> livros = new List<string>();
                        List<string> datadev = new List<string>();
                        List<string> status = new List<string>();
                        foreach (ItemViewModel item in App.ViewModel.Items)
                        {
                            livros.Add(item.Titulo);
                            datadev.Add(item.StringDevolucao);
                            status.Add(item.Status);
                        }
                        if (livros.ElementAt(0) != null)
                        {
                            livro1.Text = livros.ElementAt(0);
                            datadev1.Text = datadev.ElementAt(0);
                            temporest1.Text = status.ElementAt(0);
                        }
                    }
                });
            }
        }
    }
}
