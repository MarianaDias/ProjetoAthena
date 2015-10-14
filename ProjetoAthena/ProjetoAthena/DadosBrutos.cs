using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAthena
{
    class DadosBrutos
    {
        private string[] id;
        private string[] titulo;
        private string[] stringDevolucao;
        private string[] status = new string[4];
        private DateTime[] dataDevolucao = new DateTime[4];
        private bool[] reservado;

        public string[] Id
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
                }                
            }
        }

        public string[] Titulo
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
                }
            }
        }

        public string[] StringDevolucao
        {
            get
            {
                return stringDevolucao;
            }

            set
            {                
                stringDevolucao = value;
                Separa();                
            }
        }
        
        public void Separa()
        {            
            for (int i = 0; i<4;i++)
            {
                if (stringDevolucao[i] != null)
                {
                    dataDevolucao[i] = new DateTime(2000 + Convert.ToInt32(stringDevolucao[i].Substring(6)), Convert.ToInt32(stringDevolucao[i].Substring(3,2)) , Convert.ToInt32(stringDevolucao[i].Substring(0,2)));
                    CheckStatus(i);
                }
            }
        }


        public string[] Status
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
                }                
            }
        }

        public DateTime[] DataDevolucao
        {
            get
            {
                return dataDevolucao;
            }            
        }
        
        public bool[] Reservado
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
        
        private void CheckStatus(int index)
        {
            int totalDias = (dataDevolucao[index] - DateTime.Now).Days;
            if (totalDias >= 7)
            {
                Status[index] = "Renovado";
            }
            else if (totalDias < 7 && totalDias > 0)
            {
                Status[index] = "Devolver em " + totalDias + "dias";
            }
            else if (totalDias < 0)
            {
                Status[index] = "Vencido";
            }
            else if (totalDias == 0)
            {
                Status[index] = "Devolver hoje";
            }
            if (Reservado[index])
            {
                Status[index] = "Reservado";
            }
        }
        
    }
}
