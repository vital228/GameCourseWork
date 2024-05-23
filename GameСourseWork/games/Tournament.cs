using GameСourseWork.games;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameСourseWork
{
    public class NEventArgs : EventArgs
    {
        public int N { get; set; }
    }

    public class Tournament
    {
        private List<IArtificialIntelligence> _players;
        private string _name;
        private GeneratorBoard.Board _typeBoard;
        private int rounds = 0;

        private TableCup<Point> standing;

        private object locker;

        private int _countRound;

        private int CountRound
        {
            get { return _countRound; }
            set
            {
                if (_countRound != value)
                {
                    _countRound = value;
                    if (RoundChange != null) {
                        var nea = new NEventArgs();
                        nea.N = value;
                        RoundChange(this, nea);
                    }
                }
            }
        }
        private int N;
        private bool _endCup = false;

        public bool EndCup
        {
            get;
            set;
        }


        public event EventHandler<NEventArgs> RoundChange;
        private void ResetPlayers(IArtificialIntelligence player1, IArtificialIntelligence player2)
        {
            player1.Reset();
            player2.Reset();
        }

        public int GetCountPlayer()
        {
            return _players.Count;
        }

        public Tournament(string name):this(name, GeneratorBoard.Board.None){}
        public Tournament(string name , GeneratorBoard.Board typeBoard)
        {
            RoundChange += Tournament_RoundChange;
            _players = new List<IArtificialIntelligence>();
            standing = new TableCup<Point>();
            _name = name;
            _typeBoard = typeBoard;
        }

        private void Tournament_RoundChange(object sender, NEventArgs e)
        {
            if (_players.Count * _players.Count * N == e.N)
            {
                EndCup= true;
            }
        }

        public Point this[int i, int j]
        {
            get { return standing[i, j]; }
        }

        public void AddPlayer(IArtificialIntelligence player)
        {
            _players.Add(player);
            TableCup<Point> new_table = new TableCup<Point>(_players.Count, _players.Count);
            for (int i=0; i<_players.Count; i++)
            {
                for (int j=0; j < _players.Count; j++)
                {
                    if (standing.Rows>i && standing.Columns > j)
                    {
                        new_table[i, j] = standing[i, j];
                    }
                    else
                    {
                        new_table[i, j] = new Point(0,0);
                    }
                }
            }
            standing = new_table;
        }


        public void PlayTournament()
        {
            Random random = new Random();

            for (int i = 0; i < _players.Count; i++)
            {
                for (int j = 0; j < _players.Count; j++)
                {
                    ResetPlayers(_players[i], _players[j]);
                    Game game = new Game(_players[i], _players[j], _typeBoard);
                    int winner = game.playGame();
                    if (winner == 1)
                    {
                        standing[i, j] = new Point(standing[i, j].X + 1, standing[i, j].Y);
                    }
                    else if (winner == 2)
                    {
                        standing[i, j] = new Point(standing[i,j].X, standing[i, j].Y + 1);
                    }
                    else
                    {
                        Console.WriteLine("The match between " + _players[i].Name + " and " + _players[j].Name + " ended in a draw");
                    }
                    CountRound += 1;
                    if (random.Next(100) == 63) 
                        game.saveGame(_name + "\\" + _players[i].Name + "-" + _players[j].Name + "_game#" + (standing[i,j].X + standing[i,j].Y));
                }
            }
        }


        public void PlayTournamentWithParallel(int n)
        {
            CountRound = 0;
            locker = new object();
            N = n;
            EndCup = false;
            rounds += n;
            List<Thread> threads = new List<Thread>();
            var numThreads = 100;
            var toProcess = numThreads;
            ThreadPool.SetMaxThreads(100,100);
            var resetEvent = new ManualResetEvent(false);
            for (int i = 0; i < _players.Count; i++)
            {
                for (int j = 0; j < _players.Count; j++)
                {
                    int ti = i, tj = j, tn = n;
                    
                    ThreadPool.QueueUserWorkItem(
                        new WaitCallback(delegate (object state) {
                            PlayGames(ti, tj, tn);
                            if (Interlocked.Decrement(ref toProcess) == 0) resetEvent.Set();
                        }), null);
                    //ThreadPool.UnsafeQueueUserWorkItem(() => PlayGames(ti, tj, tn));
                    //thread.Start();
                    //threads.Add(thread);
                   
                }
            }
            while (!EndCup) { }
            EndCup = false;
            for (int i=0; i < threads.Count; i++)
            {
                threads[i].Join();
            }
        }

        private void PlayGames(int i, int j, int n)
        {
            IArtificialIntelligence p1 = (IArtificialIntelligence)_players[i].Clone();
            IArtificialIntelligence p2 = (IArtificialIntelligence)_players[j].Clone();
            int w1=0, w2=0;
            for (int k = 0; k < n; k++)
            {
                ResetPlayers(p1, p2);

                Game game = new Game(p1, p2, _typeBoard);
                int winner = game.playGame();
                Random random = new Random();
                if (winner == 1)
                {
                    w1++;
                }
                else if (winner == 2)
                {
                    w2++;
                }
                else
                {
                    Console.WriteLine("The match between " + _players[i].Name + " and " + _players[j].Name + " ended in a draw");
                }
                if (random.Next(1000) == 63)
                    game.saveGame(_name + "\\" + _players[i].Name + "-" + _players[j].Name + "_game#" + (w1 + w2) +".json");
            }
            lock (locker)
            {
                standing[i, j] = new Point(standing[i, j].X + w1, standing[i, j].Y + w2);
                CountRound += n;
            }
        }



        public void PlayTournament(int n)
        {
            CountRound = 0;
            for (int i = 0; i < n; i++)
            {
                rounds++;
                Console.WriteLine(i);
                PlayTournament();
            }
        }


        public string ToHtmlTable()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table border=\"1\">");
            sb.AppendLine($"<caption>{_name}</caption>");
            // создаем заголовок таблицы
            sb.AppendLine("<tr><td></td>");
            for (int j = 0; j < standing.Columns; j++)
            {
                sb.AppendLine($"<th>{_players[j].Name}</th>");
            }
            sb.AppendLine("<th>Avg.W1</th>");
            sb.AppendLine("<th>w1</th>");
            sb.AppendLine("<th>d1</th>");
            sb.AppendLine("<th>Avg.W2</th>");
            sb.AppendLine("<th>w2</th>");
            sb.AppendLine("<th>d2</th>");
            sb.AppendLine("</tr>");

            // создаем строки таблицы
            for (int i = 0; i < standing.Rows; i++)
            {
                sb.AppendLine($"<tr><th>{_players[i].Name}</th>");
                int sum = 0;
                int countWin = 0;
                int countDraw = 0;
                int sum2 = 0;
                int countWin2 = 0;
                int countDraw2 = 0;
                for (int j = 0; j < standing.Columns; j++)
                {
                    sb.Append("<td BGCOLOR=\"#");
                    if (standing[i, j].X - 0.1*rounds >= standing[i, j].Y)
                    {
                        sb.Append("00FF00");

                    }else if (standing[i, j].X > standing[i, j].Y)
                    {
                        sb.Append("90EE90");
                    }
                            
                    if (standing[i, j].X + 0.1*rounds <= standing[i, j].Y)
                    {
                        sb.Append("FF0000");
                    }else if (standing[i, j].X < standing[i, j].Y)
                    {
                        sb.Append("FA8072");
                    }
                    if (standing[i, j].X == standing[i, j].Y)
                    {
                        sb.Append("FFFFFF");
                    }
                    sb.AppendLine($"\">{ standing[i, j].X};{ standing[i, j].Y}</ td > ");
                    sum += standing[i, j].X;
                    sum2 += standing[j, i].Y;
                    if (10*(standing[i, j].X- standing[i, j].Y) >= rounds)
                    {
                        countWin++;
                    }
                    else if (10 * Math.Abs(standing[i, j].X - standing[i, j].Y) < rounds)
                    {
                        countDraw++;
                    }
                    if (10*(standing[j, i].Y - standing[j, i].X) >= rounds)
                    {
                        countWin2++;
                    }
                    else if (10*Math.Abs(standing[j, i].Y - standing[j, i].X) < rounds)
                    {
                        countDraw2++;
                    }
                }
                sb.AppendLine($"<td BGCOLOR=\"#FFFF80\">{sum/_players.Count}</td>");
                sb.AppendLine($"<td BGCOLOR=\"#FFFF80\">{countWin}</td>");
                sb.AppendLine($"<td BGCOLOR=\"#FFFF80\">{countDraw}</td>");
                sb.AppendLine($"<td BGCOLOR=\"#FFFF80\">{sum2/_players.Count}</td>");
                sb.AppendLine($"<td BGCOLOR=\"#FFFF80\">{countWin2}</td>");
                sb.AppendLine($"<td BGCOLOR=\"#FFFF80\">{countDraw2}</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }
    }
}
