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
using System.IO;

namespace TCC_TutelaProvisoria
{
    public partial class Form1 : Form
    {
        //public Word.Application wordDoc;
        public Word.Document doc;
        public StringBuilder data = new StringBuilder();
        string FolderPath;
        string DocPath;
        string[] DocPaths;
        List<string> FilesFromFolder;

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
                Filter = "docx files(*.docx)|*.docx|doc files(*.doc)|*.doc|All files(*.*)|*.*"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DocPath = openFileDialog1.FileName;

                //wordDoc = new Word.Application();
                //wordDoc.Visible = true;           //Abre o arquivo no pc quando der o Open
                doc = Util.GenerateDocument(DocPath);

                //wordDoc.Selection.Document.Content.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                string s = Util.GetAllText(doc, data);
                Console.WriteLine(s);
                MessageBox.Show(s);

                //richTextBox1 = new RichTextBox();

                ////richTextBox1.Dock = DockStyle.Fill;
                //richTextBox1.AppendText(s);
                //richTextBox1.Show();
            }


            //if (wordDoc != null && TextoPesquisado.Text != String.Empty)
            //    Util.SelectionFind(wordDoc, TextoPesquisado.Text);
        }

        private void pegarCaminhoDaPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilesFromFolder = new List<string>();
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "To get a folder path: "; 

            if (folderDialog.ShowDialog() == DialogResult.OK)
                FolderPath = folderDialog.SelectedPath;

            MessageBox.Show("Caminho da pasta: " + FolderPath);

            if (!String.IsNullOrEmpty(FolderPath))          //Se ele nao for nula nem vazia, pega o caminho de cada arquivo
            {
                DocPaths = new string[Directory.GetFiles(FolderPath).Length - 1];
                DocPaths = Directory.GetFiles(FolderPath);
            }

            ////Testando se ta pegando o caminho certo dos arquivos (ESTÁ!)
            //foreach (string path in DocPaths)
            //{
            //    MessageBox.Show(path);
            //}

            if(DocPaths != null)
                FilesFromFolder = Util.GetAllTextFromFilesInAFolder(DocPaths);

            foreach (string path in FilesFromFolder)
            {
                MessageBox.Show(path);
            }
        }
    }
}
