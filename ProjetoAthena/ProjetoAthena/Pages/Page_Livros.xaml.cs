using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Page_Livros : Page
    {        
        public Page_Livros()
        {
            this.InitializeComponent();
            Limpar();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;            
        }        

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.DataConexao.RetornarLivros((IAsyncResult resultado)=>
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
                int number = 0;                
                foreach (ItemViewModel item in App.ViewModel.Items)
                {
                    int totalDias = (item.DataDevolucao - DateTime.Now).Days;
                    switch (number)
                    {
                        case 0:
                            livro1.Text = item.Titulo;
                            datadev1.Text = item.StringDevolucao;
                            temporest1.Text = "Devolver em " + totalDias + " dias";
                            break;
                        case 1:
                            livro2.Text = item.Titulo;
                            datadev2.Text = item.StringDevolucao;
                            temporest2.Text = "Devolver em " + totalDias + " dias";
                            break;
                        case 2:
                            livro3.Text = item.Titulo;
                            datadev3.Text = item.StringDevolucao;
                            temporest3.Text = "Devolver em " + totalDias + " dias";
                            break;
                        case 3:
                            livro4.Text = item.Titulo;
                            datadev4.Text = item.StringDevolucao;
                            temporest4.Text = "Devolver em " + totalDias + " dias";
                            break;
                        default:
                            break;
                    }
                    number++;
                }
            });
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
            datadev1.Text = "";
            datadev2.Text = "";
            datadev3.Text = "";
            datadev4.Text = "";
            temporest1.Text = "";
            temporest2.Text = "";
            temporest3.Text = "";
            temporest4.Text = "";
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

        void MostraLivros()
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
            int number = 0;
            foreach (ItemViewModel item in App.ViewModel.Items)
            {
                switch (number)
                {
                    case 0:
                        livro1.Text = item.Titulo;
                        datadev1.Text = item.StringDevolucao;
                        temporest1.Text = item.Status;
                        break;
                    case 1:
                        livro2.Text = item.Titulo;
                        datadev2.Text = item.StringDevolucao;
                        temporest2.Text = item.Status;
                        break;
                    case 2:
                        livro3.Text = item.Titulo;
                        datadev3.Text = item.StringDevolucao;
                        temporest3.Text = item.Status;
                        break;
                    case 3:
                        livro4.Text = item.Titulo;
                        datadev4.Text = item.StringDevolucao;
                        temporest4.Text = item.Status;
                        break;
                    default:
                        break;
                }
                number++;                
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
                    MostraLivros();
                });
            }
        }
    }
}
