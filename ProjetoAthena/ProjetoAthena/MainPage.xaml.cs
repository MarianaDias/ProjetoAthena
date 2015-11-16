using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjetoAthena
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        void Loga(IAsyncResult resultado)
        {
            if (!App.DataConexao.Erro)
            {
                var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { this.Frame.Navigate(typeof(Pages.Page_Livros)); });
            }
            else if (App.DataConexao.DadosIncorretos)
            {

                var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    textoaviso.Text = "CPF ou Senha Incorreto";
                    textoaviso.Visibility = Visibility.Visible;
                });
            }
        }

        private void sobre_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Page_Sobre));
        }

        private void limpar_Click(object sender, RoutedEventArgs e)
        {
            CPF.Text = "";
            Senha.Password = "";
        }

        private async void logar_Click(object sender, RoutedEventArgs e)
        {

            if (App.NetWorkAvailable)
            {
                lembrar_Click(logar, e);
                App.DataConexao.RetornarLivros(Loga);
                App.DataConexao.Usuario = CPF.Text;
                App.DataConexao.Senha = Senha.Password;
            }
            else if (!App.NetWorkAvailable)
            {
                var connection = new MessageDialog("Sem Conexão!");
                await connection.ShowAsync();
              
            }
        }

        private void lembrar_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)lembrar.IsChecked)
            {
                App.Check = true;
                App.DataConexao.Usuario = CPF.Text;
                App.DataConexao.Senha = Senha.Password;
                App.Usuario = CPF.Text;
                App.Senha = Senha.Password;
            }
            else
            {
                App.Check = false;
                App.Uncheck();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (App.Usuario != "" && App.Senha != "")
            {
                CPF.Text = App.Usuario;
                Senha.Password = App.Senha;
                lembrar.IsChecked = true;
            }
        }

        private void CPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ok = false;
            CPF.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 122, 122));
            if (CPF.Text.Length == 11)
            {
                textoaviso.Visibility = Visibility.Collapsed;
                byte[] asciiBytes = Encoding.ASCII.GetBytes(CPF.Text);
                foreach (byte i in asciiBytes)
                {
                    ok = false;
                    if (i < 48 || i > 57)
                    {
                        CPF.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                        textoaviso.Text = "CPF inválido";
                        textoaviso.Visibility = Visibility.Visible;
                        break;
                    }
                    ok = true;
                }
                if (ok)
                {
                    CPF.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                }
            }
            else if (CPF.Text.Length < 11)
            {
                CPF.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 122, 122));
                textoaviso.Visibility = Visibility.Collapsed;
            }
        }

        private void Senha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            Senha.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 122, 122));
            if (Senha.Password.Length == 4)
            {
                senhaaviso.Visibility = Visibility.Collapsed;
                byte[] asciiBytes = Encoding.ASCII.GetBytes(Senha.Password);
                foreach (byte i in asciiBytes)
                {
                    ok = false;
                    if (i < 48 || i > 57)
                    {
                        Senha.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                        senhaaviso.Text = "Senha inválida";
                        senhaaviso.Visibility = Visibility.Visible;
                        break;
                    }
                    ok = true;
                }
                if (ok)
                {
                    Senha.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                }
            }
            else if (Senha.Password.Length < 4)
            {
                Senha.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 122, 122));
                senhaaviso.Visibility = Visibility.Collapsed;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Page_Sobre));
        }


    } 
}
