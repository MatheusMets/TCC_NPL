using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC_TutelaProvisoria
{
    public class Tutela
    {
        string nome;
        string caminho;
        string texto;
        bool aprovada;
        Dictionary<string, int> quantPalavrasDaBOW;

        public Tutela(string nome, string caminho, string texto)
        {
            this.nome = nome;
            this.caminho = caminho;
            this.texto = texto;
        }


        public Tutela()
        {

        }


        #region [Get Set]

        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        public string Caminho
        {

            get
            {
                return caminho;
            }
            set
            {
                caminho = value;
            }

        }
        
        public string Texto
        {
            get
            {
                return texto;
            }
            set
            {
                texto = value;
            }
        }

        public bool Aprovada
        {
            get
            {
                return aprovada;
            }
            set
            {
                aprovada = value;
            }
        }

        public Dictionary<string, int> QuantPalavrasDaBOW
        {
            get
            {
                return quantPalavrasDaBOW;
            }
            set
            {
                quantPalavrasDaBOW = value;
            }
        }

        #endregion


    }
}
