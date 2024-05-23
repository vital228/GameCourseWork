using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    public class PotentialAlgorithm : AbstractToCellAlgorithm
    {
        public override string Name { get => "Potential"; set { } }
        protected int[,] visitsCount;
        protected Point lastPosOpp;
        

        public PotentialAlgorithm() {
            visitsCount = new int[9,9];
            for (int i=0; i<9; i++)
            {
                for (int j=0; j<9; j++)
                {
                    visitsCount[i,j] = 0;
                }
            }
            lastPosOpp = new Point(-1,-1);
        }

        private PotentialAlgorithm(string name, int[,] visitsCount, Point lastPosOpp)
        {
            Name = name;
            this.visitsCount = visitsCount;
            this.lastPosOpp = lastPosOpp;
        }

        public override void Reset()
        {
            visitsCount = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    visitsCount[i, j] = 0;
                }
            }
            lastPosOpp = new Point(-1, -1);
        }

        public void update(Point player, Point opponent)
        {
            if (lastPosOpp == new Point(-1, -1) && player == new Point(7, 7))
            {
                visitsCount[1, 1] = 1;
            }
            else
            {
                if (lastPosOpp != new Point(-1, -1))
                    visitsCount[lastPosOpp.Y, lastPosOpp.X] += 1;
            }
            lastPosOpp = opponent;
            visitsCount[player.Y, player.X]++;
        }

        public override char step(int[,] board, Point player, Point opponent)
        {
            char[] move = { 'U', 'R', 'L', 'D' };
            if (lastPosOpp== new Point(-1,-1) && player == new Point(7,7))
            {
                visitsCount[1, 1] = 1;
            }
            else
            {
                if (lastPosOpp != new Point(-1, -1))
                    visitsCount[lastPosOpp.Y, lastPosOpp.X] += 1;
            }
            lastPosOpp = opponent;
            setAvailableCells();
            availableCells[player.Y, player.X].distance = 0;
            bfs(board, player, opponent);
            int[,] potential = new int[9, 9];
            int mx = 0;
            List <Cell> mxPoint = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    potential[i, j] = 0;
                    if (availableCells[i, j].direction >= 0)
                    {
                        for (int i1 = -2; i1<=2; i1++)
                        {
                            for (int j1 = -2; j1 <= 2; j1++)
                            {
                                int y = i + i1, x = j + j1;
                                if (Math.Abs(i1) + Math.Abs(j1)<=2 && y>=0 && x>=0 && y<9 && x<9 && availableCells[y, x].direction >= 0)
                                {
                                    if (board[8 - y, 8 - x] == 0 || board[y, x] == 0)
                                    {
                                        potential[i, j] += 2 * Math.Max(0, visitsCount[8 - y, 8 - x] - visitsCount[y, x]);
                                    }
                                    else
                                    {
                                        potential[i, j] += 5 - visitsCount[y, x];
                                    }
                                }
                            }
                        }
                        if (potential[i,j] == mx)
                        {
                            mxPoint.Add(availableCells[i,j]);
                        }
                        if (potential[i, j] > mx)
                        {
                            mxPoint.Clear();
                            mx = potential[i, j];
                            mxPoint.Add(availableCells[i, j]);
                        }
                    }
                        
                }
            }
            visitsCount[player.Y, player.X]++;
            if (mxPoint.Count > 0)
            {
                Random random = new Random();
                //int k = random.Next(0, mxPoint.Count);
                mxPoint.Sort(FunctionSort);
                int k = 1;
                while (k < mxPoint.Count && FunctionSort(mxPoint[k - 1], mxPoint[k]) == 0)
                {
                    k++;
                }
                return move[mxPoint[random.Next(k)].direction];
            }
            else
            {
                return move[0];
            }

        }

        protected override int FunctionSort(Cell c1, Cell c2)
        {
            return c2.distance - c1.distance;
        }

        public override object Clone()
        {
            return new PotentialAlgorithm(Name, (int[,])visitsCount.Clone(), lastPosOpp);
        }
    }
}
