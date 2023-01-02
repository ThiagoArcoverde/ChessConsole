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

            // #jogadaespecial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RemovePeca(OrigemT);
                t.AddMove();
                Tab.SetPeca(t, DestinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RemovePeca(OrigemT);
                t.AddMove();
                Tab.SetPeca(t, DestinoT);
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

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RemovePeca(DestinoT);
                t.RemoveMove();
                Tab.SetPeca(t, OrigemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RemovePeca(DestinoT);
                t.RemoveMove();
                Tab.SetPeca(t, OrigemT);
            }
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
            if (EmXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
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
            foreach (Peca x in PecasCapturadas)
            {
                if (x.Cor == cor)
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
            if (cor == Cor.Branca)
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
            foreach (Peca x in InGamePieces(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null)
            {
                throw new TabuleiroException("Não há rei da cor" + cor + "no tabuleiro!");
            }
            foreach (Peca x in InGamePieces(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool EmXequeMate(Cor cor)
        {
            if (!EmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in InGamePieces(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecuteMove(x.Posicao, destino);
                            bool testeXeque = EmXeque(cor);
                            UndoMove(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void AddNewPiece(char coluna, int linha, Peca peca)
        {
            Tab.SetPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void PrepararPecas()
        {
            //Peças brancas
            AddNewPiece('a', 1, new Torre(Tab, Cor.Branca));
            AddNewPiece('b', 1, new Cavalo(Tab, Cor.Branca));
            AddNewPiece('c', 1, new Bispo(Tab, Cor.Branca));
            AddNewPiece('d', 1, new Dama(Tab, Cor.Branca));
            AddNewPiece('e', 1, new Rei(Tab, Cor.Branca, this));
            AddNewPiece('f', 1, new Bispo(Tab, Cor.Branca));
            AddNewPiece('g', 1, new Cavalo(Tab, Cor.Branca));
            AddNewPiece('h', 1, new Torre(Tab, Cor.Branca));
            AddNewPiece('a', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('b', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('c', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('d', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('e', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('f', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('g', 2, new Peao(Tab, Cor.Branca));
            AddNewPiece('h', 2, new Peao(Tab, Cor.Branca));

            //Peças Pretas
            AddNewPiece('a', 8, new Torre(Tab, Cor.Preta));
            AddNewPiece('b', 8, new Cavalo(Tab, Cor.Preta));
            AddNewPiece('c', 8, new Bispo(Tab, Cor.Preta));
            AddNewPiece('d', 8, new Dama(Tab, Cor.Preta));
            AddNewPiece('e', 8, new Rei(Tab, Cor.Preta, this));
            AddNewPiece('f', 8, new Bispo(Tab, Cor.Preta));
            AddNewPiece('g', 8, new Cavalo(Tab, Cor.Preta));
            AddNewPiece('h', 8, new Torre(Tab, Cor.Preta));
            AddNewPiece('a', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('b', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('c', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('d', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('e', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('f', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('g', 7, new Peao(Tab, Cor.Preta));
            AddNewPiece('h', 7, new Peao(Tab, Cor.Preta));

        }
    }
}
