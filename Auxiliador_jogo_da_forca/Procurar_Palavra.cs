using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace Auxiliador_jogo_da_forca
{
    class Procurar_Palavra
    {
        public static void Procurar (string Texto)
        {
            string[] Linhas = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory()+"\\dicionario.txt"); //Nao recomendo para dicionarios grandes. Para arquivo grandes recomendo dividir em pedacos
            string PalavraLowerCase;
            string PedacoLowerCase;
            int PosicaoPedaco = 0; // Index da array do texto separado a ser procurado. existe como funcionar sem isso porem, nao recomendo
            char[] LetrasErradas;
            char[] LetrasContidas;
            Dictionary<int, string[]> PalvrasCompativeis = new Dictionary<int, string[]>();
            Dictionary<char, int> MelhorPossivelLetra = new Dictionary<char, int>(); // Dicionario das letras que mais apareceram nos "buracos"
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            LetrasErradas = frm.textBox2.Text.ToCharArray();
            LetrasContidas = SepararLetrasContidas(Texto);
            List<string> Melhor_Palavra = new List<string>(); // Dicionario Das palavras que se encaixam
            Dictionary<char, int> AgoraMelhorPossivelLetra = new Dictionary<char, int>(); // Dicionario das letras que apareceram nos "buracos" (letra,vezes)
            foreach (string Pedaco in Texto.Split(' '))
            {
                Melhor_Palavra.Clear();
                foreach (string Palavra in Linhas)
                {
                    if (Pedaco.Length != Palavra.Length) continue;
                    
                    PalavraLowerCase = Palavra.ToLower();
                    PedacoLowerCase = Pedaco.ToLower();
                    bool Adicionado = false;
                    bool LetraAdicionada = false;
                    for (int x =0; x != PedacoLowerCase.Length;x++) // Esse codigo pode e deve ser melhor optimizado para maiores dicionarios, eu so nao sei como
                    { 

                        if (LetrasErradas.Contains(PalavraLowerCase[x]))
                        {
                            if (Adicionado == true) Melhor_Palavra.Remove(PalavraLowerCase);
                            if (LetraAdicionada == true) AgoraMelhorPossivelLetra.Clear();

                            break;
                        }
                        if (PedacoLowerCase[x].Equals('?') && !LetrasContidas.Contains(PalavraLowerCase[x]) )
                        {
                            LetraAdicionada = true;
                            if (AgoraMelhorPossivelLetra.ContainsKey(PalavraLowerCase[x]))
                            {
                                AgoraMelhorPossivelLetra[PalavraLowerCase[x]] = AgoraMelhorPossivelLetra[PalavraLowerCase[x]] + 1;
                            }
                            else
                            {
                                AgoraMelhorPossivelLetra.Add(PalavraLowerCase[x], 1);
                            }
                            continue;
                        }

                        if (PedacoLowerCase[x].Equals(PalavraLowerCase[x]))
                        {
                            if (Adicionado == false)
                            {
                                Melhor_Palavra.Add(PalavraLowerCase);
                                Adicionado = true;
                            }
                            continue;
                        }
                        else
                        {
                            if (Adicionado == true) Melhor_Palavra.Remove(PalavraLowerCase);
                            if (LetraAdicionada == true) AgoraMelhorPossivelLetra.Clear();
                            break;
                        }
                    }
                    MelhorPossivelLetra = MesclarDicionarios(MelhorPossivelLetra, AgoraMelhorPossivelLetra);
                    
                }
                PalvrasCompativeis.Add(PosicaoPedaco, Melhor_Palavra.ToArray());
                PosicaoPedaco++;
            }
            frm.PossiveisPalavras = PalvrasCompativeis;
            frm.comboBox1.Items.Clear();
            foreach (int k in Enumerable.Range(1, PosicaoPedaco))
            {
                frm.comboBox1.Items.Add(k);
            }
            frm.listBox1.Items.Clear();
            frm.listBox1.Items.AddRange(PalvrasCompativeis[0]);
            frm.comboBox1.Text = "1";
            if (MaiorValorChar(MelhorPossivelLetra) != "\0")
                frm.label2.Text = "Melhor escolha de letra possivel : " + MaiorValorChar(MelhorPossivelLetra);
            else
                frm.label2.Text = "Palavra nao existe no dicionario";
        }
        private static string MaiorValorChar(Dictionary<char, int> dicionario)
        {
            KeyValuePair<char, int> Maior = new KeyValuePair<char, int>();
            foreach (KeyValuePair<char, int> Valor in dicionario)
            {
                if (Valor.Value > Maior.Value) Maior = Valor;
            }
            return Maior.Key.ToString();
        }

        private static char[] SepararLetrasContidas(string texto)
        {
            char[] chartexto = texto.ToCharArray();
            string temp = "";
            foreach(char p in chartexto)
            {
                if (!temp.Contains(p.ToString()) && !p.Equals('?'))
                {
                    temp += p.ToString();
                }
                
            }
            return temp.ToCharArray();
        }

        private static Dictionary<char,int> MesclarDicionarios(Dictionary<char,int> dicionario0, Dictionary<char, int> dicionario1)
        {
            Dictionary<char, int> Resultado = new Dictionary<char, int>();
            foreach (KeyValuePair<char,int> atual in dicionario0)
            {
                if(Resultado.ContainsKey(atual.Key))
                {
                    Resultado[atual.Key] = Resultado[atual.Key] + atual.Value;
                }
                else
                {
                    Resultado.Add(atual.Key,atual.Value);
                }
            }
            foreach (KeyValuePair<char, int> atual in dicionario1)
            {
                if (Resultado.ContainsKey(atual.Key))
                {
                    Resultado[atual.Key] = Resultado[atual.Key] + atual.Value;
                }
                else
                {
                    Resultado.Add(atual.Key, atual.Value);
                }
            }
            return Resultado;
        }
        
    }
}
