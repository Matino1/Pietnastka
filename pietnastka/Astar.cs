﻿using System.Diagnostics;
using System.Collections;

namespace pietnastka
{
    internal class Astar : SearchingAlgorithm
    {
        public Astar() : base()
        {
        }

        public override bool result(Gameboard rootBoard, string algorithm)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);
            TreeNode rootTreeNode = new TreeNode();
            rootTreeNode.gameNode = rootNode;

            //List<Node> list = new List<Node>();
            //SortedList<int, Node> list = new SortedList<int, Node>();
            //SortedSet<Node> list = new SortedSet<Node>();
            /*if (algorithm == "manh")
            {
                list = new SortedSet<Node>(Comparer<Node>.Create((a1, a2) => a1.getGameboard().manhattanDistance.CompareTo(a2.getGameboard().manhattanDistance)));
                //list.Add(rootNode.getGameboard().manhattanDistance, rootNode);
            }
            else if (algorithm == "hamm")
            {
                //list.Add(rootNode.getGameboard().hammingDistance, rootNode);
                list = new SortedSet<Node>(Comparer<Node>.Create((a1, a2) => a1.getGameboard().hammingDistance.CompareTo(a2.getGameboard().hammingDistance)));
            }*/
            BinaryTree tree = new BinaryTree();
            tree.Add(rootTreeNode);

            //list.Add(rootNode);

            bool isFinished = false;
            Node node;
            //List<ulong> visitedBoards = new List<ulong>();
            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            while (tree.root != null)
            {
                //node = list.First();
                //list.Remove(list.First());

                node = tree.Remove().gameNode;

                nodesProcessed++;

                //if (!visitedBoards.Contains(node.getBoardHash()))
                //{
                if (node.getPreviousMoves().Count == 0)
                {
                    visitedBoards.Add(node.getBoardHash());
                }
                if (node.getGameboard().IsFinished())
                {
                    depth = node.level;
                    maxLevel = node.level;
                    solutionMoves = node.getPreviousMoves();

                    if (node.getPreviousMoves().Count == 0)
                        Console.WriteLine("Game already finished");
                    isFinished = true;
                }

                List<char> moves = new List<char>();
                foreach (char move in node.getPossibleMoves())
                {
                    if (node.getGameboard().isMoveLegal(move))
                    {
                        if (visitedBoards.Add(node.getGameboard().nextMove(move)))
                        {
                            moves.Add(move);
                            nodesVisited++;
                        }
                    }
                }
                if (node.level < maxLevel)
                    node.addChildren(moves);
                //Console.WriteLine(nodesVisited);
                List<Node> nodes = node.getChildren();
                foreach (Node child in nodes)
                {
                    if (child.level <= maxLevel)
                    {
                        /*if (algorithm == "manh")
                        {
                            list.Add(rootNode.getGameboard().manhattanDistance, rootNode);
                        }
                        else if (algorithm == "hamm")
                        {
                            list.Add(rootNode.getGameboard().hammingDistance, rootNode);
                        }*/
                        //list.Add(child);
                        TreeNode childTreeNode = new TreeNode();
                        childTreeNode.gameNode = child;
                        tree.Add(childTreeNode);
                    }
                        //list.Add(child);
                }
                /*if (algorithm == "hamm")
                {
                    list.Sort((p, q) => p.getGameboard().hammingDistance.CompareTo(q.getGameboard().hammingDistance));
                }
                else if (algorithm == "manh")
                {
                    list.Sort((p, q) => p.getGameboard().manhattanDistance.CompareTo(q.getGameboard().manhattanDistance));
                }*/

                //nodesProcessed = visitedBoards.Count - 1;
                
                
            }
            resultLenght = solutionMoves.Count;
            saveElapsedTime(stopWatch);
            if (isFinished)
            {
                return true;
            }
            else
            {
                resultLenght = -1;
                return false;
            }
        }
    }
}