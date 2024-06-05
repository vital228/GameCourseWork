using System;
using System.Drawing;
using System.Windows.Forms;


namespace GameСourseWork
{
    public partial class PlayGameForm : Form
    {

        private const int Rows = 9;
        private const int Columns = 9;

        private readonly Button[,] _cells;

        private Game game;
        private int winner;

        

        public PlayGameForm(Game game)
        {
            InitializeComponent();
            this.game = game;

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
                    
                    table.Controls.Add(cell, column, row);
                    _cells[row, column] = cell;
                }
            }
            // Добавляем таблицу на форму
            Controls.Add(table);
            buttonShow.Enabled = false;
            buttonShow.Visible = false;
            int mv1 = game.step();
            if (mv1 != -1)
            {
                int mv2 = game.step();
                if (mv1 != -1 && mv2 != -1)
                {
                    buttonShow.Enabled = true;
                    buttonShow.Visible = true;
                    buttonR.Enabled = false;
                    buttonL.Enabled = false;
                    buttonU.Enabled = false;
                    buttonD.Enabled = false;
                    buttonR.Visible = false;
                    buttonL.Visible = false;
                    buttonU.Visible = false;
                    buttonD.Visible = false;
                    winner = this.game.playGame();
                    return;
                }
                
            }
            setBoard(game.informationBoards.Count - 1);
        }



        private void buttonShow_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Start();
        }


        private void EndGame()
        {
            game.saveGame((game.ai[1] == null ? "Player" : game.ai[1].Name) + "-" + (game.ai[2] == null ? "Player" : game.ai[2].Name) + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".json");
            MessageBox.Show(
                    "Победил игрок " + winner + ". Конец игры",
                    "Конец игры",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
        }
        private int indexStep = 0;
        

        private void stepPlayer(char move)
        {
            int win = game.step(move);
            if (win > 0)
            {
                winner = win;
                EndGame();
                this.Close();
                return;
            }
            win = game.step();
            if (win == -1)
            {
                setBoard(game.informationBoards.Count - 1);
            }
            else
            {
                if (win > 0)
                {
                    winner = win;
                    EndGame();
                    this.Close();
                    return;
                }
                else
                {
                    setBoard(game.informationBoards.Count - 1);
                }
            }

            buttonD.Enabled = game.canStep('D');
            buttonU.Enabled = game.canStep('U');
            buttonR.Enabled = game.canStep('R');
            buttonL.Enabled = game.canStep('L');
            if (!(buttonD.Enabled || buttonL.Enabled || buttonR.Enabled || buttonU.Enabled)) {
                winner = 3 - game.isTurn;
                EndGame();
                Close();
                return;
            }
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            stepPlayer('L');
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            stepPlayer('R');
        }

        private void buttonU_Click(object sender, EventArgs e)
        {
            stepPlayer('U');
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            stepPlayer('D');
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
                    else if (game.informationBoards[step].Board[row, column] > 0)
                    {
                        _cells[row, column].Text = "1";
                        _cells[row, column].ForeColor = Color.Green;
                        
                    }
                    else
                    {
                        _cells[row, column].Text = "0";
                        _cells[row, column].ForeColor = Color.Gray;
                        
                    }
                }
            }
        }

        

        private void timer_Tick(object sender, EventArgs e)
        {
            
            Console.WriteLine(indexStep);
            if (indexStep>=game.informationBoards.Count)
            {
                timer.Stop();
                EndGame();
                this.Close();
                return;
            }
            else
            {
                setBoard(indexStep);
                indexStep++;
            }
        }
    }
}
