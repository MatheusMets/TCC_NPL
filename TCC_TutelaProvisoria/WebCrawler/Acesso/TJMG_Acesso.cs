using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCC_TutelaProvisoria.WebCrawler.PageObject;

namespace TCC_TutelaProvisoria.WebCrawler.Acesso
{
    public class TJMG_Acesso : TJMG_PageObject
    {
        public void AcessarTJMG()
        {
            try
            {
                InicializaBrowserAnonimo("http://www.tjmg.jus.br/portal-tjmg/");
                BuscarJurisprudencia("Alimentos critérios de fixação cônjuge");
                Thread.Sleep(30000);
                TirarPrint();
            }
            catch (Exception ex)
            {
                TirarPrint();
                MessageBox.Show("ERRO: " + ex.Message);
            }
            finally
            {
                FinalizaNavegador();
            }

        }
    }
}
