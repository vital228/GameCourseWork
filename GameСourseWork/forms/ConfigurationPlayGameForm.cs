using GameСourseWork.algorithms;
using GameСourseWork.games;
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
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                comboBoxPlayer1.Items.Add(PureStrategies.Get(i));
            }
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                comboBoxPlayer1.Items.Add(new ArticulationPointsModificationAlgorithm(PureStrategies.Get(i)));
            }
            //comboBoxPlayer1.Items.Add(new ChatGPTAlgorithm());
            // TODO: Add an AI algorithm for selecting the first player for the game. See the example above.
            comboBoxPlayer2.Items.Add("Человек");
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                comboBoxPlayer2.Items.Add(PureStrategies.Get(i));
            }
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                comboBoxPlayer2.Items.Add(new ArticulationPointsModificationAlgorithm(PureStrategies.Get(i)));
            }
            //comboBoxPlayer2.Items.Add(new ChatGPTAlgorithm());
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
