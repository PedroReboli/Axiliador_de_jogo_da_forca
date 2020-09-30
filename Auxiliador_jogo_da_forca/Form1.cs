using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auxiliador_jogo_da_forca
{
    public partial class Form1 : Form
    {
        public Dictionary<int, string[]> PossiveisPalavras = new Dictionary<int, string[]>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Dictionary<int, string> AgoraMelhorPossivelLetra = new Dictionary<int, string>();
            var relogio = System.Diagnostics.Stopwatch.StartNew();
            for (int x = 0;x < 10000000; x++)
            {
                AgoraMelhorPossivelLetra.Add(x, "a");
            }
            relogio.Stop();
            MessageBox.Show($"{relogio.ElapsedMilliseconds}");
            List<string> teste = new List<string>();
            relogio = System.Diagnostics.Stopwatch.StartNew();
            for (int x = 0; x < 10000000; x++)
            {
                teste.Add("a");
            }
            relogio.Stop();
            MessageBox.Show($"{relogio.ElapsedMilliseconds}");
            //AgoraMelhorPossivelLetra.Add('a', 2);*/
            Procurar_Palavra.Procurar(textBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                listBox1.Items.AddRange(PossiveisPalavras[Convert.ToInt32(comboBox1.Text)-1]);
            }
            catch { }
        }
    }
}
