using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC_TutelaProvisoria.Entities
{
    public class PesquisaJurisprudencia
    {
        public string Pesquisa { get; set; }
        public List<Jurisprudencia> Jurisprudencias { get; set; }

        public PesquisaJurisprudencia()
        {

        }

        public string ShowPesquisa()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Jurisprudencia j in Jurisprudencias)
            {
                sb.Append(j.ShowJurisprudencia());
                sb.Append("\n\n");
            }
            return "Pesquisa realizada: " + Pesquisa + "\n" +
                   "Foram obtidas " + Jurisprudencias.Count + " jurisprudencias" + "\n\n" +
                   sb.ToString();
        }
    }
}
