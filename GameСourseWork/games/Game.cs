using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using GameСourseWork.games;
using static GameСourseWork.games.GeneratorBoard;

namespace GameСourseWork
{
    public struct InformationBoard
    {
        [JsonConverter(typeof(TwoDimensionalArrayConverter))]
        public int[,] Board { get; set; }
        public Point[] Player { get; set; }
        public int IsNextTurn { get; set; }
        public char LastTurn { get; set; }
    }



    public class Game
    {
        int[,] board;
        Point[] player;
        public IArtificialIntelligence[] ai;
        public int isTurn;
        char lastTurn;
        public List<InformationBoard> informationBoards = new List<InformationBoard>();
        int winner = 0;

        public Game():this(Board.None){}

        public Game(Board type)
        {
            board = GeneratorBoard.Generator(type);
            player = new Point[3];
            ai = new IArtificialIntelligence[3];
            player[1] = new Point(1, 1);
            player[2] = new Point(7, 7);
            isTurn = 1;
            lastTurn = 'O';
            informationBoards.Add(createInfoBoard());
        }

        private InformationBoard createInfoBoard()
        {
            var info = new InformationBoard();
            info.IsNextTurn = isTurn;
            info.LastTurn = lastTurn;
            info.Player = new Point[3];
            for (int i = 1; i < 3; i++)
            {
                info.Player[i]= new Point(player[i].X, player[i].Y);
            }
            info.Board = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j< 9; j++)
                {
                    info.Board[i, j] = board[i, j];
                }
            }
            return info;
        }

        public Game(IArtificialIntelligence ai1, IArtificialIntelligence ai2) : this(ai1, ai2, Board.None){}

        public Game(IArtificialIntelligence ai1, IArtificialIntelligence ai2, Board type) : this(type)
        {
            ai[1] = ai1;
            ai[2] = ai2;
        }

        public bool isMoveAllowed(Point cell)
        {
            if (cell.X >= 9 || cell.X < 0 || cell.Y >= 9 || cell.Y < 0) return false;
            if (player[1] == cell || player[2] == cell) return false;
            return board[cell.Y, cell.X] > 0;
        }

        public static bool isMoveAllowed(int[,] board, Point cell)
        {
            if (cell.X >= 9 || cell.X < 0 || cell.Y >= 9 || cell.Y < 0) return false;
            return board[cell.Y, cell.X] > 0;
        }

        private int movePlayer(char move, ref Point player)
        {
            switch (move)
            {
                case 'R':
                    if (isMoveAllowed(new Point(player.X + 1, player.Y)))
                    {
                        board[player.Y, player.X]--;
                        player.X++;
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case 'L':
                    if (isMoveAllowed(new Point(player.X - 1, player.Y)))
                    {
                        board[player.Y, player.X]--;
                        player.X--;
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case 'U':
                    if (isMoveAllowed(new Point(player.X, player.Y - 1)))
                    {
                        board[player.Y, player.X]--;
                        player.Y--;
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case 'D':
                    if (isMoveAllowed(new Point(player.X, player.Y + 1)))
                    {
                        board[player.Y, player.X]--;
                        player.Y++;
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                default:
                    return 1;
            }
            return 0;
        }
        
        private int[,] ToOneZero(int[,] board)
        {
            int[,] new_board = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    new_board[i, j]= (board[i, j]>0?1:0);
                }
            }
            return new_board;
        }

        public int playGame()
        {
            if (ai[1] == null || ai[2] == null) return -1;
            if (winner > 0) return winner;
            while (true)
            {
                char move;
                try
                {
                    move = ai[1].step(ToOneZero(board), player[1], player[2]);
                }
                catch (Exception e)
                {
                    winner = 2;
                    return 2;
                }
                lastTurn = move;
                int isCorrectMove = movePlayer(move, ref player[1]);
                isTurn = 2;
                informationBoards.Add(createInfoBoard());
                if (isCorrectMove == 1) {
                    winner = 2;
                    informationBoards[informationBoards.Count - 1].Player[1] = new Point(-1, -1);
                    return 2; 
                }
                try
                {
                    move = ai[2].step(ToOneZero(board), player[2], player[1]);
                }
                catch(Exception e)
                {
                    winner = 1;
                    return 1;
                }
                lastTurn = move;
                isCorrectMove = movePlayer(move, ref player[2]);
                isTurn = 1;
                informationBoards.Add(createInfoBoard());
                if (isCorrectMove == 1)
                {
                    winner = 1;
                    informationBoards[informationBoards.Count - 1].Player[2] = new Point(-1, -1);
                    return 1;
                }
            }
        }

        public int step()
        {
            if (winner > 0) return winner;
            if (ai[isTurn] == null) return -1;
            char move = '\0';
            try
            {
                move = ai[isTurn].step(ToOneZero(board), player[isTurn], player[3 - isTurn]);
            }
            catch (Exception e)
            {
                winner = 3 - isTurn;
                return 3 - isTurn;
            }
            lastTurn = move;
            int isCorrectMove = movePlayer(move, ref player[isTurn]);
            isTurn = 3 - isTurn;
            informationBoards.Add(createInfoBoard());
            if (isCorrectMove == 1)
            {
                winner = isTurn;
                informationBoards[informationBoards.Count - 1].Player[3 - isTurn] = new Point(-1, -1);
                return isTurn;
            }
            return 0;
        }
        public int step(char move)
        {
            if (winner > 0) return winner;
            lastTurn = move;
            int isCorrectMove = movePlayer(move, ref player[isTurn]);
            isTurn = 3 - isTurn;
            informationBoards.Add(createInfoBoard());
            if (isCorrectMove == 1)
            {
                winner = isTurn;
                informationBoards[informationBoards.Count - 1].Player[3 - isTurn] = new Point(-1, -1);
                return isTurn;
            }
            return 0;
        }

        public bool canStep(char move)
        {
            Point player0 = player[isTurn];
            int isCorrectMove = movePlayer(move, ref player0);
            return isCorrectMove == 0;
        }
        public void saveGame(string fileName)
        {

            // Сериализуем список informationBoards в JSON-строку
            string jsonString = JsonConvert.SerializeObject(informationBoards, Formatting.Indented);

            // Сохраняем JSON-строку в файл
            File.WriteAllText(fileName, jsonString);
        }

        public string loadGame(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                informationBoards = JsonConvert.DeserializeObject<List<InformationBoard>>(jsonString);
                player = informationBoards.Last().Player;
                board = informationBoards.Last().Board;
                isTurn = informationBoards.Last().IsNextTurn;
                lastTurn = informationBoards.Last().LastTurn;
            }
            catch (FileNotFoundException)
            {
                return "Файл не найден";
            }
            catch (JsonException)
            {
                // Обработка исключения: ошибка при десериализации JSON
                return "Обработка исключения: ошибка при десериализации JSON";
            }

            return null;
        }

}
}
