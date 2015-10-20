using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace ProjetoAthena
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get;
            private set;
        }

        private string sampleProperty = "Sample Runtime Property Value";
        public string SampleProperty
        {
            get
            {
                return sampleProperty;
            }

            set
            {
                if (value != sampleProperty)
                {
                    sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
                sampleProperty = value;
            }
        }
        /*
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }
        */
        public bool IsDataLoaded
        {
            get;
            set;
        }

        private AsyncCallback callbackPage;

        public void LoadData(AsyncCallback callback)
        {
            callbackPage = callback;
            App.DataConexao.RetornarLivros(RetLivrosCallback);
        }

        public void LoadBooks()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(App.BooksID))
            {
                App.ViewModel.Items = ApplicationData.Current.LocalSettings.Values[App.BooksID] as ObservableCollection<ItemViewModel>;
            }
            IsDataLoaded = true;
        }
        public void RetLivrosCallback(IAsyncResult resultado)
        {
            this.IsDataLoaded = true;
            callbackPage(resultado);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
