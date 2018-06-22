using System;
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

        public static string GetAllText(Word.Document Doc, StringBuilder data)
        {

            String read = string.Empty;

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
            
            //return Doc.Selection.Find.Text;
        }

    }
}
