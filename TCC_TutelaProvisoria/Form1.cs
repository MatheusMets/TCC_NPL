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
        string CaminhoDaPasta;
        string CaminhoDoDocumento;
        string[] CaminhosDosDocumentos;
        List<string> ListaDeDocumentos;
        List<string> BagOfWords;

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
                CaminhoDoDocumento = openFileDialog1.FileName;

                //wordDoc = new Word.Application();
                //wordDoc.Visible = true;           //Abre o arquivo no pc quando der o Open
                doc = Util.GerarInstanciaDocumento(CaminhoDoDocumento);
                
            }

            if (doc != null)
            {
                string Tutela = Util.RetornaOTextoDeUmArquivoDocx(doc, data);
                Console.WriteLine(Tutela);
                MessageBox.Show(Tutela);
            }
        }

        private void pegarCaminhoDaPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaDeDocumentos = new List<string>();
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                Description = "To get a folder path: "
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
                CaminhoDaPasta = folderDialog.SelectedPath;

            MessageBox.Show("Caminho da pasta: " + CaminhoDaPasta);

            CaminhosDosDocumentos = Util.RetornaTodosOsCaminhosDeArquivosBaseadoNumaPasta(CaminhoDaPasta);

            ////Testando se ta pegando o caminho certo dos arquivos (ESTÁ!)
            //foreach (string path in DocPaths)
            //{
            //    MessageBox.Show(path);
            //}

            if(CaminhosDosDocumentos != null)
                ListaDeDocumentos = Util.RetornaTodosOsTextosDeArquivosDocx(CaminhosDosDocumentos);

            foreach (string path in ListaDeDocumentos)
            {
                MessageBox.Show(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BagOfWords = new List<string>();
            BagOfWords = Util.RetornaBagOfWords(ListaDeDocumentos);

            MessageBox.Show("Quant. de palavras encontradas: " + BagOfWords.Count);

            foreach (string palavra in BagOfWords)
            {
                MessageBox.Show(palavra);
            }

        }
    }
}
