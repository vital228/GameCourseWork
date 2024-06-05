using GameСourseWork.algorithms;
using GameСourseWork.games;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static GameСourseWork.games.GeneratorBoard;

namespace GameСourseWork
{
    public partial class ConfigurationPlayGameForm : Form
    {
        int minWidth = 185, maxWidth = 525;
        private List<GroupBox> groupBoxStrategies;


        public static T Clone<T>(T controlToClone) where T : Control
        {
            T instance = Activator.CreateInstance<T>();

            Type control = controlToClone.GetType();
            PropertyInfo[] info = control.GetProperties();
            object p = control.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, controlToClone, null);
            foreach (PropertyInfo pi in info)
            {
                if ((pi.CanWrite) && !(pi.Name == "WindowTarget") && !(pi.Name == "Capture"))
                {
                    pi.SetValue(instance, pi.GetValue(controlToClone, null), null);
                }
            }
            return instance;
        }

        public ConfigurationPlayGameForm()
        {
            groupBoxStrategies = new List<GroupBox>();
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
            comboBoxPlayer1.Items.Add("Mixed Strategy");
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
            comboBoxPlayer2.Items.Add("Mixed Strategy");
            //comboBoxPlayer2.Items.Add(new ChatGPTAlgorithm());
            // TODO: Add an AI algorithm for selecting the second player for the game. See the example above.

            for (int i = 0; i < 2; i++)
            {
                var newBox = Clone(groupBoxStrategy1);
                setGroupBoxStrategies(newBox);
                groupBoxStrategies.Add(newBox);
                flowLayoutPanel1.Controls.Add(groupBoxStrategies.Last());
                groupBoxStrategies.Last().Visible = false;
                groupBoxStrategies.Last().Text += " " + (i+1);
            }


            comboBoxPlayer1.SelectedIndex = 0;
            comboBoxPlayer2.SelectedIndex = 0;
            comboBoxBoard.SelectedIndex = 0;
            
        }
        private void setGroupBoxStrategies(GroupBox box)
        {
            int i = 0, j = 0;
            for (int k = 0; k < PureStrategies.Count; k++)
            {
                var strategy = PureStrategies.Get(k);
                var label = Clone(labelStrategy);
                label.Text = strategy.ToString();
                label.Location = new Point(i + 10, j + 30);
                label.Visible = true;
                var numeric = Clone(numericUpDownStrategy);
                numeric.Location = new Point(label.Size.Width + 15, j + 30);
                numeric.Visible = true;
                box.Controls.Add(numeric);
                box.Controls.Add(label);
                j += 30;
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            IArtificialIntelligence ai1, ai2;
            if (comboBoxPlayer1.SelectedIndex == 0)
            {
                ai1 = null;
            }else if(comboBoxPlayer1.SelectedItem.ToString() == "Mixed Strategy")
            {
                int sum = 0;
                List<WeightStrategy> weightStrategies = new List<WeightStrategy>();
                int i = 0;
                foreach (var controls in groupBoxStrategies[0].Controls)
                {
                    if (controls is NumericUpDown)
                    {
                        int val = (int)((NumericUpDown)controls).Value;
                        weightStrategies.Add(new WeightStrategy(PureStrategies.Get(i), val));
                        sum += val;
                        i++;
                    }
                }
                if (sum == 0)
                {
                    Console.WriteLine("Sum equal 0");
                    return;
                }
                ai1 = new MixedStrategy(weightStrategies);
            }
            else
            {
                ai1 = (IArtificialIntelligence) comboBoxPlayer1.SelectedItem;
            }

            if (comboBoxPlayer2.SelectedIndex == 0)
            {
                ai2 = null;
            }
            else if (comboBoxPlayer2.SelectedItem.ToString() == "Mixed Strategy")
            {
                int sum = 0;
                List<WeightStrategy> weightStrategies = new List<WeightStrategy>();
                int i = 0;
                foreach (var controls in groupBoxStrategies[1].Controls)
                {
                    if (controls is NumericUpDown)
                    {
                        int val = (int)((NumericUpDown)controls).Value;
                        weightStrategies.Add(new WeightStrategy(PureStrategies.Get(i), val));
                        sum += val;
                        i++;
                    }
                }
                if (sum == 0)
                {
                    Console.WriteLine("Sum equal 0");
                    return;
                }
                ai2 = new MixedStrategy(weightStrategies);
            }
            else
            {
                ai2 = (IArtificialIntelligence) comboBoxPlayer2.SelectedItem;
            }

            Game game = new Game(ai1, ai2, (Board) comboBoxBoard.SelectedIndex);
            PlayGameForm form = new PlayGameForm(game);
            form.Show();
        }

        private void comboBoxPlayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlayer1.SelectedItem.ToString() == "Mixed Strategy")
            {
                this.Width = maxWidth;
                groupBoxStrategies[0].Visible= true;
            }
            else
            {
                if (comboBoxPlayer2.SelectedItem != null && comboBoxPlayer2.SelectedItem.ToString() != "Mixed Strategy")
                    this.Width = minWidth;
                groupBoxStrategies[0].Visible = false;
            }
        }

        private void comboBoxPlayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlayer2.SelectedItem.ToString() == "Mixed Strategy")
            {
                this.Width = maxWidth;
                groupBoxStrategies[1].Visible = true;
            }
            else
            {
                if (comboBoxPlayer1.SelectedItem.ToString() != "Mixed Strategy")
                    this.Width = minWidth;
                groupBoxStrategies[1].Visible = false;
            }
        }
    }
}
