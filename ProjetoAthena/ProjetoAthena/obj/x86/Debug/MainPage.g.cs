﻿#pragma checksum "C:\Users\Matheus\Documents\Projetos\ProjetoAthena\ProjetoAthena\ProjetoAthena\ProjetoAthena\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1BF020226798C79EF063A4DF3554C634"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetoAthena
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 9 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).DoubleTapped += this.Page_DoubleTapped;
                    #line default
                }
                break;
            case 2:
                {
                    this.LayoutRoot = (global::Windows.UI.Xaml.Controls.CommandBar)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element3 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 12 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element3).Click += this.AppBarButton_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.Athena = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5:
                {
                    this.CPF = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 31 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.CPF).TextChanged += this.CPF_TextChanged;
                    #line default
                }
                break;
            case 6:
                {
                    this.Senha = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                    #line 32 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.PasswordBox)this.Senha).PasswordChanged += this.Senha_PasswordChanged;
                    #line default
                }
                break;
            case 7:
                {
                    this.textoCPF = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.textoSenha = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9:
                {
                    this.lembrar = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 35 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.lembrar).Click += this.lembrar_Click;
                    #line default
                }
                break;
            case 10:
                {
                    this.logar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 36 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.logar).Click += this.logar_Click;
                    #line default
                }
                break;
            case 11:
                {
                    this.limpar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 37 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.limpar).Click += this.limpar_Click;
                    #line default
                }
                break;
            case 12:
                {
                    this.logolivro = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 13:
                {
                    this.textoaviso = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14:
                {
                    this.senhaaviso = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

