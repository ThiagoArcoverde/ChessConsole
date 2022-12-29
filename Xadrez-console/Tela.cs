using System;
using tabuleiro;
using Xadrez;
using System.Collections.Generic;

namespace Xadrez_console
{
    class Tela
    {

        public static void PrintGame(PartidaDeXadrez p)
        {
            PrintTabuleiro(p.Tab);
            Console.WriteLine();
            PrintOffGamePieces(p);
            Console.WriteLine("= = = = = = = = = =");
            Console.WriteLine("Turno: " + p.Turno);
            Console.WriteLine("Aguardando jogada: " + p.JogadorAtual);
            if (p.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void PrintOffGamePieces(PartidaDeXadrez p)
        {
            Console.WriteLine("Pecas Capturadas");
            Console.Write("Brancas: ");
            ImprimirConjunto(p.OffGamePieces(Cor.Branca));
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(p.OffGamePieces(Cor.Preta));
            Console.ForegroundColor = aux;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]\n");
        }

        public static void PrintTabuleiro(Tabuleiro tab)
        {
            for(int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for(int j = 0; j < tab.Colunas; j++)
                {
                    PrintPeca(tab.Peca(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("   A B C D E F G H");
        }

        public static void PrintTabuleiro(Tabuleiro tab, bool[,] posPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAux = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAux;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    PrintPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("   A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez GetPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void PrintPeca(Peca peca)
        {
            if(peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if(peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
