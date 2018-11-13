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
            Jurisprudencias = new List<Jurisprudencia>();
        }

        public void PreenchendoListaArtigos()
        {
            bool SaoArtigos = false;
            int CountArtigos = 0;
            //    gerais contidas nos artigos 194 e 195.    Desta 
            foreach (var j in Jurisprudencias)
            {
                StringBuilder sb = new StringBuilder();

                var PalavrasDaJuris = j.InteiroTeor.Split(' ');
                var PreviousWord = PalavrasDaJuris.ElementAt(0);

                foreach(var CurrentWord in PalavrasDaJuris)
                {
                    if (SaoArtigos)
                    {
                        if (CurrentWord.EndsWith(".") || CurrentWord.Equals(".") || 
                            CurrentWord.EndsWith(")") || CurrentWord.Equals(")") ||
                            CurrentWord.EndsWith(":") || CurrentWord.Equals(":") ||
                            CurrentWord.EndsWith(@"\") || CurrentWord.Equals(@"\") ||
                            CurrentWord.EndsWith("\"") || CurrentWord.Equals("\"") ||
                            CurrentWord.EndsWith("\n") || CurrentWord.EndsWith("\r") ||
                            CountArtigos > 8)                                               //Precisa de um maximo, de ler 8 palavras após achar um "ARTIGOS"
                        {
                            sb.Append(CurrentWord);
                            SaoArtigos = false;
                            CountArtigos = 0;
                            sb.Clear();
                        }
                        else
                        {
                            sb.Append(CurrentWord);
                            CountArtigos++;
                        }
                    }

                    else
                    {
                        if (PreviousWord.Equals("ARTIGOS") && Util.PalavraContemDigito(CurrentWord))  //Se a palavra anterior é "ARTIGOS" e a atual contem numeros
                        {
                            SaoArtigos = true;

                            sb.Append(CurrentWord);
                        }

                        else if (PreviousWord.Contains("ART.") || PreviousWord.Equals("ARTIGO") || (PreviousWord.Equals("ART")))
                        {
                            if (Util.PalavraContemDigito(CurrentWord))             //Só adicionará a lista de artigos caso a palavra posterior contenha um numero (Ex: Art. 23)
                            {
                                j.ListaArtigos.Add(CurrentWord);
                            }
                        }
                    }

                    PreviousWord = CurrentWord.ToUpper();
                }

            }
        }

        public string ShowPesquisa()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Jurisprudencia j in Jurisprudencias)
            {
                sb.Append(j.ShowJurisprudencia());
                sb.Append("\n\n\n");
            }
            return "Pesquisa realizada: " + Pesquisa + "\n" +
                   "Foram obtidas " + Jurisprudencias.Count + " jurisprudencias" + "\n\n\n\n" +
                   sb.ToString();
        }
    }
}
