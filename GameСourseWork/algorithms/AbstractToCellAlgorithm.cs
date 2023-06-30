using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    public abstract class AbstractToCellAlgorithm : IArtificialIntelligence
    {
        protected struct Cell
        {
            public int distance;
            public int direction;
            public int X, Y;
        }

        public abstract string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public virtual void Reset()
        {
        }

        public AbstractToCellAlgorithm()
        {

        }

        protected Cell[,] availableCells;

        protected void bfs(int[,] board, Point s, Point opponent)
        {
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(s);
            while (q.Count > 0)
            {
                Point v = q.Dequeue();
                if (v.Y > 0 && availableCells[v.Y - 1, v.X].distance < 0 && board[v.Y - 1, v.X] > 0 && new Point(v.X, v.Y - 1) != opponent)
                {
                    availableCells[v.Y - 1, v.X].distance = availableCells[v.Y, v.X].distance + 1;
                    if (availableCells[v.Y, v.X].direction < 0) availableCells[v.Y - 1, v.X].direction = 0;
                    else availableCells[v.Y - 1, v.X].direction = availableCells[v.Y, v.X].direction;
                    q.Enqueue(new Point(v.X, v.Y - 1));
                }
                if (v.Y < 8 && availableCells[v.Y + 1, v.X].distance < 0 && board[v.Y + 1, v.X] > 0 && new Point(v.X, v.Y + 1) != opponent)
                {
                    availableCells[v.Y + 1, v.X].distance = availableCells[v.Y, v.X].distance + 1;
                    if (availableCells[v.Y, v.X].direction < 0) availableCells[v.Y + 1, v.X].direction = 3;
                    else availableCells[v.Y + 1, v.X].direction = availableCells[v.Y, v.X].direction;
                    q.Enqueue(new Point(v.X, v.Y + 1));
                }
                if (v.X > 0 && availableCells[v.Y, v.X - 1].distance < 0 && board[v.Y, v.X - 1] > 0 && new Point(v.X - 1, v.Y) != opponent)
                {
                    availableCells[v.Y, v.X - 1].distance = availableCells[v.Y, v.X].distance + 1;
                    if (availableCells[v.Y, v.X].direction < 0) availableCells[v.Y, v.X - 1].direction = 2;
                    else availableCells[v.Y, v.X - 1].direction = availableCells[v.Y, v.X].direction;
                    q.Enqueue(new Point(v.X - 1, v.Y));
                }
                if (v.X < 8 && availableCells[v.Y, v.X + 1].distance < 0 && board[v.Y, v.X + 1] > 0 && new Point(v.X + 1, v.Y) != opponent)
                {
                    availableCells[v.Y, v.X + 1].distance = availableCells[v.Y, v.X].distance + 1;
                    if (availableCells[v.Y, v.X].direction < 0) availableCells[v.Y, v.X + 1].direction = 1;
                    else availableCells[v.Y, v.X + 1].direction = availableCells[v.Y, v.X].direction;
                    q.Enqueue(new Point(v.X + 1, v.Y));
                }

            }
        }


        protected abstract int FunctionSort (Cell c1, Cell c2);


        public virtual char step(int[,] board, Point player, Point opponent)
        {
            setAvailableCells();
            char[] move = { 'U', 'R', 'L', 'D' };
            availableCells[player.Y, player.X].distance = 0;
            bfs(board, player, opponent);
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (availableCells[i, j].direction >= 0)
                        cells.Add(availableCells[i, j]);
                }
            }
            cells.Sort(FunctionSort);
            int k = 1;
            while (k<cells.Count && FunctionSort(cells[k-1], cells[k]) == 0)
            {
                k++;
            }

            if (cells.Count > 0) {
                Random random= new Random();
                return move[cells[random.Next(k)].direction];
            }
            else
                return move[0];
        }

        protected void setAvailableCells()
        {
            availableCells = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    availableCells[i, j].distance = -1;
                    availableCells[i, j].direction = -1;
                    availableCells[i, j].X = j;
                    availableCells[i, j].Y = i;
                }
            }
        }

        public void ReportGameEnd(bool win)
        {
        }
    }
}
