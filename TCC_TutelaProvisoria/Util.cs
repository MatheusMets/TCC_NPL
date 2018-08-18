using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;

namespace TCC_TutelaProvisoria
{
    public static class Util
    {

        public static void SelectionFind(Word.Application Doc, object findText)
        {
            Doc.Selection.Find.ClearFormatting();

            object missing = null;

            if (Doc.Selection.Find.Execute(ref findText,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing))
            {
                MessageBox.Show("Texto encontrado!");
            }
            else
            {
                MessageBox.Show("Texto não localizado");
            }
        }

        public static SqlConnection RetornaConexao(string StrConnection)
        {
            SqlConnection Con1 = new SqlConnection(StrConnection);
            return Con1;
        }

        public static bool IsArquivoWord(string CaminhoDoDocumento)
        {
            if (CaminhoDoDocumento.EndsWith(".docx") ||
                CaminhoDoDocumento.EndsWith(".doc") ||
                CaminhoDoDocumento.EndsWith(".dot") ||
                CaminhoDoDocumento.EndsWith(".dotx") ||
                CaminhoDoDocumento.EndsWith(".dotm"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static Word.Document GerarInstanciaDocumento(string CaminhoDoDocumento)
        {
            if (IsArquivoWord(CaminhoDoDocumento))
            {
                Word.Application wordDoc = new Word.Application();
                Word.Document doc = wordDoc.Documents.Open(CaminhoDoDocumento, ReadOnly: true, Visible: false);

                return doc;
            }

            return null;

        }

        public static string RetornaOTextoDeUmArquivoDocx(Word.Document Doc, StringBuilder TextoDoArquivo)
        {
            try
            {
                TextoDoArquivo = new StringBuilder();

                for (int i = 0; i < Doc.Paragraphs.Count; i++)
                {
                    string temp = Doc.Paragraphs[i + 1].Range.Text.Trim();
                    if (temp != string.Empty)
                    {
                        TextoDoArquivo.Append(temp);
                        TextoDoArquivo.Append(" ");
                    }
                }

                Doc.Close();
                return TextoDoArquivo.ToString();
            }

            catch (Exception)
            {
                MessageBox.Show("Primeiro carregue uma arquivo word para poder le-lo");
                return null;
            }

            //return Doc.Selection.Find.Text;
        }

        public static string[] RetornaTodosOsCaminhosDeArquivosBaseadoNumaPasta(string CaminhoDaPasta)
        {
            string[] DocPaths;

            if (!String.IsNullOrEmpty(CaminhoDaPasta))          //Se ele nao for nula nem vazia, pega o caminho de cada arquivo
            {
                DocPaths = new string[Directory.GetFiles(CaminhoDaPasta).Length - 1];
                DocPaths = Directory.GetFiles(CaminhoDaPasta);
                return DocPaths;
            }
            else
            {
                return null;
            }
        }

        public static List<Tutela> RetornaTodosOsTextosDeArquivosDocx(string[] CaminhosDosDocumentos)
        {
            Tutela tutela = new Tutela();
            List<Tutela> ListaDeTutelas = new List<Tutela>();

            foreach (string caminho in CaminhosDosDocumentos)
            {
                if (Util.IsArquivoWord(caminho))
                {
                    tutela = new Tutela
                    {
                        Caminho = caminho,
                        Nome = Path.GetFileName(caminho)
                    };

                    ListaDeTutelas.Add(tutela);

                    //MessageBox.Show(caminho);
                }
            }

            Word.Document doc;
            StringBuilder data = new StringBuilder();

            foreach (Tutela T in ListaDeTutelas)
            {
                doc = Util.GerarInstanciaDocumento(T.Caminho);

                if (doc != null)
                    T.Texto = RetornaOTextoDeUmArquivoDocx(doc, data);
            }

            return ListaDeTutelas;
        }

        public static List<string> RetornaBagOfWords(List<Tutela> ListaDeTutelas)
        {

            List<string> TodasAsPalavrasDoBagOfWords = new List<string>();
            List<string> PalavrasDeUmaTutela = new List<string>();
            string PalavraParaEntrarNoBagOfWords;
            bool JaEstaNaBagOfWords = false;

            foreach (Tutela tutela in ListaDeTutelas)                                                       //Verifica todas as string que tem textos de tutelas
            {
                PalavrasDeUmaTutela.Clear();

                PalavrasDeUmaTutela = tutela.Texto.Split(' ').ToList();

                foreach (string PalavraParaEntrarNoBagOfWordsBase in PalavrasDeUmaTutela)                   //Verifica todas as palavras dentro de uma tutela lida
                {
                    JaEstaNaBagOfWords = false;
                    PalavraParaEntrarNoBagOfWords = RemovePontuacaoDaPalavra(PalavraParaEntrarNoBagOfWordsBase);        //Retirando a pontuacao das palavras

                    if (TodasAsPalavrasDoBagOfWords.Count == 0)
                    {
                        TodasAsPalavrasDoBagOfWords.Add(PalavraParaEntrarNoBagOfWords.ToLower());            //Todas as palavras devem ser adicionadas em LowerCase
                    }
                    else
                    {
                        foreach (string PalavraDoBagOfWords in TodasAsPalavrasDoBagOfWords)                  //Verifica se palavra ja esta na bag of words
                        {
                            if (PalavraParaEntrarNoBagOfWords.ToLower().Equals(PalavraDoBagOfWords))
                            {
                                JaEstaNaBagOfWords = true;
                                break;
                            }
                        }

                        if (!JaEstaNaBagOfWords && !String.IsNullOrEmpty(PalavraParaEntrarNoBagOfWords))    //Se a palavra não está na bag of words, adiciona
                        {
                            TodasAsPalavrasDoBagOfWords.Add(PalavraParaEntrarNoBagOfWords.ToLower());
                        }

                    }

                }
            }

            return TodasAsPalavrasDoBagOfWords;
        }

        public static List<string> RemovePontuacaoDeUmaListaDeString(List<string> PalavrasDeUmaTutela)
        {
            List<string> PalavrasDeUmaTutelaTemp = new List<string>();

            foreach (string PalavraTemp in PalavrasDeUmaTutela)
            {
                var NovaPalavra = new StringBuilder();

                foreach (char c in PalavraTemp)
                {
                    if (!char.IsPunctuation(c) && !char.IsSymbol(c) && !char.IsWhiteSpace(c))
                        NovaPalavra.Append(c);
                }

                if (!String.IsNullOrWhiteSpace(NovaPalavra.ToString()))
                {
                    PalavrasDeUmaTutelaTemp.Add(NovaPalavra.ToString());
                }
                
            }

            return PalavrasDeUmaTutelaTemp;
        }

        public static string QuantidadePalavrasPorTutela(List<Tutela> ListaDeTutelas, List<string> BagOfWords)
        {
            List<string> PalavrasDeUmaTutelaTemp = new List<string>();
            List<string> PalavrasDeUmaTutela = new List<string>();
            StringBuilder Relatorio = new StringBuilder();
            int Count;
            string NomeTutelaAtual = String.Empty;

            Relatorio.Append("RELATORIO: QUANTIDADE DE PALAVRAS QUE SE REPETEM POR TUTELA \n\n\n");

            foreach (string PalavraBagOfWords in BagOfWords)
            {
                Relatorio.Append("/******  PALAVRA SENDO ANALISADA: \"" + PalavraBagOfWords + "\"  ******/ \n");

                foreach (Tutela tutela in ListaDeTutelas)
                {
                    Count = 0;

                    PalavrasDeUmaTutelaTemp = tutela.Texto.Split(' ').ToList();
                    PalavrasDeUmaTutelaTemp = PalavrasDeUmaTutelaTemp.ConvertAll(d => d.ToLower());
                    PalavrasDeUmaTutela = RemovePontuacaoDeUmaListaDeString(PalavrasDeUmaTutelaTemp);

                    foreach (string palavra in PalavrasDeUmaTutela)
                    {
                        if (palavra.Equals(PalavraBagOfWords))
                            Count++;
                    }

                    NomeTutelaAtual = tutela.Nome;

                    //Console.WriteLine("Palavra " + PalavraBagOfWords + " foi encontrada " + Count + " vezes na tutela " + NomeTutelaAtual);
                    Relatorio.Append("Palavra \"" + PalavraBagOfWords + "\" foi encontrada " + Count + " vezes na tutela \"" + NomeTutelaAtual + "\"\n");
                }

                Relatorio.Append("\n\n");
                //Console.WriteLine("\n\n");
            }

            return Relatorio.ToString();

        }

        public static string RemovePontuacaoDaPalavra(string palavra)
        {
            var NovaPalavra = new StringBuilder();

            foreach (char c in palavra)
            {
                if (!char.IsPunctuation(c) && !char.IsSymbol(c) && !char.IsWhiteSpace(c))
                    NovaPalavra.Append(c);
            }
            
            return NovaPalavra.ToString();
        }

        //public static Dictionary<string, int> Retorna()
        //{
        //    Dictionary<string, int> 

        //}

    }
}
