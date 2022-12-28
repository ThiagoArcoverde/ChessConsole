using System;
using System.Collections.Generic;
using tabuleiro;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();

            PrepararPecas();
        }

        public void ExecuteMove(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemovePeca(origem);
            p.AddMove();

            Peca pecaCapturada = Tab.RemovePeca(destino);
            Tab.SetPeca(p, destino);
            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecuteMove(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidateOrigin(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.peca(pos).CheckPossibleMoves())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida!");
            }
        }

        public void ValidateDestiny(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).CanMoveTo(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> OffGamePieces(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in PecasCapturadas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> InGamePieces(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(OffGamePieces(cor));
            return aux;
        }

        public void AddNewPiece(char coluna, int linha, Peca peca)
        {
            Tab.SetPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void PrepararPecas()
        {
            AddNewPiece('c', 1, new Torre(Tab, Cor.Branca));
            AddNewPiece('c', 2, new Torre(Tab, Cor.Branca));
            AddNewPiece('d', 2, new Torre(Tab, Cor.Branca));
            AddNewPiece('e', 2, new Torre(Tab, Cor.Branca));
            AddNewPiece('e', 1, new Torre(Tab, Cor.Branca));
            AddNewPiece('d', 1, new Rei(Tab, Cor.Branca));

            AddNewPiece('c', 7, new Torre(Tab, Cor.Preta));
            AddNewPiece('c', 8, new Torre(Tab, Cor.Preta));
            AddNewPiece('d', 7, new Torre(Tab, Cor.Preta));
            AddNewPiece('e', 7, new Torre(Tab, Cor.Preta));
            AddNewPiece('e', 8, new Torre(Tab, Cor.Preta));
            AddNewPiece('d', 8, new Rei(Tab, Cor.Preta));
        }
    }
}
