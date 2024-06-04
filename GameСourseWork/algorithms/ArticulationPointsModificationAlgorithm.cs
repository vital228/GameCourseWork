using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace GameСourseWork.algorithms
{
    public class ArticulationPointsModificationAlgorithm : IArtificialIntelligence
    {
        public IArtificialIntelligence Strategy { get; private set; }

        private int V = 81;
        public ArticulationPointsModificationAlgorithm(IArtificialIntelligence strategy)
        {
            Strategy = strategy;
            adj = new List<int>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<int>();
            }
        }

        public string Name { get => "AP + " + Strategy.Name; set { } }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            return new ArticulationPointsModificationAlgorithm((IArtificialIntelligence)Strategy.Clone());
        }

        public void ReportGameEnd(bool win)
        {
           
        }

        public void Reset()
        {
            Strategy.Reset();
        }

        public char step(int[,] board, Point player, Point opponent)
        {
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<int>();
                int k = i / 9, j = i%9;
                if (board[k, j] == 0) continue;
                if (k>0 && board[k - 1, j] > 0)
                {
                    adj[i].Add((k - 1) * 9 + j);
                }
                if (k < 8 && board[k + 1, j] > 0)
                {
                    adj[i].Add((k + 1) * 9 + j);
                }
                if (j > 0 && board[k, j - 1] > 0)
                {
                    adj[i].Add(k * 9 + j - 1);
                }
                if (j < 8 && board[k, j + 1] > 0)
                {
                    adj[i].Add(k * 9 + j + 1);
                }
            }
            List<bool> visited = new List<bool>(V);
            List<int> disc = new List<int>(V);
            List<int> low = new List<int>(V);
            List<int> parent = new List<int>(V);
            List<int> ap = new List<int>(); // To store articulation points

            for (int i = 0; i < V; i++)
            {
                parent.Add(-1);
                visited.Add(false);
                disc.Add(0);
                low.Add(0);
            }
            for (int i = 0; i < V; i++)
                if (!visited[i])
                    APUtil(i, visited, disc, low, parent, ap);
            for (int i = 0; i < V; i++)
            {
                parent[i] = -1;
                visited[i] = false;
            }
            dfs(opponent.Y * 9 + opponent.X, visited, ap);
            List<int> targets = new List<int>();
            foreach(int v in ap)
            {
                if (visited[v])
                {
                    targets.Add(v);
                }
            }
            for (int i = 0; i< V; i++)
            {
                visited[i] = false;
            }
            dfs(player.Y*9 + player.X, visited, targets, opponent.Y * 9 + opponent.X);
            List<int> openTargers = new List<int>();
            foreach (int v in targets)
            {
                if (visited[v])
                {
                    openTargers.Add(v);
                }
            }
            if (openTargers.Count > 0)
            {
                if (Strategy.GetType() == typeof(PotentialAlgorithm))
                    ((PotentialAlgorithm)Strategy).update(player, opponent);
                List<int> dist = new List<int>();
                for (int i=0;i<V;i++)
                {
                    dist.Add(-1);
                }
                bfs(player.Y * 9 + player.X, dist, opponent.Y * 9 + opponent.X);
                List<int> minPath = new List<int>(V);
                int mn = V;
                foreach(int v in openTargers)
                {
                    List<int> path = findMinPath(player.Y * 9 + player.X, v, dist);
                    if (path.Count < mn)
                    {
                        mn= path.Count;
                        minPath = path;
                    }
                }
                (int, int, char)[] directions = {
                                (1, 0, 'D'),
                                (-1, 0, 'U'),
                                (0, 1, 'R'),
                                (0, -1, 'L')
                                };
                if (minPath.Count == 0)
                {
                    for (int i = 0; i < V; i++)
                    {
                        visited[i] = false;
                    }
                    int bestDirectionCount = 0;
                    char bestDirection = 'N';

                    

                    foreach (var (dy, dx, direction) in directions)
                    {
                        if (Math.Abs(player.X + dx - 4) < 5 && Math.Abs(player.Y + dy - 4)<5 && board[player.Y + dy, player.X + dx] > 0 && new Point(player.X +dx, player.Y +dy)!=opponent)
                        {
                            int count = CountVisited((player.Y + dy) * 9 + player.X + dx, visited, player.Y * 9 + player.X);
                            if (count > bestDirectionCount)
                            {
                                bestDirectionCount = count;
                                bestDirection = direction;
                            }
                        }
                    }
                    if (bestDirection == 'N') return Strategy.step(board, player, opponent);
                    return bestDirection;
                }
                else
                {
                    int u =minPath[0];
                    foreach (var (dy, dx, direction) in directions)
                    {
                        if (u == (player.Y + dy)* 9 + player.X + dx)
                        {
                            if (new Point(player.X + dx, player.Y + dy) == opponent) return Strategy.step(board, player, opponent);
                            return direction;
                        }
                    }
                    return Strategy.step(board, player, opponent);
                }
            }
            else
            {
                return Strategy.step(board, player, opponent);
            }
        }

        private int CountVisited(int start, List<bool> visited, int initial)
        {
            int count = 0;
            if (start < 0 && start >= 81) return 0;
            dfs(start, visited, new List<int> { initial });
            for (int i = 0; i < V; i++)
            {
                if (visited[i])
                    count++;
                visited[i] = false;
            }
            return count;
        }

        private void bfs(int s, List<int> dist, int unavailableCell = -1)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            dist[s] = 0;
            while(queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in adj[u])
                {
                    if (dist[v] == -1 && v != unavailableCell)
                    {
                        dist[v] = dist[u] + 1;
                        queue.Enqueue(v);
                    }
                }
            }
        }

        private List<int> findMinPath(int s, int t, List<int> dist)
        {
            List<int> path = new List<int>();
            while (t != s)
            {
                path.Add(t);
                foreach (int v in adj[t])
                {
                    if (dist[t] == dist[v] + 1)
                    {
                        t = v;
                        break;
                    }
                }
            }
            path.Reverse();
            return path;
        }

        private void dfs(int u, List<bool> visited, List<int> ap, int unavailableCell = -1)
        {
            visited[u] = true;
            foreach (int v in adj[u])
            {
                if (!visited[v] && v != unavailableCell)
                {
                    if (ap.Contains(v))
                    {
                        visited[v] = true;
                    }
                    else
                    {
                        dfs(v, visited, ap, unavailableCell);
                    }
                }
            }

        }

        private int time = 0;
        private List<int>[] adj;
        // A recursive function that finds articulation points using DFS traversal
        // u --> The vertex to be visited next
        // visited[] --> keeps track of visited vertices
        // disc[] --> Stores discovery times of visited vertices
        // low[] -- >> earliest visited vertex (the vertex with minimum discovery time) that is reachable from subtree rooted with vertex u
        // parent[] --> Stores parent vertices in DFS tree
        // ap[] --> Store articulation points
        private void APUtil(int u, List<bool> visited, List<int> disc, List<int> low, List<int> parent, List<int> ap)
        {
            int children = 0;
            visited[u] = true;
            disc[u] = low[u] = ++time;
            foreach (int v in adj[u])
            {
                if (!visited[v])
                {
                    children++;
                    parent[v] = u;
                    APUtil(v, visited, disc, low, parent, ap);

                    low[u] = Math.Min(low[u], low[v]);

                    if (parent[u] == -1 && children > 1)
                        ap.Add(u);

                    if (parent[u] != -1 && low[v] >= disc[u])
                        ap.Add(u);
                }
                else if (v != parent[u])
                    low[u] = Math.Min(low[u], disc[v]);
            }
        }
    }
}
