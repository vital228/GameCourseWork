using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork
{
    public class FunctionAlgorithm : IArtificialIntelligence
    {
        public string Name { get => "Function"; set { } }

        private int lastMove;

        private bool[,] used;

        public override string ToString()
        {
            return Name;
        }
        
        private int bfs(int[,] board, Point s, Point opponent, Point player)
        {
            if (s.X < 0 || s.X > 8) return -1;
            if (s.Y < 0 || s.Y > 8) return -1;
            if (board[s.Y, s.X] == 0) return -1;
            if (s == opponent) return -1;
            used= new bool[9, 9];
            used[s.Y, s.X] = true;

            int sum = 0;
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(s);
            while (q.Count > 0)
            {
                Point v = q.Dequeue();
                if (v == player) continue;
                if (v.Y>0 && !used[v.Y-1, v.X] && board[v.Y-1, v.X]>0)
                {
                    used[v.Y - 1, v.X] = true;
                    q.Enqueue(new Point(v.X, v.Y-1));
                    sum++;
                }
                if (v.Y < 8 && !used[v.Y + 1, v.X] && board[v.Y + 1, v.X] > 0)
                {
                    used[v.Y + 1, v.X] = true;
                    q.Enqueue(new Point(v.X, v.Y + 1));
                    sum++;
                }
                if (v.X > 0 && !used[v.Y, v.X-1] && board[v.Y, v.X - 1] > 0)
                {
                    used[v.Y, v.X-1] = true;
                    q.Enqueue(new Point(v.X-1, v.Y));
                    sum++;
                }
                if (v.X < 8 && !used[v.Y, v.X + 1] && board[v.Y, v.X + 1] > 0)
                {
                    used[v.Y, v.X + 1] = true;
                    q.Enqueue(new Point(v.X + 1, v.Y));
                    sum++;
                }

            }
            return sum;
        }

        public void Reset()
        {
            lastMove = -1;
        }

        public FunctionAlgorithm()
        {
            lastMove = -1;
        }


        private void initK(int[,] board, Point player, ref int[] k)
        {
            int i = player.Y;
            int j = player.X;
            if (i >= 2 && board[i - 2, j] > 0)
            {
                k[0]++;
            }
            if (i <= 6 && board[i + 2, j] > 0)
            {
                k[3]++;
            }
            if (j >= 2 && board[i, j - 2] > 0)
            {
                k[2]++;
            }
            if (j <= 6 && board[i, j + 2] > 0)
            {
                k[1]++;
            }
            if (i >= 1 && j>=1 && board[i-1, j - 1] > 0)
            {
                k[0]++;
                k[2]++;
            }
            if (i < 8 && j >= 1 && board[i + 1, j - 1] > 0)
            {
                k[3]++;
                k[2]++;
            }
            if (i >= 1 && j < 8 && board[i - 1, j + 1] > 0)
            {
                k[0]++;
                k[1]++;
            }
            if (i < 8 && j < 8 && board[i + 1, j + 1] > 0)
            {
                k[3]++;
                k[1]++;
            }
        }

        public char step(int[,] board, Point player, Point opponent)
        {
            char[] move = { 'U', 'R', 'L', 'D' };

            int[] c = { bfs(board, new Point(player.X, player.Y - 1), opponent, player),
             bfs(board, new Point(player.X + 1, player.Y), opponent, player),
             bfs(board, new Point(player.X - 1, player.Y), opponent, player),
             bfs(board, new Point(player.X, player.Y + 1), opponent, player)};
            int[] k = { 0, 0, 0, 0 };
            initK(board, player, ref k);
            int im = 0;
            int mx = c[0] * (k[0] + 1);
            if (lastMove >= 0) {
                im = 3 - lastMove;
                mx = c[im] * (k[im] + 1);
            }
            for (int i = 0; i < 4; i++)
            {
                if (c[i] * (k[i] + 1) > mx)
                {
                    im = i;
                    mx = c[i] * (k[i] + 1);
                }
            }
            lastMove = im;
            return move[im];
        }
    }
}
