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
    public partial class ConfigurationPlayGameForm : Form
    {
        public ConfigurationPlayGameForm()
        {
            InitializeComponent();
            comboBoxPlayer1.Items.Add("Человек");
            comboBoxPlayer1.Items.Add(new FunctionAlgorithm());
            comboBoxPlayer1.Items.Add(new FollowEnemyAlgorithm());
            comboBoxPlayer2.Items.Add("Человек");
            comboBoxPlayer2.Items.Add(new FunctionAlgorithm());
            comboBoxPlayer2.Items.Add(new FollowEnemyAlgorithm());
            comboBoxPlayer1.SelectedIndex = 0;
            comboBoxPlayer2.SelectedIndex = 0;
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

            Game game = new Game(ai1, ai2);
            PlayGameForm form = new PlayGameForm(game);
            form.Show();
        }
    }
}
