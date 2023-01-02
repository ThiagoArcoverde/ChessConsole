using System;
using tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {

        private PartidaDeXadrez Partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida): base(tab, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool CheckMove(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }
        
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && QteMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);
            //verificando acima da peça
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && CheckMove(pos)){
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando diagonal direita superior da peça
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando direita da peça
            pos.SetValue(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando diagonal direita inferior da peça
            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando abaixo da peça
            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando diagonal esquerda inferior da peça
            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando esquerda da peça
            pos.SetValue(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //verificando diagonal esquerda superior da peça
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }


            // #jogadaespecial roque
            if(QteMovimentos == 0 && !Partida.Xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao PosT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(PosT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if(Tab.peca(p1) == null && Tab.peca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Posicao PosT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(PosT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return mat; 
        }
    }
}
