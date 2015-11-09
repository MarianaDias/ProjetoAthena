using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAthena
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string id;
        private string titulo;
        private string stringDevolucao;
        private string status;
        private DateTime dataDevolucao;
        private bool reservado;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this,new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged("ID");
                }                
            }
        }

        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                if (value != titulo)
                {
                    titulo = value;
                    NotifyPropertyChanged("Titulo");
                }
            }
        }

        public string StringDevolucao
        {
            get
            {
                return stringDevolucao;
            }

            set
            {                
                stringDevolucao = value;                
                DataDevolucao = new DateTime(2000 + Convert.ToInt32(stringDevolucao.Substring(6)), Convert.ToInt32(stringDevolucao.Substring(3, 2)), Convert.ToInt32(stringDevolucao.Substring(0, 2)));
                CheckStatus();
                NotifyPropertyChanged("StringDevolucao");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value != status)
                {
                    status = value;
                    NotifyPropertyChanged("Status");
                }                
            }
        }

        
        public DateTime DataDevolucao
        {
            get
            {
                return dataDevolucao;
            }

            set
            {
                if (value != dataDevolucao)
                {
                    dataDevolucao = value;                   
                    stringDevolucao = dataDevolucao.Day.ToString("00") + "/" + dataDevolucao.Month.ToString("00") + "/" + dataDevolucao.Year.ToString("00");
                    NotifyPropertyChanged("DataDevolucao");
                       // CheckStatus();
                    
                }
                dataDevolucao = value;
            }
        }
        

        public bool Reservado
        {
            get
            {
                return reservado;
            }

            set
            {
                reservado = value;
            }
        }

        private void CheckStatus()
        {
            int totalDias = (dataDevolucao - DateTime.Now).Days;
            if (totalDias >= 6)
            {
                Status = "Renovado";
            }
            else if (totalDias < 6 && totalDias > 0)
            {
                Status = "Devolver em " + totalDias + "dias";
            }
            else if (totalDias < 0)
            {
                Status = "Vencido";
            }
            else if (totalDias == 0)
            {
                Status = "Devolver hoje";
            }
            if (Reservado)
            {
                Status = "Reservado";
            }
        }

    }
}
