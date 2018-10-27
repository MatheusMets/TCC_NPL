using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCC_TutelaProvisoria.Entities;
using TCC_TutelaProvisoria.WebCrawler.PageObject;

namespace TCC_TutelaProvisoria.WebCrawler.Acesso
{
    public class TJMG_Acesso : TJMG_PageObject
    {
        PesquisaJurisprudencia pesquisaJurisprudencia;
        Jurisprudencia jurisprudencia;

        public PesquisaJurisprudencia AcessarTJMG()
        {
            int QuantJurisprudenciasObtidas = 0;
            int QuantJurisprudenciasEncontradas = 0;
            pesquisaJurisprudencia = new PesquisaJurisprudencia();

            try
            {
                InicializaBrowserAnonimo("http://www.tjmg.jus.br/portal-tjmg/");
                BuscarJurisprudencia("conjuge alimentos posse de bens");         pesquisaJurisprudencia.Pesquisa = "conjuge alimentos posse de bens";
                ClicaNaPrimeiraJurisprudencia();

                QuantJurisprudenciasEncontradas = ObterQuantJurisprudencias();

                for(QuantJurisprudenciasObtidas = 0; QuantJurisprudenciasObtidas < QuantJurisprudenciasEncontradas; QuantJurisprudenciasObtidas++)
                {
                    jurisprudencia = new Jurisprudencia();

                    jurisprudencia.Processo = ObterProcesso();
                    jurisprudencia.Relator = ObterRelator();
                    jurisprudencia.Sumula = ObterSumula();
                    jurisprudencia.InteiroTeor = ObterOrgaoJulgador();
                    jurisprudencia.DataJulgamento = ObterDataJulgamento();
                    jurisprudencia.DataPublicacao = ObterDataPublicacao();
                    jurisprudencia.Ementa = ObterEmenta();
                    jurisprudencia.InteiroTeor = ObterInteiroTeor();

                    pesquisaJurisprudencia.Jurisprudencias.Add(jurisprudencia);
                    IrParaProximaPagina();
                }
                
                return pesquisaJurisprudencia;
            }
            catch (Exception ex)
            {
                TirarPrint();
                MessageBox.Show("ERRO: \n\n" + ex.Message);

                return pesquisaJurisprudencia;
            }
            finally
            {
                MessageBox.Show("Obteve " + QuantJurisprudenciasObtidas + " jurisprudencuas de " + QuantJurisprudenciasEncontradas);
                FinalizaNavegador();
            }

        }
    }
}
