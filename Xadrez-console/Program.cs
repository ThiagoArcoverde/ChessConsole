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
                    Console.Clear();
                    Tela.PrintTabuleiro(p.Tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.GetPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.GetPosicaoXadrez().ToPosicao();
                    p.ExecuteMove(origem, destino);

                }

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
