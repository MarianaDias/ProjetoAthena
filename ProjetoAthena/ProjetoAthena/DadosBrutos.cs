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
        private string status;
        //private DateTime[] dataDevolucao;
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
                dataDevolucao = new DateTime[4];
                for (int i = 0; i < 4; i++)
                {
                    dataDevolucao[i] = new DateTime(2000 + Convert.ToInt32(stringDevolucao[i].Substring(6)), Convert.ToInt32(stringDevolucao[i].Substring(3, 2)), Convert.ToInt32(stringDevolucao[i].Substring(0, 2)));
                    //CheckStatus();
                }
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
                }                
            }
        }

        /*
        public DateTime[] DataDevolucao
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
                    for (int i=0; i < 4; i++)
                    {
                        stringDevolucao[i] = dataDevolucao[i].Day.ToString("00") + "/" + dataDevolucao[i].Month.ToString("00") + "/" + dataDevolucao[i].Year.ToString("00");
                       // CheckStatus();
                    }
                }
                dataDevolucao = value;
            }
        }
        */

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

        /*private void CheckStatus()
        {
            int totalDias = (dataDevolucao - DateTime.Now).Days;
            if (totalDias >= 7)
            {
                Status = "Renovado";
            }
            else if (totalDias < 7 && totalDias > 0)
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
        }*/

    }
}
