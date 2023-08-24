using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Drawing;
using System.Security.Principal;

namespace MyVirtualPet
{
    internal class Program
    {
        //inicio da imagem
        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @"C:\Users\Danilo Filitto\Downloads\Panda.jpg";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);


        }
        //fim da imagem
        static void LerArquivoTexto(string nome, string nomeDono, ref float alimentado, ref float limpo, ref float feliz)
        {
            String dir = Environment.CurrentDirectory + "\\";
            String file = dir + nome + nomeDono + ".txt";
            if (File.Exists(file))
            {
                string[] dados = File.ReadAllLines(file);
                alimentado = float.Parse(dados[2]);
                limpo = float.Parse(dados[3]);
                feliz = float.Parse(dados[4]);

                if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
                {
                    Console.WriteLine("Assistente Virtual.:");
                    Console.WriteLine("O seu bichinho está muito fraco");
                    Console.Write("Vamos cuidar dele pra você...");
                    Console.WriteLine("Pronto, ele está saudável e feliz");
                    alimentado = 100;
                    limpo = 100;
                    feliz = 100;
                }
            }
        }

        static void GravarArquivoTexto(string nome, string nomeDono, float alimentado, float limpo, float feliz)
        {
            String fileContent = nome + Environment.NewLine;
            fileContent += nomeDono + Environment.NewLine; 
            fileContent += alimentado + Environment.NewLine;
            fileContent += limpo + Environment.NewLine;
            fileContent += feliz + Environment.NewLine;

            //gravei no arquivo texto
            String dir = Environment.CurrentDirectory + "\\";
            String file = dir + nome + nomeDono + ".txt";
            File.WriteAllText(file, fileContent);
        }

        static string Falar()
        {
            //fales do pet

            string[] frases = new string[10];

            frases[0] = "Nossa, o dia foi muito legal, comi o sofá!";
            frases[1] = "Não contava com minha astúcia!";
            frases[3] = "Foi sem querer querendo!";
            frases[4] = "Que saudades, passei o dia todo esperando você chegar";
            frases[5] = "Hoje assistir um filme muito bom!";
            frases[6] = "Você não vai com minha cara!";
            frases[7] = "Oh! E agora, quem poderá mim ajudar!";
            frases[8] = "Não gostaria de tomar uma xicara de café?";
            frases[9] = "Isso mim dá coisa!";

            Random rand = new Random();
            string frase = frases[rand.Next(frases.Length)];

            return frase;
        }

        static void LerDados(ref string nome, ref string nomeDono)
        {
            Console.Write("Qual é o seu nome? ");
            nomeDono = Console.ReadLine();
            Console.Write("Qual é o nome do seu Pet Virtual? ");
            nome = Console.ReadLine();
            Console.WriteLine("Olá {0}, eu sou o seu bichinho virtual.", nomeDono );
        }

        static string Interagir(string nomeDono, ref float alimentado, ref float limpo, ref float feliz) 
        {
            Random rand = new Random();
            Console.WriteLine("{0}, o que vamos fazer hoje? ", nomeDono);
            Console.Write("Brincar/Comer/Banho/nada.: ");
            string entrada = Console.ReadLine().ToLower();

            switch (entrada)
            {
                case "brincar": feliz += rand.Next(30); break;
                case "Comer": alimentado += rand.Next(30); break;
                case "banho": limpo += rand.Next(30); break;
            }
            if (feliz > 100) feliz = 100;
            if (limpo > 100) limpo = 100;
            if (alimentado > 100) alimentado = 100;

            return entrada;
        }

        static void AtualizarStatus(ref float alimentado, ref float limpo, ref float feliz)
        {
            //alterar o status do pet
            // 0 - perde alimento / 1 - perde limpeza / 2 - perde limpeza

            Random rand = new Random();
            int caracteristica = rand.Next(3);
            switch (caracteristica)
            {
                case 0: alimentado -= rand.Next(10, 40); break;
                case 1: limpo -= rand.Next(10, 40); break;
                case 2: feliz -= rand.Next(10, 40); break;

            }
        }

        static void ExibirStatus(float alimentado, float limpo, float feliz, int tipo)
        {
            if(tipo == 0)
            {
                Console.WriteLine("Status do Pet");
                Console.WriteLine("Alimentado.: {0}", alimentado);
                Console.WriteLine("Limpo.: {0}", limpo);
                Console.WriteLine("Feliz.: {0}", feliz);
            }
            if(tipo == 1)
            {
                if (alimentado > 40 && alimentado < 60)
                {
                    Console.WriteLine("Eu estou faminto!!!!!");
                    Console.WriteLine("Nada melhor do que um comidinha...");
                }
                if (limpo > 40 && limpo < 60)
                {
                    Console.WriteLine("Nossa, estou meio sujinho!!!!!");
                    Console.WriteLine("Nada melhor do que um banho...");
                }
                if (feliz > 40 && feliz < 60)
                {
                    Console.WriteLine("Fiquei em casa o dia todo!!!!!");
                    Console.WriteLine("Nada melhor do que brincar...");
                }
            }
            if(tipo == 2)
            {
                Console.WriteLine("Status do Pet");
                Console.WriteLine("Alimentado.: {0}", alimentado);
                Console.WriteLine("Limpo.: {0}", limpo);
                Console.WriteLine("Feliz.: {0}", feliz);

                Console.WriteLine("Nível de Felicidade");

                if (alimentado > 40 && alimentado < 60)
                {
                    Console.WriteLine("Eu estou faminto!!!!!");
                    Console.WriteLine("Nada melhor do que um comidinha...");
                }
                if (limpo > 40 && limpo < 60)
                {
                    Console.WriteLine("Nossa, estou meio sujinho!!!!!");
                    Console.WriteLine("Nada melhor do que um banho...");
                }
                if (feliz > 40 && feliz < 60)
                {
                    Console.WriteLine("Fiquei em casa o dia todo!!!!!");
                    Console.WriteLine("Nada melhor do que brincar...");
                }
            }
        }
        static void Main(string[] args)
        {
            //dados do jogo
            string entrada = "";
            String foto = Environment.CurrentDirectory + "\\Panda.jpg";
            string nomeDono = "";

            //dados do pet
            string nome = "";

            //Status do pet
            float alimentado = 100;
            float limpo = 100;
            float feliz = 100;
            
            ExibirImagem(foto, 35, 20);

            Console.WriteLine("My Pet Virtual");

            LerDados(ref nome, ref nomeDono);            

            //coletar os dados do pet no arquivo texto
            LerArquivoTexto(nome, nomeDono, ref alimentado, ref limpo, ref  feliz);

            //o programam principal
            //brinca com o PET
            entrada = "sim"; 
            while(entrada.ToLower() != "nada" && alimentado > 0 && limpo > 0 && feliz > 0)
            {
                Console.Clear();
                Console.WriteLine("Olá! {0}", nomeDono);
                Console.WriteLine(Falar());
                Thread.Sleep(3000);

                AtualizarStatus(ref alimentado, ref limpo, ref feliz);
                Console.Clear();

                ExibirStatus(alimentado, limpo, feliz, 1);

                Thread.Sleep(3000);
                Console.Clear();

                entrada = Interagir(nomeDono, ref alimentado, ref limpo, ref feliz);

            }
            //saiu do jogo
            if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
            {
                Console.WriteLine("Que descuido {0}... seu PET morreu!!!!!!", nomeDono);
                Console.WriteLine("Você não cuidou de mim, te vejo na próxima vida!...");
            }
            else
            {
                Console.WriteLine("{0}, não vejo a hora de brinca de novo com você!!!!", nomeDono);
                Console.WriteLine("Volte Logo!!!!!!!!!!!!!!");
            }
            
            Console.WriteLine("Até outro dia!!!");
            //armazenar os dados
            //criei os dados

            GravarArquivoTexto(nome, nomeDono, alimentado, limpo, feliz);

            Console.ReadKey();
        }
    }
}
