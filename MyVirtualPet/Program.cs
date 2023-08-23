using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyVirtualPet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * O jogo mantém os dados do pet mesmo ao finalizar o game
             * Inicia o programa via prompt e coleto o nome do pet
             * Coleta os dados de alimento, limpo e feliz em um arquivo
             * De tempos em tempo vamos diminuir o valor de uma caracteristica do pet
             */

            string nome = "";
            string nomeDono = "";
            string entrada = "";
            string resposta = "";

            float alimentado = 100;
            float limpo = 100;
            float feliz = 100;

            //diminuir os valores das características do pet
            int caracteristica = 0;
            Random rand = new Random();

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
            
            Console.WriteLine("My Pet Virtual");
            
            //coleta de dados
            //entrada de dados
            if (args.Length > 0)
            {
                nome = args[0];
            }
            else
            {
                Console.Write("Qual é o nome do seu pet? ");
                nome = Console.ReadLine();
            }

            Console.Write("Oi, qual o nome do meu dono? ");
            nomeDono = Console.ReadLine();
            Console.WriteLine("Legal estava com muita saudade de você, " + nomeDono + "!");
            //coletar os dados do pet no arquivo texto
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
            //o programam principal
            entrada = "sim";
            while(entrada.ToLower() != "nada" && alimentado > 0 && limpo > 0 && feliz > 0)
            {
                Console.Clear();
                Console.WriteLine("Alimentado.: " + alimentado);
                Console.WriteLine("Limpo.: " + limpo);
                Console.WriteLine("Feliz.: " + feliz);
                //alterar o status do pet
                // 0 - perde alimento / 1 - perde limpeza / 2 - perde limpeza
                caracteristica = rand.Next(3);
                switch (caracteristica)
                {
                    case 0:
                        alimentado -= rand.Next(10,40);
                        break;
                    case 1:
                        limpo -= rand.Next(10,40);
                        break;
                    case 2:
                        feliz -= rand.Next(10,40);
                        break;

                }
                
                Console.WriteLine("Olá! {0}", nomeDono);
                Console.WriteLine(frases[rand.Next(frases.Length)]);

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

                Thread.Sleep(3000);
                Console.Clear();

                
                
                Console.WriteLine("{0}, o que vamos fazer hoje? ", nomeDono);
                Console.Write("Brincar/Comer/Banho/nada.: ");
                resposta = Console.ReadLine().ToLower();

                switch (resposta)
                {
                    case "brincar":
                        feliz += rand.Next(30);
                        break;
                    case "Comer":
                        alimentado += rand.Next(30);
                        break;
                    case "banho":
                        limpo += rand.Next(30);
                        break;
                    case "nada":
                        entrada = "nada";
                        break;
                }
                if (feliz > 100) feliz = 100;
                if (limpo > 100) limpo = 100;
                if (alimentado > 100) alimentado = 100;

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
            String fileContent = nome + Environment.NewLine;
            fileContent += nomeDono + Environment.NewLine;
            fileContent += alimentado + Environment.NewLine;
            fileContent += limpo + Environment.NewLine;
            fileContent += feliz + Environment.NewLine;
            //gravei no arquivo texto
            File.WriteAllText(file, fileContent);

            Console.ReadKey();
        }
    }
}
