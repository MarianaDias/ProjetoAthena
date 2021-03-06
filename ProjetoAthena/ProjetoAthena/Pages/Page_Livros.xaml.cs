﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
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
        private const int erroConexao = 0,
                          erroAcessoSite = 1,
                          erroSemLivros = 2;
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

        /// <summary>
        /// Show pop-up about an error.
        /// </summary>
        private async void Erro(int error)
        {
            switch (error)
            {
                case 0:
                    var connection = new MessageDialog("Sem Conexão!");
                    await connection.ShowAsync();
                    break;
                case 1:
                    var unkownError = new MessageDialog("Erro! Tente novamente.");
                    await unkownError.ShowAsync();
                    break;
                case 2:
                    var books = new MessageDialog("Sem livros para renovar");
                    await books.ShowAsync();
                    break;
                default:
                    break;
            }            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);            
            if (!App.NetWorkAvailable)
            {
                Erro(erroConexao);
            }
            else
            {
                App.DataConexao.RetornarLivros((IAsyncResult resultado) =>
                {
                    if (App.ViewModel.Items.Count <= 0)
                    {
                        var ignored = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            livro1.Text = "Não há livros Emprestados";
                        });
                        
                    }
                    else
                    {
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
                                case 4:
                                    livro5.Text = item.Titulo;
                                    datadev5.Text = item.StringDevolucao;
                                    temporest5.Text = "Devolver em " + totalDias + " dias";
                                    break;
                                case 5:
                                    livro6.Text = item.Titulo;
                                    datadev6.Text = item.StringDevolucao;
                                    temporest6.Text = "Devolver em " + totalDias + " dias";
                                    break;
                                default:
                                    break;
                            }
                            number++;
                        }
                    }
                });
            }            
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
            livro5.Text = "";
            livro6.Text = "";
            datadev1.Text = "";
            datadev2.Text = "";
            datadev3.Text = "";
            datadev4.Text = "";
            datadev5.Text = "";
            datadev6.Text = "";
            temporest1.Text = "";
            temporest2.Text = "";
            temporest3.Text = "";
            temporest4.Text = "";
            temporest5.Text = "";
            temporest6.Text = "";
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
                    Erro(erroConexao);
                }
            }
            else
            {
                Erro(erroSemLivros);
            }
        }

        void MostraLivros()
        {   
                  
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
                    case 4:
                        livro5.Text = item.Titulo;
                        datadev5.Text = item.StringDevolucao;
                        temporest5.Text = item.Status;
                        break;
                    case 5:
                        livro6.Text = item.Titulo;
                        datadev6.Text = item.StringDevolucao;
                        temporest6.Text = item.Status;
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
                    Erro(erroAcessoSite);
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
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Page_Sobre));
        }
    }
}
