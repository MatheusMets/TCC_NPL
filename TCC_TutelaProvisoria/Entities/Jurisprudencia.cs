using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC_TutelaProvisoria.Entities
{
    public class Jurisprudencia
    {
        public string Processo { get; set; }
        public string Relator { get; set; }
        public string OrgaoJulgador { get; set; }
        public string Sumula { get; set; }
        public string DataJulgamento { get; set; }
        public string DataPublicacao { get; set; }
        public string Ementa { get; set; }
        public string InteiroTeor { get; set; }

        public Jurisprudencia()
        {

        }

        public string ShowJurisprudencia()
        {
            return "Processo: " + Processo + "\n" +
                   "Relator: " + Relator + "\n" +
                   "Orgao Julgador / Câmara: " + OrgaoJulgador + "\n" +
                   "Sumula: " + Sumula + "\n" +
                   "Data de julgamento: " + DataJulgamento + "\n" +
                   "Data de publicacao: " + DataPublicacao + "\n"
                   //+ "Ementa: " + Ementa + "\n" +
                   //"Inteiro Teor: " + InteiroTeor
                   ; 
        }

    }
}
