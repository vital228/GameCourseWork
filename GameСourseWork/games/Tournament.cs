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

        private TableCup<Point> standing;

        private void ResetPlayers(IArtificialIntelligence player1, IArtificialIntelligence player2)
        {
            player1.Reset();
            player2.Reset();
        }

        public Tournament(string name)
        {
            _players = new List<IArtificialIntelligence>();
            standing = new TableCup<Point>();
            _name = name;
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
            for (int i = 0; i < _players.Count; i++)
            {
                for (int j = 0; j < _players.Count; j++)
                {
                    ResetPlayers(_players[i], _players[j]);
                    Game game = new Game(_players[i], _players[j]);
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
                    game.saveGame(_name + "\\" + _players[i].Name + "-" + _players[j].Name + "_game#" + (standing[i,j].X + standing[i,j].Y));
                }
            }
        }

        public void PlayTournament(int n)
        {
            for (int i = 0; i < n; i++)
                PlayTournament();
        }

        public string ToHtmlTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table border=\"1\">");

            // создаем заголовок таблицы
            sb.AppendLine("<tr><td></td>");
            for (int j = 0; j < standing.Columns; j++)
            {
                sb.AppendLine($"<th>{_players[j].Name}</th>");
            }
            sb.AppendLine("</tr>");

            // создаем строки таблицы
            for (int i = 0; i < standing.Rows; i++)
            {
                sb.AppendLine($"<tr><th>{_players[i].Name}</th>");
                for (int j = 0; j < standing.Columns; j++)
                {
                    sb.AppendLine($"<td>{standing[i, j].X} ; {standing[i, j].Y}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            return sb.ToString();
        }
    }
}
