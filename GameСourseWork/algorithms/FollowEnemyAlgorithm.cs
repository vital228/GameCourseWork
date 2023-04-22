using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork
{
    public class FollowEnemyAlgorithm : IArtificialIntelligence
    {
        public string Name { get => "Follow Enemy"; set { } }

        public override string ToString()
        {
            return Name;
        }

        public FollowEnemyAlgorithm() { 

        }

        public void Reset()
        {
            
        }

        List<Point> findPath(int[,] board, Point player, Point opponent)
        {
            var path = new List<Point>();
            var father = new Point[9, 9];
            for (int i = 0; i < 81; i++)
            {
                father[i / 9, i % 9] = new Point(-1, -1); 
            }
            father[player.Y, player.X] = player;
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(player);
            Point fal = new Point(-1, -1);
            while (q.Count > 0)
            {
                Point v = q.Dequeue();
                if (v == opponent)
                {
                    path.Add(v);
                    while (v != player)
                    {
                        v = father[v.Y, v.X];
                        path.Add(v);
                    }
                    return path;
                }

                if (v.Y > 0 && father[v.Y - 1, v.X] == fal  && board[v.Y - 1, v.X] > 0)
                {
                    father[v.Y - 1, v.X] = v;
                    q.Enqueue(new Point(v.X, v.Y - 1));
                }
                if (v.Y < 8 && father[v.Y + 1, v.X] ==fal && board[v.Y + 1, v.X] > 0)
                {
                    father[v.Y + 1, v.X] = v;
                    q.Enqueue(new Point(v.X, v.Y + 1));
                }
                if (v.X > 0 && father[v.Y, v.X - 1] == fal && board[v.Y, v.X - 1] > 0)
                {
                    father[v.Y, v.X - 1] = v;
                    q.Enqueue(new Point(v.X - 1, v.Y));
                }
                if (v.X < 8 && father[v.Y, v.X + 1] == fal && board[v.Y, v.X + 1] > 0)
                {
                    father[v.Y, v.X + 1] = v;
                    q.Enqueue(new Point(v.X + 1, v.Y));
                }
            }
            return path;
        }

        public char step(int[,] board, Point player, Point opponent)
        {
            char[] move = { 'U', 'R', 'L', 'D' };
            List<Point> path = findPath(board, player, opponent);
            path.Reverse();
            if (path.Count > 0)
            {
                if (path[1] == opponent)
                {
                    return new FunctionAlgorithm().step(board, player, opponent);
                }
                else
                {
                    if (path[1].X == player.X + 1) return move[1];
                    if (path[1].X == player.X - 1) return move[2];
                    if (path[1].Y == player.Y + 1) return move[3];
                    if (path[1].Y == player.Y - 1) return move[0];
                }
            }
            
            return new FunctionAlgorithm().step(board, player, opponent);
            
        }
    }
}
