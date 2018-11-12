using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC_TutelaProvisoria.Entities
{
    public class Artigo
    {
        public string NomeArtigo { get; set; }



        public Artigo(string nomeArtigo)
        {
            this.NomeArtigo = nomeArtigo;
        }

        public string ShowArtigo()
        {
            return "ARTIGO: " + NomeArtigo + "\n"
                   ;
        }
    }
}
