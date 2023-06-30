using GameСourseWork.other;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    public class NNalgorithm : PotentialAlgorithm
    {
        private float[] input = new float[85];//input to the neural network
        public NeuralNetwork network;
        public int position;

        public override string Name { get => "Neural Network"; set => Name = value; }

        public NNalgorithm() : base() {
            
        }

        public override char step(int[,] board, Point player, Point opponent)
        {
            if (lastPosOpp == new Point(-1, -1) && player == new Point(7, 7) && visitsCount[1, 1] == 0)
            {
                visitsCount[1, 1] = 1;
            }
            else
            {
                if (lastPosOpp != new Point(-1, -1))
                    visitsCount[lastPosOpp.Y, lastPosOpp.X] += 1;
                else
                {
                    int der = 0;
                }
            }
            lastPosOpp = opponent;
            int[,] potential = new int[9, 9];

            for (int i = 0; i < board.GetLength(0); i++) {
                for (int j = 0; j< board.GetLength(1); j++)
                {
                    if (board[8 - i, 8 - j] == 0 || board[i, j] == 0)
                    {
                        input[9 * i + j] = 2 * Math.Max(0, visitsCount[8 - i, 8 - j] - visitsCount[i, j]);
                    }
                    else
                    {
                        input[9 * i + j] = 5 - visitsCount[i, j];
                    }
                }
            }
            input[81] = player.X;
            input[82] = player.Y;
            input[83] = opponent.X;
            input[84] = opponent.Y;
            float[] output = network.FeedForward(input);
            int maxIndex = -1;
            float max = -1;
            for (int i=0; i< output.Length; i++)
            {
                if (max < output[i])
                {
                    max = output[i];
                    maxIndex = i;
                }
            }
            char[] move = { 'U', 'R', 'L', 'D' };
            visitsCount[player.Y, player.X]++;
            switch (maxIndex)
            {
                case 0:
                    if (player.Y == 0)
                        position -= 10;
                    break;
                case 1:
                    if (player.X == 8)
                        position -= 10;
                    break;
                case 2:
                    if (player.X == 0)
                        position -= 10;
                    break;
                case 3:
                    if (player.Y == 8)
                        position -= 10;
                    break;
                default:
                    break;
            }

            return move[maxIndex];
        }
        public void UpdateFitness()
        {
            network.fitness = position;//updates fitness of network for sorting
        }
    }
}
