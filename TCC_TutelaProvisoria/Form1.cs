using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace TCC_TutelaProvisoria
{
    public partial class Form1 : Form
    {

        public Word.Application wordDoc;
        public Word.Document doc;
        public StringBuilder data = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Open the .docx file: ",
                DefaultExt = "docx",
                Filter = "docx files(*.docx)|*.docx| txt files(*.txt)|*.txt| All files(*.*)|*.*"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                wordDoc = new Word.Application();
                //wordDoc.Visible = true;           //Abre o arquivo no pc quando der o Open
                doc = wordDoc.Documents.Open(filePath, ReadOnly: true);

                //wordDoc.Selection.Document.Content.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (wordDoc != null)
            {
                string s = Util.GetAllText(doc, data);
                MessageBox.Show(s);
                Console.WriteLine(s);
            }


            //if (wordDoc != null && TextoPesquisado.Text != String.Empty)
            //    Util.SelectionFind(wordDoc, TextoPesquisado.Text);
        }
    }
}
