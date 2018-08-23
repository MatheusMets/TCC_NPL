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
using static System.Windows.Forms.CheckedListBox;

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
            CheckedListTutelasLidas.Visible = false;

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
            CheckedListTutelasLidas.Visible = false;

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
            {
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

                MessageBox.Show("Leu todas as tutelas!\n Quantidade de tutelas lidas: " + L_ListaDeTutelas.Count);
            }
            else
            {
                MessageBox.Show("Nada havia nesta pasta. Escolha uma pasta que contenha arquivos word :/");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckedListTutelasLidas.Visible = false;

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
            CheckedListTutelasLidas.Visible = false;

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
            richTextBox1.Clear();
            CheckedListTutelasLidas.Visible = true;

            var Itens = CheckedListTutelasLidas.Items;
            Itens.Clear();

            foreach (Tutela tutela in G_ListaDeTutelas)
            {
                Itens.Add(tutela.Nome);
            }

            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckedListTutelasLidas.Visible = false;
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CheckedListTutelasLidas.Visible = false;
            List<string> NomesDasTutelasQueSeraoAnalisadas = new List<string>();
            List<Tutela> TutelasQueSeraoAnalisadas = new List<Tutela>();

            CheckedItemCollection ItemsMarcados;
            ItemsMarcados = CheckedListTutelasLidas.CheckedItems;

            if (ItemsMarcados.Count > 2)
                MessageBox.Show("Deve selecionar apenas 2 tutelas");

            else
            {
                foreach (var item in ItemsMarcados)
                {
                    NomesDasTutelasQueSeraoAnalisadas.Add(item.ToString());
                }

                foreach (Tutela tutela in G_ListaDeTutelas)
                {

                    foreach (string NomeTutela in NomesDasTutelasQueSeraoAnalisadas)
                    {

                        if (tutela.Nome.Equals(NomeTutela))
                        {
                            TutelasQueSeraoAnalisadas.Add(tutela);
                            break;                                      //Vai sempre pegar o primeiro nome que achar. Esse é o problema de ter tutelas com nomes repetidos. Tinha que tratar com Identificador
                        }
                    }
                }

                double Similaridade = Util.RealizaSimilaridade(TutelasQueSeraoAnalisadas.ElementAt(0), TutelasQueSeraoAnalisadas.ElementAt(1));

                MessageBox.Show("A porcentagem de semelhança entre as tutelas analisadas é de aproximadamente " + Similaridade * 100 + "%");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int QuantTutelas = G_ListaDeTutelas.Count;

            G_ListaDeTutelas.Clear();

            MessageBox.Show("Tutelas excluidas com sucesso! \n" + QuantTutelas + " tutelas foram excluidas");
        }

        private void CheckedListTutelasLidas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && CheckedListTutelasLidas.CheckedItems.Count >= 2)
                e.NewValue = CheckState.Unchecked;
        }
    }
}
