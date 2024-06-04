using Neat.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    internal class NeatAlgorithm : IArtificialIntelligence
    {
        public string Name { get => "NeatAlgorithm"; set => throw new NotImplementedException(); }

        public override string ToString() { return Name; }

        public NeatBox neatBox;

        public int fit;

        public NeatAlgorithm(NeatBox neatBox) { 
            this.neatBox = neatBox;
        }

        public void ReportGameEnd(bool win)
        {
            
        }

        public void Reset()
        {
            
        }

        public char step(int[,] board, Point player, Point opponent)
        {
            List<double> inputs = new List<double>();

            for (int i=0; i<board.GetLength(0); i++)
            {
                for (int j=0; j<board.GetLength(1); j++)
                {
                    inputs.Add(board[i,j]);
                }
            }
            inputs.Add(player.X);
            inputs.Add(player.Y);
            inputs.Add(opponent.X);
            inputs.Add(opponent.Y);
            List<double> outputs = neatBox.calculateOutput(inputs);

            int indexMax = 0;
            for (int i = 1;i<outputs.Count;i++)
            {
                if (outputs[indexMax] < outputs[i])
                {
                    indexMax = i;
                }
            }
            char[] move = { 'U', 'R', 'L', 'D' };

            Point newPoint = new Point();
            newPoint.X = player.X;
            newPoint.Y = player.Y;
            switch (move[indexMax])
            {
                case 'U':
                    newPoint.Y--;
                    break;
                case 'R':
                    newPoint.X++;
                    break;
                case 'L':
                    newPoint.X--;
                    break;
                case 'D':
                    newPoint.Y++;
                    break;
            }
            if (Math.Abs(newPoint.X - 4)<=4 && Math.Abs(newPoint.Y - 4) <= 4 || newPoint == opponent)
            {
                int i = newPoint.Y, j = newPoint.X;
                if (board[i, j] > 0)
                {
                    fit += PlusFit(board, player, opponent, 3);
                }
                else
                {
                    fit -= 28 - PlusFit(board, player, opponent, 7);
                }
            }
            else
            {
                fit -= 50;
            }
            return move[indexMax];
        }
        private int PlusFit(int[,] board, Point player, Point opponent, int count)
        {
            int plus = 0;
            for (int i = player.Y - 1; i <= player.Y + 1; i++)
            {
                for (int j = player.X - 1; j<= player.X + 1; j++)
                {
                    if ((player.Y == i || player.X == j) && (Math.Abs(i-4) == 5 || Math.Abs(j-4) == 5 || board[i,j]==0 || new Point(j,i)==opponent))
                    {
                        plus += count;
                    }
                }
            }
            return plus;
        }

        public object Clone()
        {
            return new NeatAlgorithm(neatBox);
        }
    }
}
