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
        public List<Artigo> ListaArtigos { get; set; }

        public Jurisprudencia()
        {
            ListaArtigos = new List<Artigo>();
        }

        public Jurisprudencia(string Processo, string Relator, string OrgaoJulgador, string Sumula, string DataJulgamento, string DataPublicacao, string Ementa, string InteiroTeor)
        {
            this.Processo = Processo;
            this.Relator = Relator;
            this.OrgaoJulgador = OrgaoJulgador;
            this.Sumula = Sumula;
            this.DataJulgamento = DataJulgamento;
            this.DataPublicacao = DataPublicacao;
            this.Ementa = Ementa;
            this.InteiroTeor = InteiroTeor;
            ListaArtigos = new List<Artigo>();
        }

        public string ShowJurisprudencia()
        {
            return "PROCESSO: " + Processo + "\n" +
                   "RELATOR: " + Relator + "\n" +
                   "ORGAO JULGADOR / CAMARA: " + OrgaoJulgador + "\n" +
                   "SUMULA: " + Sumula + "\n" +
                   "DATA DE JULGAMENTO: " + DataJulgamento + "\n" +
                   "DATA DE PUBLICACAO: " + DataPublicacao + "\n\n"
                   + "EMENTA: " + Ementa + "\n\n" +
                   "INTEIRO TEOR: " + InteiroTeor
                   ; 
        }

    }
}
