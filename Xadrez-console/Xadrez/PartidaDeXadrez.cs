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
        public bool Xeque { get; private set; }
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

        public Peca ExecuteMove(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemovePeca(origem);
            p.AddMove();

            Peca pecaCapturada = Tab.RemovePeca(destino);
            Tab.SetPeca(p, destino);
            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void UndoMove(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RemovePeca(destino);
            p.RemoveMove();
            if (pecaCapturada != null)
            {
                Tab.SetPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tab.SetPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecuteMove(origem, destino);

            if (EmXeque(JogadorAtual))
            {
                UndoMove(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (EmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }


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

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach(Peca x in InGamePieces(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if(r == null)
            {
                throw new TabuleiroException("Não há rei da cor" + cor + "no tabuleiro!");
            }
            foreach(Peca x in InGamePieces(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if(mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
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
