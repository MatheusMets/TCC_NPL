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

        public static Word.Document GenerateDocument(string DocPath)
        {
            Word.Application wordDoc = new Word.Application();
            Word.Document doc = wordDoc.Documents.Open(DocPath, ReadOnly: true);

            return doc;
        }

        public static string GetAllText(Word.Document Doc, StringBuilder data)
        {
            try { 
                string read = String.Empty;
                data = new StringBuilder();

                for (int i = 0; i < Doc.Paragraphs.Count; i++)
                {
                    string temp = Doc.Paragraphs[i + 1].Range.Text.Trim();
                    if (temp != string.Empty)
                    {
                        data.Append(temp);
                        data.Append(" ");
                    }
                }

                Doc.Close();
                return data.ToString();
            }

            catch (Exception)
            {
                MessageBox.Show("Primeiro carregue uma arquivo word para poder le-lo");
                return null;
            }
            
            //return Doc.Selection.Find.Text;
        }

        public static List<string> GetAllTextFromFilesInAFolder(string[] DocPaths)
        {
            List<string> TextFiles = new List<string>();
            Word.Document doc;
            StringBuilder data = new StringBuilder();

            foreach (string text in DocPaths)
            {
                doc = Util.GenerateDocument(text);

                TextFiles.Add(GetAllText(doc, data));
            }

            return TextFiles;
        }

    }
}
