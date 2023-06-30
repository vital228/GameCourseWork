using GameСourseWork.games;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork
{
    public class Tournament
    {
        private List<IArtificialIntelligence> _players;
        private string _name;
        private GeneratorBoard.Board _typeBoard;
        private int rounds = 0;

        private TableCup<Point> standing;

        private void ResetPlayers(IArtificialIntelligence player1, IArtificialIntelligence player2)
        {
            player1.Reset();
            player2.Reset();
        }

        public Tournament(string name):this(name, GeneratorBoard.Board.None){}
        public Tournament(string name , GeneratorBoard.Board typeBoard)
        {
            _players = new List<IArtificialIntelligence>();
            standing = new TableCup<Point>();
            _name = name;
            _typeBoard = typeBoard;
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
            rounds++;
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

                    if (random.Next(100) == 63) 
                        game.saveGame(_name + "\\" + _players[i].Name + "-" + _players[j].Name + "_game#" + (standing[i,j].X + standing[i,j].Y));
                }
            }
        }

        public void PlayTournament(int n)
        {
            for (int i = 0; i < n; i++)
            {
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
                    if (standing[i,j].X- 0.1 * rounds >= standing[i, j].Y)
                    {
                        countWin++;
                    }else if (Math.Abs(standing[i, j].X - standing[i, j].Y) < 0.1 * rounds)
                    {
                        countDraw++;
                    }
                    if (standing[j, i].Y - 0.1 * rounds >= standing[j, i].X)
                    {
                        countWin2++;
                    }
                    else if (Math.Abs(standing[j, i].Y - standing[j, i].X) < 0.1 * rounds)
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
