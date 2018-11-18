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
        public List<string> ListaArtigos { get; set; }
        public string StatusJurisprudencia { get; set; }

        public Jurisprudencia()
        {
            ListaArtigos = new List<string>();
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
            ListaArtigos = new List<string>();
            PreencheStatusJurisprudencia();
        }

        public string ShowJurisprudencia()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var Artigo in ListaArtigos)
            {
                sb.Append(Artigo);
            }

            return "PROCESSO: " + Processo + "\n" +
                   "RELATOR: " + Relator + "\n" +
                   "ORGAO JULGADOR / CAMARA: " + OrgaoJulgador + "\n" +
                   "SUMULA: " + Sumula + "\n" +
                   "DATA DE JULGAMENTO: " + DataJulgamento + "\n" +
                   "DATA DE PUBLICACAO: " + DataPublicacao + "\n\n"
                   + "EMENTA: " + Ementa + "\n\n" +
                   "INTEIRO TEOR: " + InteiroTeor + "\n" +
                   "ARTIGOS ENCONTRADOS: " + sb.ToString ()
                   ; 
        }

        public void PreenchendoListaArtigos()
        {
            bool SaoArtigos = false;
            int CountArtigos = 0;
            StringBuilder sb = new StringBuilder();

            var PalavrasDaJuris = InteiroTeor.Split(' ');
            var PreviousWord = PalavrasDaJuris.ElementAt(0);

            foreach (var CurrentWord in PalavrasDaJuris)
            {
                if (SaoArtigos)
                {
                    if (CurrentWord.EndsWith(".") || CurrentWord.Equals(".") ||
                        CurrentWord.EndsWith(")") || CurrentWord.Equals(")") ||
                        CurrentWord.EndsWith(":") || CurrentWord.Equals(":") ||
                        CurrentWord.EndsWith(@"\") || CurrentWord.Equals(@"\") ||
                        CurrentWord.EndsWith("\"") || CurrentWord.Equals("\"") ||
                        CurrentWord.EndsWith("\n") || CurrentWord.EndsWith("\r") ||
                        CountArtigos > 8)                                                       //Precisa de um maximo, de ler 8 palavras após achar um "ARTIGOS"
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
                    if (PreviousWord.Contains("ARTIGOS") && Util.PalavraContemDigito(CurrentWord))  //Se a palavra anterior é "ARTIGOS" e a atual contem numeros
                    {
                        SaoArtigos = true;

                        sb.Append(CurrentWord);
                    }

                    else if (PreviousWord.Contains("ART.") || PreviousWord.Equals("ARTIGO") || (PreviousWord.Equals("ART")))
                    {
                        if (Util.PalavraContemDigito(CurrentWord))             //Só adicionará a lista de artigos caso a palavra posterior contenha um numero (Ex: Art. 23)
                        {
                            ListaArtigos.Add(CurrentWord);
                        }
                    }
                    else if (CurrentWord.StartsWith("ARTS.") || CurrentWord.StartsWith("ART.") || CurrentWord.StartsWith("art.") || CurrentWord.StartsWith("arts."))
                    {
                        if (Util.PalavraContemDigito(CurrentWord))             //Só adicionará a lista de artigos caso a palavra posterior contenha um numero (Ex: Art. 23)
                        {
                            ListaArtigos.Add(CurrentWord);
                        }
                    }
                }

                PreviousWord = CurrentWord.ToUpper();
            }

            NormalizaArtigos();
            
        }

        public void NormalizaArtigos()          //Normalizar para deixar somente números
        {
            List<string> NewListaArtigos = new List<string>();
            StringBuilder sb;

            for (int i = 0; i < this.ListaArtigos.Count; i++)
            {
                sb = new StringBuilder();

                if (!Util.PalavraContemDigito(this.ListaArtigos.ElementAt(i)))
                    ListaArtigos.RemoveAt(i);

                else
                {
                    foreach (char c in this.ListaArtigos.ElementAt(i))
                    {
                        if (char.IsDigit(c) || c.Equals('-'))
                        {
                            sb.Append(c);
                        }
                    }
                }

                NewListaArtigos.Add(sb.ToString());

            }

            this.ListaArtigos = NewListaArtigos;

        }

        public void PreencheStatusJurisprudencia()
        {
            if (!String.IsNullOrEmpty(this.Sumula))
            {
                //Se contem conjuntos X de palavras, é deferido. Senão, é indeferido... Parcialmente deferido ou indiferente. 

                /*
                    NEGARAM PROVIMENTO
                    NEGARAM PROVIMENTO AO RECURSO
                    NEGARAM PROVIMENTO AO RECURSO
                    SÚMULA: NEGARAM PROVIMENTO AO RECURSO
                    DERAM PROVIMENTO AO AGRAVO
                    DERAM PROVIMENTO
                    Em reexame necessário, reformo parcialmente a sentença
                    Dou provimento à primeira apelação, nego provimento à segunda apelação e à apelação adesiva
                    NEGARAM PROVIMENTO
                    NEGARAM PROVIMENTO
                    NEGARAM PROVIMENTO AO RECURSO
                    Negaram provimento ao recurso e de ofício integraram a decisão
                    RECURSO NÃO PROVIDO
                    DERAM PROVIMENTO AO RECURSO, VENCIDO O SEGUNDO VOGAL
                    DERAM PROVIMENTO, VENCIDO O SEGUNDO VOGAL
                    SÚMULA: DERAM PROVIMENTO AO RECURSO, VENCIDO PARCIALMENTE O SEGUNDO VOGAL
                    REJEITARAM AS PRELIMINARES E, NO MÉRITO, NEGARAM PROVIMENTO AO RECURSO
                    NEGARAM PROVIMENTO AO RECURSO, VENCIDA A DESEMBARGADORA RELATORA
                    REJEITARAM AS PRELIMINARES, POR MAIORIA, E DERAM PARCIAL PROVIMENTO AO RECURSO, À UNANIMIDADE
                    NEGARAM PROVIMENTO AO RECURSO
                    NEGARAM PROVIMENTO
                    RECURSO NÃO PROVIDO, VENCIDA A RELATORA
                    DERAM PARCIAL PROVIMENTO AO RECURSO
                    RECURSO NÃO PROVIDO, VENCIDA A RELATORA" Esteve presente o(a) Dra. Procuradora de Justiça Reyvani Jabour Ribeiro pelo(a) agravado(a)(s)
                    DERAM PROVIMENTO AO RECURSO
                    POR MAIORIA, NEGARAM PROVIMENTO AO AGRAVO, VENCIDO O RELATOR
                    RECURSO PROVIDO EM PARTE. VENCIDA A RELATORA" Esteve presente o(a) Andrea Paulino dos Santos pelo(a) agravante(s)
                    REFORMARAM A SENTENÇA, NO REEXAME NECESSÁRIO, PREJUDICADOS OS RECURSOS VOLUNTÁRIOS, VENCIDA A DESEMBARGADORA RELATORA
                    RECURSO NÃO PROVIDO, VENCIDA A RELATORA.."Esteve presente o(a) Matheus Miranda de Oliveira pelo(a) agravante(s)
                    RECURSO PROVIDO, VENCIDA A RELATORA
                    DERAM PROVIMENTO PARCIAL, POR MAIORIA
                    NEGARAM PROVIMENTO, VENCIDA A RELATORA
                    DERAM PROVIMENTO, VENCIDO O SEGUNDO VOGAL
                    REJEITARAM PRELIMINARES E DERAM PROVIMENTO AO RECURSO
             
                */



            }
        }

    }
}
