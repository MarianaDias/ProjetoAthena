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

namespace ProjetoAthena
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        
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
                    usuario = LoadEncryptedText(App.UsernameID);
                return usuario;
            }
            set
            {
                usuario = value;
                SaveEncryptedText(usuario, App.UsernameID);
            }
        }

        private static string senha = null;

        public static string Senha
        {
            get
            {
                if (senha == null)
                    senha = LoadEncryptedText(App.PasswordID);
                return senha;
            }
            set
            {
                senha = value;
                SaveEncryptedText(senha, App.PasswordID);
            }
        }

        public static string UsernameID = "Username";
        public static string PasswordID = "Password";
        public static string BooksID = "Books";

        private static string filePath = "EncryptedFile";
        //Algorithm to provid a key to encryption
        //Use AES, CBC mode with PKCS#7 padding (good default choice)
        private static SymmetricKeyAlgorithmProvider aesCbcPkcs7 = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
        //Create an AES 128-bit (16 byte) key
        static CryptographicKey key = aesCbcPkcs7.CreateSymmetricKey(CryptographicBuffer.GenerateRandom(16));

        //Create a 16 byte initialization vector
        static IBuffer iv = CryptographicBuffer.GenerateRandom(aesCbcPkcs7.BlockLength);


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
        }

        /// <summary>
        /// Load an encrypted text to the local settings of the application
        /// </summary>
        /// <param name="textToEncrypt">Text to be encrypted</param>
        /// <param name="identifier">String that is used to identifie the text that will be encrypted</param>
        private static void SaveEncryptedText(string textToEncrypt, string identifier)
        {
            
            var settings = ApplicationData.Current.LocalSettings;
                  
            if (!settings.Values.ContainsKey(identifier))
            {
                settings.Values.Add(identifier, true);
            }
            else
            {
                settings.Values[identifier] = true;
            }
            
            //Encrypt the data
            // Convert the PIN to a byte[].
            byte[] PinByte = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] EncryptedPinByte = CryptographicEngine.Encrypt(key,PinByte.AsBuffer(),iv).ToArray();

            // Create a file in the application's isolated storage.
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream writestream = new IsolatedStorageFileStream(filePath + identifier, System.IO.FileMode.Create, System.IO.FileAccess.Write, file);

            // Write pinData to the file.
            Stream writer = new StreamWriter(writestream).BaseStream;
            writer.Write(EncryptedPinByte, 0, EncryptedPinByte.Length);            
        }

        /// <summary>
        /// Load an encrypted text to the local settings of the application
        /// </summary>
        /// <param name="identifier">String that is used to identifie the data that will be loaded</param>
        /// <returns></returns>
        private static string LoadEncryptedText(string identifier)
        {
            bool saved = false;
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(identifier))
            {
                saved = Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values[identifier]);
            }
            string DecryptedPinByte = null;

            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            if (saved && file.FileExists(filePath + identifier))
            {
                IsolatedStorageFileStream readStream = new IsolatedStorageFileStream(filePath + identifier, System.IO.FileMode.Open,FileAccess.Read, file);

                Stream reader = new StreamReader(readStream).BaseStream;

                byte[] EncryptedPinByte = new byte[reader.Length];
                reader.Read(EncryptedPinByte, 0, EncryptedPinByte.Length);
                

                //Decrypt the data
                DecryptedPinByte = new string(Encoding.UTF8.GetChars(CryptographicEngine.Decrypt(key,EncryptedPinByte.AsBuffer(),iv).ToArray()));
            }
            return DecryptedPinByte;
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
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
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
            deferral.Complete();
        }
    }
}
