using System;
using tabuleiro;

namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
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
            //Acima
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna);
            while(Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;

            }

            //Abaixo
            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;

            }

            //Direita
            pos.SetValue(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {   
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            //Esquerda
            pos.SetValue(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;

            }

            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}

