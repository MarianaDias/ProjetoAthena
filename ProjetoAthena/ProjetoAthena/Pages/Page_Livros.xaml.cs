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
        AthenaData parametro;
        public Page_Livros()
        {
            this.InitializeComponent();
            Limpar();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parametro = e.Parameter as AthenaData;
            this.parametro = parametro;
            if (parametro.Dados.Titulo[0] != null)
                livro1.Text = parametro.Dados.Titulo[0];
            if (parametro.Dados.Titulo[1] != null)
                livro2.Text = parametro.Dados.Titulo[1];
            if (parametro.Dados.Titulo[2] != null)
                livro3.Text = parametro.Dados.Titulo[2];
            if (parametro.Dados.Titulo[3] != null)
                livro4.Text = parametro.Dados.Titulo[3];
            if (parametro.Dados.DataDevolucao[0] != null)            
                datadev1.Text = parametro.Dados.DataDevolucao[0].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[0].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[0].Year.ToString("00");
            if (parametro.Dados.DataDevolucao[1] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[1].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[1].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[1].Year.ToString("00");
            if (parametro.Dados.DataDevolucao[2] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[2].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[2].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[2].Year.ToString("00");
            if (parametro.Dados.DataDevolucao[3] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[3].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[3].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[3].Year.ToString("00");
            if (parametro.Dados.Status[0] != null)
                temporest1.Text = parametro.Dados.Status[0];
            if (parametro.Dados.Status[1] != null)
                temporest1.Text = parametro.Dados.Status[1];
            if (parametro.Dados.Status[2] != null)
                temporest1.Text = parametro.Dados.Status[2];
            if (parametro.Dados.Status[3] != null)
                temporest1.Text = parametro.Dados.Status[3];
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
            parametro.RenovarLivros(sucesso);
        }
        private void sucesso(IAsyncResult result)
        {
            if (parametro.Dados.Titulo[0] != null)
                livro1.Text = parametro.Dados.Titulo[0];
            if (parametro.Dados.Titulo[1] != null)
                livro2.Text = parametro.Dados.Titulo[1];
            if (parametro.Dados.Titulo[2] != null)
                livro3.Text = parametro.Dados.Titulo[2];
            if (parametro.Dados.Titulo[3] != null)
                livro4.Text = parametro.Dados.Titulo[3];
            if (parametro.Dados.DataDevolucao[0] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[0].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[0].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[0].Year.ToString("00");
            if (parametro.Dados.DataDevolucao[1] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[1].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[1].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[1].Year.ToString("00");
            if (parametro.Dados.DataDevolucao[2] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[2].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[2].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[2].Year.ToString("00");
            if (parametro.Dados.DataDevolucao[3] != null)
                datadev1.Text = parametro.Dados.DataDevolucao[3].Day.ToString("00") + "/" + parametro.Dados.DataDevolucao[3].Month.ToString("00") + "/" + parametro.Dados.DataDevolucao[3].Year.ToString("00");
            if (parametro.Dados.Status[0] != null)
                temporest1.Text = parametro.Dados.Status[0];
            if (parametro.Dados.Status[1] != null)
                temporest1.Text = parametro.Dados.Status[1];
            if (parametro.Dados.Status[2] != null)
                temporest1.Text = parametro.Dados.Status[2];
            if (parametro.Dados.Status[3] != null)
                temporest1.Text = parametro.Dados.Status[3];
        }
    }

}

