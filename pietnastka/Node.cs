﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class Node
    {
        public int level { get; set; }
        private readonly ulong prime = 7;
        private Gameboard board;
        private List<Node> children = new List<Node>();
        private char[] possibleMoves = new char[4] { 'L', 'R', 'D', 'U' };
        private List<char> previousMoves = new List<char>();
        public char[] getPossibleMoves()
        {
            return possibleMoves;
        }

        public List<char> getPreviousMoves()
        {
            return previousMoves;
        }

        public void addPreviousMove(char move)
        {
            previousMoves.Add(move);
        }
        public Node(int level, Gameboard board)
        {
            this.level = level;
            this.board = board;
            findFullManhatanDistance();
        }
        public Node(int level, Gameboard board, List<char> previousMoves)
        {
            this.level = level;
            this.board = board;
            findFullManhatanDistance();
            foreach (char move in previousMoves)
                this.previousMoves.Add(move);
        }   

        public ulong getBoardHash()
        {
            return board.getBoardHash() + (ulong) level * prime;
        }

        public ulong getNextMoveHash(char move)
        {
            return board.NextMoveHash(move) + (ulong) level * prime;
        }

        public Gameboard getGameboard()
        {
            return board;
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public Gameboard NextMoveBoard(char move)
        {
            return board.NextMoveBoard(move);
        }

        public void addChild(char move)
        {
            Gameboard tempBoard = new Gameboard(this.board.copyBoard(), move);
            Node tempNode = new Node(this.level + 1, tempBoard, this.previousMoves);
            tempNode.addPreviousMove(move);
            this.children.Add(tempNode);
        }

        public void findFullManhatanDistance()
        {
            board.findManhattanDistance();
            board.manhattanDistance += level;
        }

        public char getReversePreviousMove()
        {
            
            if(this.previousMoves.Count > 0)
            {
                switch (this.previousMoves[previousMoves.Count - 1])
                {
                    case 'L':
                        return 'R';
                        break;

                    case 'R':
                        return 'L';
                        break;

                    case 'U':
                        return 'D';
                        break;

                    case 'D':
                        return 'U';
                        break;
                }
            }
            return 'N';
        }

        public void addChildren(List<char> moves)
        {
            //List<int> legalMoves = new List<int>();
            //for (int i = 0; i < moves.Count; i++)
            //{
            //    if (board.isMoveLegal(moves[i]))
            //    {
            //        legalMoves.Add(i);
            //    }
            //}
            foreach (char x in moves)
            {
                //if (moves.Contains(this.possibleMoves[x]))
                //    addChild(moves[x]);
                foreach (char move in possibleMoves)
                    if (x == move)
                        addChild(move);
            }
        }
    }
}
