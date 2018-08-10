using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

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
                ref missing, ref missing,ref missing, ref missing))
            {
                MessageBox.Show("Texto encontrado!");
            }
            else
            {
                MessageBox.Show("Texto não localizado");
            }
        }

        public static Word.Document GerarInstanciaDocumento(string DocPath)
        {
            Word.Application wordDoc = new Word.Application();
            Word.Document doc = wordDoc.Documents.Open(DocPath, ReadOnly: true, Visible: false);

            return doc;
        }

        public static string RetornaOTextoDeUmArquivoDocx(Word.Document Doc, StringBuilder TextoDoArquivo)
        {
            try { 
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

        public static List<string> RetornaTodosOsTextosDeArquivosDocx(string[] DocPaths)
        {
            List<string> ListaDeTutelas = new List<string>();
            Word.Document doc;
            StringBuilder data = new StringBuilder();

            foreach (string text in DocPaths)
            {
                doc = Util.GerarInstanciaDocumento(text);

                ListaDeTutelas.Add(RetornaOTextoDeUmArquivoDocx(doc, data));
            }

            return ListaDeTutelas;
        }

        public static List<string> RetornaBagOfWords(List<string> ListaDeTutelas)
        {
            List<string> TodasAsPalavrasDoBagOfWord = new List<string>();
            List<string> PalavrasDeUmaTutela = new List<string>();
            bool JaEstaNaBagOfWords = false;

            foreach (string Tutela in ListaDeTutelas)           //Verifica todas as string que tem textos de tutelas
            {
                PalavrasDeUmaTutela.Clear();

                PalavrasDeUmaTutela = Tutela.Split(' ').ToList();

                foreach (string PalavraParaEntrarNoBagOfWords in PalavrasDeUmaTutela)       //Verifica todas as palavras dentro de uma tutela lida
                {
                    JaEstaNaBagOfWords = false;

                    if (TodasAsPalavrasDoBagOfWord.Count == 0)
                    {
                        TodasAsPalavrasDoBagOfWord.Add(PalavraParaEntrarNoBagOfWords.ToLower());        //Todas as palavras devem ser adicionadas em LowerCase
                    }
                    else
                    {
                        foreach (string PalavraDoBagOfWords in TodasAsPalavrasDoBagOfWord)          //Verifica se palavra ja esta na bag of words
                        {
                            if (PalavraParaEntrarNoBagOfWords.ToLower().Equals(PalavraDoBagOfWords))
                            {
                                JaEstaNaBagOfWords = true;
                                break;
                            }   
                        }

                        if (!JaEstaNaBagOfWords && !String.IsNullOrEmpty(PalavraParaEntrarNoBagOfWords))                                //Se a palavra não está na bag of words, adiciona
                        {
                            RemovePontuacaoDaPalavra(PalavraParaEntrarNoBagOfWords);        //TO-DO
                            TodasAsPalavrasDoBagOfWord.Add(PalavraParaEntrarNoBagOfWords.ToLower());
                        }

                    }

                }
            }


            return TodasAsPalavrasDoBagOfWord;
        }

        public static void RemovePontuacaoDaPalavra(string palavra)
        {
            //TO-DO
        }

    }
}
