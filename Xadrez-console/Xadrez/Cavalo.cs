using tabuleiro;

namespace Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "C";
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

            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna - 2);
            if(Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.SetValue(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos) && CheckMove(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }


            return mat;
        }
    }
}
