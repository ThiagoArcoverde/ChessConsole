using System;
using tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor): base(tab, cor)
        {
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

            return mat; 
        }
    }
}
