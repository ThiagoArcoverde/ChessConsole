using System;
using tabuleiro;
using Xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez p = new PartidaDeXadrez();

                while (!p.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.PrintGame(p);
                        Console.WriteLine("= = = = = = = = = =");
                        Console.Write("Origem: ");
                        Posicao origem = Tela.GetPosicaoXadrez().ToPosicao();
                        p.ValidateOrigin(origem);

                        bool[,] posPossivel = p.Tab.peca(origem).MovimentosPossiveis();


                        Console.Clear();
                        Tela.PrintTabuleiro(p.Tab, posPossivel);

                        Console.WriteLine("= = = = = = = = = =");
                        Console.Write("Destino: ");
                        Posicao destino = Tela.GetPosicaoXadrez().ToPosicao();
                        p.ValidateDestiny(origem, destino);

                        p.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.PrintGame(p);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}
