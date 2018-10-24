using Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC_TutelaProvisoria.WebCrawler.PageObject
{
    public abstract class TJMG_PageObject : DriverFactory
    {   
        public void BuscarJurisprudencia(string Pesquisa)
        {
            PreencheCampo(Pesquisa, By.Id("palavras"));
            ClicaBotao(By.XPath("//*[@id='acordao-form']/div[2]/button"));
        }

        public string ObterInteiroTeor()
        {
            return "";
        }

        public string ObterQuantidadePaginas()
        {
            return "";
        }
    }
}
