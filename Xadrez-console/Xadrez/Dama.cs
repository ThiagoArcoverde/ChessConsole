using tabuleiro;

namespace Xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tab, Cor cor): base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }

        private bool CheckMove(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Posicao.Coluna, Posicao.Linha];
            Posicao pos = new Posicao(0, 0);

            //Acima
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
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

            //NO
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.SetValue(pos.Linha - 1, pos.Coluna - 1);
            }

            //NE
            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.SetValue(pos.Linha - 1, pos.Coluna + 1);
            }

            //SE
            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.SetValue(pos.Linha + 1, pos.Coluna + 1);
            }

            //SO
            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.SetValue(pos.Linha + 1, pos.Coluna - 1);
            }

            return mat;
        }
    }
}
