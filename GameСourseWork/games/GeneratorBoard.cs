using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameСourseWork.games.GeneratorBoard;

namespace GameСourseWork.games
{
    public static class GeneratorBoard
    {
        public enum Board
        {
            None,
            MultiLayered,
            Sparse,
            SingleLayered,
            Centered,
            Edge,
            PlayerSquare,
            NonPlayerSquare,
            Canyon
        }


        public static int[,] Generator(Board type)
        {
            switch (type)
            {
                case Board.None:
                    return NoneBoard();
                case Board.MultiLayered:
                    return MultiLayeredBoard();
                case Board.Sparse:
                    return SparseBoard();
                case Board.SingleLayered:
                    return SingleBoard();
                case Board.Centered:
                    return CenteredBoard();
                case Board.Edge:
                    return EdgeBoard();
                case Board.PlayerSquare:
                    return MultiPlayerBoard();
                case Board.NonPlayerSquare:
                    return SparsePlayerBoard();
                case Board.Canyon:
                    return BoardWithCanyon();
                default:
                    return NoneBoard();
            }
        }

        private static int[,] AllRandomBoard(int minValue, int maxValue)
        {
            int[,] board = new int[9, 9];
            Random random = new Random();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = random.Next(minValue, maxValue);
                }
            }
            return board;
        }

        private static int distanceToCentre(int y, int x)
        {
            return Math.Abs(x - 4) + Math.Abs(y - 4);
        }

        private static int[,] ProbabilitiesFromCentre(int[,] probabilities)
        {
            int[,] board = new int[9, 9];
            Random random = new Random();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int d = distanceToCentre(i, j);
                    int sum = probabilities[d, 0];
                    int t = random.Next(101);
                    int k = 1;
                    while (sum < t)
                    {
                        sum += probabilities[d, k];
                        k++;
                    }
                    board[i, j] = k;
                }
            }
            return board;
        }

        public static int[,] NoneBoard()
        {
            return AllRandomBoard(1, 6);
        }
        public static int[,] MultiLayeredBoard()
        {
            return AllRandomBoard(4, 6);
        }
        public static int[,] SparseBoard()
        {
            return AllRandomBoard(1, 3);
        }
        public static int[,] SingleBoard()
        {
            return AllRandomBoard(1, 2);
        }

        public static int[,] CenteredBoard()
        {
            int[,] p = { { 0, 0, 0, 0, 100 },// расстояние 0 от центра
                         { 0, 0, 0, 25, 75 }, // 1
                         { 0, 0, 10, 40, 50 }, // 2
                         { 0, 10, 25, 40, 25 }, //3
                         { 10, 20, 35, 25, 15 }, //4
                         { 15, 25, 35, 20, 10 }, //5
                         { 25, 40, 25, 10, 0 }, //6
                         { 50, 40, 10, 0, 0 }, //7
                         { 75, 25, 0, 0, 0 }, //8
                         { 100, 0, 0, 0, 0 }, //9
                        };
            return ProbabilitiesFromCentre(p);
        }
        public static int[,] EdgeBoard()
        {
            int[,] p = {
                        {100, 0, 0, 0, 0}, //0
                        {75, 25, 0, 0, 0}, //1
                        {50, 40, 10, 0, 0}, //2
                        {25, 40, 25, 10, 0}, //3
                        {15, 25, 35, 20, 10}, //4
                        {10, 20, 35, 25, 15}, //5
                        {0, 10, 25, 40, 25}, //6
                        {0, 0, 10, 40, 50}, //7
                        {0, 0, 0, 25, 75}, //8
                        {0, 0, 0, 0, 100} //9
                        };
            return ProbabilitiesFromCentre(p);
        }

        public static int[,] MultiPlayerBoard()
        {
            int[,] board = new int[9, 9];
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 4 && j < 4) || (j > 4 && i > 4)) {
                        board[i, j] = random.Next(3, 6);
                    }
                    else
                    {
                        board[i, j] = random.Next(1, 3);
                    }
                }
            }
            return board;
        }
        public static int[,] SparsePlayerBoard()
        {
            int[,] board = new int[9, 9];
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i < 4 && j < 4) || (j > 4 && i > 4))
                    {
                        board[i, j] = random.Next(1, 3);
                    }
                    else
                    {
                        board[i, j] = random.Next(3, 6);
                    }
                }
            }
            return board;
        }

        public static int[,] BoardWithCanyon()
        {
            int[,] board = new int[9, 9];
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i==4 || j==4)
                    {
                        board[i, j] = 1;
                    }
                    else
                    {
                        board[i, j] = random.Next(3, 6);
                    }
                }
            }
            return board;
        }
    }
}
