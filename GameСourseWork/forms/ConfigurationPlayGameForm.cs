﻿using GameСourseWork.algorithms;
using System;
using System.Windows.Forms;
using static GameСourseWork.games.GeneratorBoard;

namespace GameСourseWork
{
    public partial class ConfigurationPlayGameForm : Form
    {
        public ConfigurationPlayGameForm()
        {
            InitializeComponent();
            comboBoxPlayer1.Items.Add("Человек");
            comboBoxPlayer1.Items.Add(new FunctionAlgorithm());
            comboBoxPlayer1.Items.Add(new FollowEnemyAlgorithm());
            comboBoxPlayer1.Items.Add(new ToCentreAlgorithm());
            comboBoxPlayer1.Items.Add(new ToEdgeBoardAlgorithm());
            comboBoxPlayer1.Items.Add(new ToFarthestCellAlgorithm());
            comboBoxPlayer1.Items.Add(new PotentialAlgorithm());
            comboBoxPlayer1.Items.Add(new ChatGPTAlgorithm());
            // TODO: Add an AI algorithm for selecting the first player for the game. See the example above.
            comboBoxPlayer2.Items.Add("Человек");
            comboBoxPlayer2.Items.Add(new FunctionAlgorithm());
            comboBoxPlayer2.Items.Add(new FollowEnemyAlgorithm());
            comboBoxPlayer2.Items.Add(new ToCentreAlgorithm());
            comboBoxPlayer2.Items.Add(new ToEdgeBoardAlgorithm());
            comboBoxPlayer2.Items.Add(new ToFarthestCellAlgorithm());
            comboBoxPlayer2.Items.Add(new PotentialAlgorithm());
            comboBoxPlayer2.Items.Add(new ChatGPTAlgorithm());
            // TODO: Add an AI algorithm for selecting the second player for the game. See the example above.
            comboBoxPlayer1.SelectedIndex = 0;
            comboBoxPlayer2.SelectedIndex = 0;
            comboBoxBoard.SelectedIndex = 0;
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            IArtificialIntelligence ai1, ai2;
            if (comboBoxPlayer1.SelectedIndex == 0)
            {
                ai1 = null;
            }
            else
            {
                ai1 = (IArtificialIntelligence) comboBoxPlayer1.SelectedItem;
            }

            if (comboBoxPlayer2.SelectedIndex == 0)
            {
                ai2 = null;
            }
            else
            {
                ai2 = (IArtificialIntelligence) comboBoxPlayer2.SelectedItem;
            }

            Game game = new Game(ai1, ai2, (Board) comboBoxBoard.SelectedIndex);
            PlayGameForm form = new PlayGameForm(game);
            form.Show();
        }
    }
}
