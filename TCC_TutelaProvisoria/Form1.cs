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
using System.Data.SqlClient;

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


        Tutela tutela = new Tutela();
        List<Tutela> G_ListaDeTutelas = new List<Tutela>();

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Open the Word file: ",
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
                string TextoTutela = Util.RetornaOTextoDeUmArquivoDocx(doc, data);
                tutela = new Tutela(Path.GetFileName(CaminhoDoDocumento), CaminhoDoDocumento, TextoTutela);
                G_ListaDeTutelas.Add(tutela);
                MessageBox.Show(TextoTutela);
            }
        }

        private void pegarCaminhoDaPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaDeDocumentos = new List<string>();
            var L_ListaDeTutelas = new List<Tutela>();

            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                Description = "To get a folder path: "
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
                CaminhoDaPasta = folderDialog.SelectedPath;

            //MessageBox.Show("Caminho da pasta: " + CaminhoDaPasta);

            CaminhosDosDocumentos = Util.RetornaTodosOsCaminhosDeArquivosBaseadoNumaPasta(CaminhoDaPasta);

            if (CaminhosDosDocumentos != null)
                L_ListaDeTutelas = Util.RetornaTodosOsTextosDeArquivosDocx(CaminhosDosDocumentos);

            foreach (Tutela tutela in L_ListaDeTutelas)
            {
                G_ListaDeTutelas.Add(tutela);
            }

            //PRINTA TODOS ARQUIVOS ENCONTRADOS DENTRO DA PASTA
            //foreach (Tutela tutela in ListaDeTutelas)
            //{
            //    MessageBox.Show(tutela.Texto);
            //}

            MessageBox.Show("Leu todas as tutelas!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (G_ListaDeTutelas != null)
            {
                richTextBox1.Clear();

                BagOfWords = new List<string>();
                BagOfWords = Util.RetornaBagOfWords(G_ListaDeTutelas);

                MessageBox.Show("Quant. de palavras encontradas: " + BagOfWords.Count);

                richTextBox1.Show();

                foreach (string palavra in BagOfWords)
                {
                    richTextBox1.AppendText(palavra + "\n");
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (G_ListaDeTutelas != null && BagOfWords != null)
            {
                string relatorio = Util.QuantidadePalavrasPorTutela(G_ListaDeTutelas, BagOfWords);

                richTextBox1.Clear();
                richTextBox1.AppendText(relatorio);
            }
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            richTextBox1.AppendText("Quantida de tutelas lidas: " + G_ListaDeTutelas.Count + "\n\n");

            if (G_ListaDeTutelas.Count > 0)
            {
                richTextBox1.AppendText("TUTELAS\n\n");

                foreach (Tutela tutela in G_ListaDeTutelas)
                {
                    richTextBox1.AppendText(tutela.Nome + "\n");
                }
            }

        }
    }
}
