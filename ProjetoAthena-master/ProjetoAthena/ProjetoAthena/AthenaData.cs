using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace ProjetoAthena
{
    class AthenaData
    {
        //sites para conexão
        private string siteBaseAthena = "https://www.athena.biblioteca.unesp.br/F/";
        private string siteAthena = "http://www.athena.biblioteca.unesp.br/F/?func=BOR-INFO";
        private string siteLoginAthena = "http://www.athena.biblioteca.unesp.br/F/ID_TOKEN?func=BOR-INFO&ssl_flag=Y&func=login-session&login_source=LOGIN-BOR&bor_id=43275785842&bor_verification=1819&bor_library=UEP50";
        //Informações do usuário
        private string usuario;
        private string senha;
        private string token;

        public string SiteBaseAthena
        {
            get
            {
                return siteBaseAthena;
            }
        }
        public string SiteAthena
        {
            get
            {
                return siteAthena;
            }
        }
        public string SiteLoginAthena
        {
            get
            {
                return siteLoginAthena.Replace("ID_USUARIO", usuario).Replace("ID_SENHA", senha).Replace("ID_TOKEN", token);
            }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value;}
        }

        public string Senha
        {
            get { return senha; }
            set { senha = value;}
        }
        public string Token
        {
            get { return token; }
            set { token = value;}
        }

        private bool senhaIncorreta = false;

        public bool SenhaIncorreta
        {
            get { return senhaIncorreta; }
            set { senhaIncorreta = value; }
        }

        private bool erro = false;

        public bool Erro
        {
            get { return erro; }
            set { erro = value; }
        }

        private bool usuarioLogado = false;

        public bool UsuarioLogado
        {
            get { return usuarioLogado; }
            set { usuarioLogado = value; }
        }

        private WebRequest webRequest;
        private DateTime horaLogin = DateTime.Now;
        private AsyncCallback callbackLogin, callbackLivros, callbackRenovar;

        public void LogarUsuario(AsyncCallback callback)
        {
            try
            {
                // Verifica se a diferença entre o login efetuado e quando for usado é de 15 minutos
                if (((DateTime.Now.Minute - horaLogin.Minute) < 15) && !erro && !senhaIncorreta && usuarioLogado)
                {
                    callback(null);
                }
                else
                {
                    callbackLogin = callback;
                    
                    webRequest = WebRequest.Create(SiteAthena);
                    webRequest.Headers["Connection"] = "Keep-Alive";                   
                   webRequest.BeginGetResponse(LogarUsuarioCallback, webRequest);
                }
            }
            catch (Exception e)
            {
                callback(null);
                erro = true;
            }
        }

        private void LogarUsuarioCallback(IAsyncResult resultado)
        {
            try
            {
                webRequest = resultado.AsyncState as HttpWebRequest;
                if (webRequest != null)
                {
                    HttpWebResponse response = (HttpWebResponse)webRequest.EndGetResponse(resultado);
                    Stream streamResponse = response.GetResponseStream();
                    StreamReader streamRead = new StreamReader(streamResponse);
                    string responseString = streamRead.ReadToEnd();
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(responseString);
                    HtmlNode link = doc.DocumentNode.Descendants().Where(n => n.Name == "form").FirstOrDefault();
                    string tokenLogin = "";
                    if (link != null)
                    {
                        tokenLogin = link.Attributes["action"].Value;
                        tokenLogin = tokenLogin.Remove(0, tokenLogin.IndexOf("/F/") + 3);
                        Token = tokenLogin;

                        webRequest = WebRequest.Create(SiteLoginAthena);
                        webRequest.Headers["Connection"] = "Keep-Alive";
                        webRequest.BeginGetResponse(LoginExecutadoCallback, webRequest);
                    }
                    else
                    {
                        erro = true;
                        callbackLogin(null);
                    }
                }
                else
                {
                    erro = true;
                    callbackLogin(null);
                }
            }
            catch (WebException e)
            {
                erro = true;
                callbackLogin(null);
            }
        }

        private void LoginExecutadoCallback(IAsyncResult resultado)
        {
            try
            {
                webRequest = resultado.AsyncState as HttpWebRequest;
                if (webRequest != null)
                {
                    HttpWebResponse response = (HttpWebResponse)webRequest.EndGetResponse(resultado);
                    Stream streamResponse = response.GetResponseStream();
                    StreamReader streamRead = new StreamReader(streamResponse);
                    string responseString = streamRead.ReadToEnd();
                    if (responseString.Contains("<!-- filename: login-session-uep01 -->"))
                    {
                        erro = true;
                        senhaIncorreta = true;
                        callbackLogin(null);
                    }
                    else
                    {
                        erro = false;
                        senhaIncorreta = false;
                        usuarioLogado = true;
                        horaLogin = DateTime.Now;
                        callbackLogin(resultado);
                    }
                }
                else
                {
                    erro = true;
                    callbackLogin(null);
                }
            }
            catch (Exception e)
            {
                erro = true;
                callbackLogin(null);
            }
        }

    }
}


