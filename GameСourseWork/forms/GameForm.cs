using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameСourseWork
{
    public partial class GameForm : Form
    {
        private const int Rows = 9;
        private const int Columns = 9;

        private readonly Button[,] _cells;

        private Game game;

        public GameForm(Game game, string name1, string name2)
        {
            InitializeComponent();

            label2.ForeColor = Color.Red;
            label2.Text = name1;
            label3.ForeColor = Color.Blue;
            label3.Text = name2;
            this.game = game;

            stepBar.Maximum = game.informationBoards.Count - 1;
            stepBar.Value = 0;
            // Создаем кнопки для каждой клетки поля и добавляем их в таблицу
            _cells = new Button[Rows, Columns];
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    var cell = new Button
                    {
                        Dock = DockStyle.None,
                        Tag = new Point(row, column),
                        Size = new Size(50, 50),
                        Margin = new Padding(1),
                    };
                    cell.Click += Cell_Click;
                    table.Controls.Add(cell, column, row);
                    _cells[row, column] = cell;
                }
            }
            setBoard(0);
            // Добавляем таблицу на форму
            Controls.Add(table);
        }





        private void Cell_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var position = (Point)button.Tag;

            // Обработка клика по клетке

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            setBoard(stepBar.Value);
        }
        private void setBoard(int step)
        {
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    if (game.informationBoards[step].Player[1].X == column && game.informationBoards[step].Player[1].Y == row)
                    {
                        _cells[row, column].Text = "A";
                        _cells[row, column].ForeColor = Color.Red;
                    }
                    else if (game.informationBoards[step].Player[2].X == column && game.informationBoards[step].Player[2].Y == row)
                    {
                        _cells[row, column].Text = "B";
                        _cells[row, column].ForeColor = Color.Blue;
                    }
                    else if (game.informationBoards[step].Board[row, column] > 0){
                        _cells[row, column].Text = game.informationBoards[step].Board[row, column].ToString();
                        _cells[row, column].ForeColor = Color.Green;
                    }
                    else
                    {
                        _cells[row, column].Text = game.informationBoards[step].Board[row, column].ToString();
                        _cells[row, column].ForeColor = Color.Gray;
                    }
                }
            }
        }
    }
}
