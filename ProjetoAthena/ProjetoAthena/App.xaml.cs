using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Text;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

namespace ProjetoAthena
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private static MainViewModel viewModel = null;
        public static MainViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = new MainViewModel();
                }
                return viewModel;
            }
        }
        private static AthenaData dataConexao = null;
        public static AthenaData DataConexao
        {
            get
            {
                if (dataConexao == null)
                    dataConexao = new AthenaData();
                return dataConexao;
            }
        }
        private static string usuario = null;

        public static string Usuario
        {
            get
            {
                if (usuario == null)
                    usuario = LoadText(App.UsernameID);
                return usuario;
            }
            set
            {
                usuario = value;
                SaveText(usuario, App.UsernameID);
            }
        }

        private static string senha = null;

        public static string Senha
        {
            get
            {
                if (senha == null)
                    senha = LoadText(App.PasswordID);
                return senha;
            }
            set
            {
                senha = value;
                SaveText(senha, App.PasswordID);
            }
        }
        private static bool netWorkAvailable = false;
        public static bool NetWorkAvailable
        {
            get
            {
                return netWorkAvailable;
            }

            set
            {
                netWorkAvailable = value;
            }
        }
        private static bool check = false;
        public static bool Check
        {
            
            set
            {
                check = value;
            }
        }


        public static string UsernameID = "Username";
        public static string PasswordID = "Password";
        public static string BooksID = "Books";

        
        private static string filePath = "EncryptedFile";

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            netWorkAvailable = NetworkInterface.GetIsNetworkAvailable();                        
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            netWorkAvailable = NetworkInterface.GetIsNetworkAvailable();
        }

       
        /// <summary>
        /// Load an encrypted text to the local settings of the application
        /// </summary>
        /// <param name="textToEncrypt">Text to be encrypted</param>
        /// <param name="identifier">String that is used to identifie the text that will be encrypted</param>
        private static void SaveText(string textToSave, string identifier)
        {            
            var settings = ApplicationData.Current.LocalSettings;
                  
            if (!settings.Values.ContainsKey(identifier))
            {
                settings.Values.Add(identifier, textToSave);
            }
            else
            {
                settings.Values[identifier] = textToSave;
            }            
        }

        /// <summary>
        /// Load an encrypted text to the local settings of the application
        /// </summary>
        /// <param name="identifier">String that is used to identifie the data that will be loaded</param>
        /// <returns></returns>
        private static string LoadText(string identifier)
        {
            string result = "";
            var applicationData = Windows.Storage.ApplicationData.Current;
            var localSettings = applicationData.LocalSettings;
            if (localSettings.Values.ContainsKey(identifier))
            {
                var content = localSettings.Values[identifier];
                result = Convert.ToString(content);
            }            
            return result;
        }

        public static void Uncheck()
        {
            DeletedEncryptedText(UsernameID);
            DeletedEncryptedText(PasswordID);
        }
        /// <summary>
        /// Deletes the Encrypted text saved in the local settings of the application
        /// </summary>
        /// <param name="identifier">String that is used to identifie the data that will be removed</param>
        private static void DeletedEncryptedText(string identifier)
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (settings.Values.ContainsKey(identifier))
            {
                settings.Values.Remove(identifier);
            }

            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            if (file.FileExists(filePath + identifier))
            {
                file.DeleteFile(filePath + identifier);
            }
            usuario = null;
            senha = null;
        }
        private static void SaveBooks()
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (!settings.Values.ContainsKey(BooksID))
            {
                settings.Values.Add(BooksID,App.ViewModel.Items);

            }
            else
            {
                settings.Values[BooksID] = App.ViewModel.Items;
            }

        }

        private static void RemoveBooks()
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (settings.Values.ContainsKey(BooksID))
            {
                settings.Values.Remove(BooksID);
            }
            viewModel.IsDataLoaded = false;
        }
      
        public static void Logout()
        {
            DeletedEncryptedText(UsernameID);
            DeletedEncryptedText(PasswordID);
            RemoveBooks();
            dataConexao = null;
            App.ViewModel.Items.Clear();
        }

        
       
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter

                // Faz login automático caso 
                if(Usuario != "" && Senha != "")
                {
                    App.DataConexao.Usuario = Usuario;
                    App.DataConexao.Senha = Senha;
                    rootFrame.Navigate(typeof(Pages.Page_Livros), e.Arguments);
                }
                else
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }                
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }
        
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            if (check)
            {
                Usuario = DataConexao.Usuario;
                Senha = DataConexao.Senha;
            }
            deferral.Complete();               
        }        
    }
}
