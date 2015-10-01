using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ProjetoAthena
{    
    class AthenaDataTeste
    {
        private string usuario = "44270814870";
        public string Usuario
        {
            get
            {
                return usuario;
            }
        }
        private string senha = "2408";
        public string Senha
        {
            get
            {
                return senha;
            }
        }

        private string token;
        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }

        private WebRequest webRequest;

        public void Logar(TextBox caixa) {
            webRequest = WebRequest.Create("http://www.athena.biblioteca.unesp.br/F/?func=BOR-INFO");
            webRequest.Headers["Connection"] = "Keep-Alive";
            if (webRequest != null)
            {
                caixa.Text = webRequest.ToString();
            }


        }
    }
}
