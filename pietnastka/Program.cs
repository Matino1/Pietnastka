﻿// See https://aka.ms/new-console-template for more information
using pietnastka;

int[,] game = new int[4, 4]{ {0, 1, 2, 7},
                             {8, 9, 12, 10},
                             {13, 3, 6, 4 },
                             {15, 14, 11, 5 } };

int[,] game2 = new int[4, 4]{ {0, 2, 3, 4},
                              {1, 6, 7, 8},
                              {5, 10, 11, 12},
                              {9, 13, 14, 15 } };
/*int x = 1;
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        game[i, j] = x*2;
        x++;
    }
}*/

BFS bfs = new();

Gameboard gameboard = new Gameboard(game);
Gameboard gameboard2 = new Gameboard(game2);

bfs.result(gameboard2);
Console.WriteLine("Solution depth: " + bfs.depth);
Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
Console.Write("Solution: ");
Console.WriteLine(bfs.resultTime);
bfs.getSolution().ForEach(move => Console.Write(move));
