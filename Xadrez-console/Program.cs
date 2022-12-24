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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.SetPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.SetPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.SetPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                Tela.PrintTabuleiro(tab);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
