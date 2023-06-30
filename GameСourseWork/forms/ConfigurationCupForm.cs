using GameСourseWork.algorithms;
using GameСourseWork.other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GameСourseWork.games.GeneratorBoard;
using static System.Net.Mime.MediaTypeNames;

namespace GameСourseWork
{
    public partial class ConfigurationCupForm : Form
    {
        public ConfigurationCupForm()
        {
            InitializeComponent();
            comboBoxBoard.SelectedIndex = 0;
        }

        private void buttonStartCup_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                return;
            }
            var cup = new Tournament(textBox1.Text, (Board)comboBoxBoard.SelectedIndex);

            string folderName = textBox1.Text;

            string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            Directory.CreateDirectory(path);
            cup.AddPlayer(new FollowEnemyAlgorithm());
            cup.AddPlayer(new FunctionAlgorithm());
            cup.AddPlayer(new ToCentreAlgorithm());
            cup.AddPlayer(new ToEdgeBoardAlgorithm());
            cup.AddPlayer(new ToFarthestCellAlgorithm());
            cup.AddPlayer(new PotentialAlgorithm());
            //cup.AddPlayer(new ChatGPTAlgorithm());
            NNalgorithm nalgorithm = new NNalgorithm();
            nalgorithm.network = new NeuralNetwork(new int[4] { 85, 128, 64, 4 });
            nalgorithm.network.Load("Save.txt");
            cup.AddPlayer(nalgorithm);
            // TODO: Add an AI algorithm to the tournament. See the example above.

            cup.PlayTournament((int)numericUpDown1.Value);
            string s = cup.ToHtmlTable();

            save("standings", s);
            MessageBox.Show(
                       "Турнир завершен",
                       "Конец турнира",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information
                   );
        }

        private void save(string file, string text)
        {
            
            string path = textBox1.Text + "_" + file + ".html";
            // сохраняем текст в файл
            File.WriteAllText(path, text);
        }

    }
}
