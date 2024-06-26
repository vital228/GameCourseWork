﻿using System;
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
            if (canStep(move))
            {
                switch (move)
                {
                    case 'R':
                        board[player.Y, player.X]--;
                        player.X++;
                        break;
                    case 'L':
                        board[player.Y, player.X]--;
                        player.X--;
                        break;
                    case 'U':
                        board[player.Y, player.X]--;
                        player.Y--;
                        break;
                    case 'D':
                        board[player.Y, player.X]--;
                        player.Y++;
                        break;
                    default:
                        return 1;
                }
                return 0;
            }
            else {
                return 1;
            }
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

        private int EndGame(int win)
        {
            winner = win;
            player[3 - win] = new Point(-1, -1);
            if (ai[win] != null)
                ai[win].ReportGameEnd(true);
            if (ai[3 - win] != null)
                ai[3 - win].ReportGameEnd(false);
            return win;
        }

        public int playGame()
        {
            if (ai[1] == null || ai[2] == null) return -1;
            if (winner > 0) return EndGame(winner);
            while (true)
            {
                char move;
                
                try
                {
                    move = ai[1].step(ToOneZero(board), player[1], player[2]);
                }
                catch (Exception e)
                {
                    return EndGame(2);
                }
                //move = ai[1].step(ToOneZero(board), player[1], player[2]);
                lastTurn = move;
                int isCorrectMove = movePlayer(move, ref player[1]);
                isTurn = 2;
                informationBoards.Add(createInfoBoard());
                if (isCorrectMove == 1) {
                    winner = 2;
                    informationBoards[informationBoards.Count - 1].Player[1] = new Point(-1, -1);
                    return EndGame(2); 
                }
                try
                {
                    move = ai[2].step(ToOneZero(board), player[2], player[1]);
                }
                catch(Exception e)
                {
                    winner = 1;
                    return EndGame(1);
                }
                lastTurn = move;
                isCorrectMove = movePlayer(move, ref player[2]);
                isTurn = 1;
                informationBoards.Add(createInfoBoard());
                if (isCorrectMove == 1)
                {
                    winner = 1;
                    informationBoards[informationBoards.Count - 1].Player[2] = new Point(-1, -1);
                    return EndGame(1);
                }
            }
        }

        public int step()
        {
            if (winner > 0) return EndGame(winner);
            if (ai[isTurn] == null) return -1;
            char move = '\0';
            try
            {
                move = ai[isTurn].step(ToOneZero(board), player[isTurn], player[3 - isTurn]);
            }
            catch (Exception e)
            {
                winner = 3 - isTurn;
                return EndGame(3 - isTurn);
            }
            lastTurn = move;
            int isCorrectMove = movePlayer(move, ref player[isTurn]);
            isTurn = 3 - isTurn;
            informationBoards.Add(createInfoBoard());
            if (isCorrectMove == 1)
            {
                winner = isTurn;
                informationBoards[informationBoards.Count - 1].Player[3 - isTurn] = new Point(-1, -1);
                return EndGame(isTurn);
            }
            return 0;
        }
        public int step(char move)
        {
            if (winner > 0) return EndGame(winner);
            lastTurn = move;
            int isCorrectMove = movePlayer(move, ref player[isTurn]);
            isTurn = 3 - isTurn;
            informationBoards.Add(createInfoBoard());
            if (isCorrectMove == 1)
            {
                winner = isTurn;
                informationBoards[informationBoards.Count - 1].Player[3 - isTurn] = new Point(-1, -1);
                return EndGame(isTurn);
            }
            return 0;
        }

        public bool canStep(char move)
        {
            Point player0 = player[isTurn];
            switch (move){
                case 'R':
                    return (isMoveAllowed(new Point(player0.X + 1, player0.Y)));
                case 'L':
                    return (isMoveAllowed(new Point(player0.X - 1, player0.Y)));
                case 'U':
                    return (isMoveAllowed(new Point(player0.X, player0.Y - 1)));
                case 'D':
                    return (isMoveAllowed(new Point(player0.X, player0.Y + 1)));
                default:
                    return false;
            }
        }
        public void saveGame(string fileName)
        {

            // Сериализуем список informationBoards в JSON-строку
            string jsonString = JsonConvert.SerializeObject(informationBoards, Formatting.Indented);
            try
            {
                // Сохраняем JSON-строку в файл
                File.WriteAllText(fileName, jsonString);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int onePlayer()
        {
            int count = 0;
            while (true)
            {
                char move;
                try
                {
                    move = ai[winner].step(ToOneZero(board), player[winner], player[3-winner]);
                }
                catch (Exception e)
                {
                    return count;
                }
                lastTurn = move;
                int isCorrectMove = movePlayer(move, ref player[winner]);
                isTurn = winner;
                count++;
                informationBoards.Add(createInfoBoard());
                if (isCorrectMove == 1)
                {
                    informationBoards[informationBoards.Count - 1].Player[winner] = new Point(-1, -1);
                    return count;
                }
            }
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
