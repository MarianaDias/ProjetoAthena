using System;
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
        }

        void Loga(IAsyncResult resultado)
        {            
            if (!App.DataConexao.Erro)
            {
                var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { this.Frame.Navigate(typeof(Pages.Page_Livros)); });                
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

        private void logar_Click(object sender, RoutedEventArgs e)
        {
            App.DataConexao.RetornarLivros(Loga);
            App.DataConexao.Usuario = CPF.Text;
            App.DataConexao.Senha = Senha.Password;
        }

        private void lembrar_Click(object sender, RoutedEventArgs e)
        {
            App.DataConexao.Usuario = CPF.Text;
            App.DataConexao.Senha = Senha.Password;
            App.Check = true;
        }
    }
}
