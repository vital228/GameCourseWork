using GameСourseWork.algorithms;
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
using static System.Net.Mime.MediaTypeNames;

namespace GameСourseWork
{
    public partial class ConfigurationCupForm : Form
    {
        public ConfigurationCupForm()
        {
            InitializeComponent();
        }

        private void buttonStartCup_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                return;
            }
            var cup = new Tournament(textBox1.Text);

            string folderName = textBox1.Text;

            string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            Directory.CreateDirectory(path);
            cup.AddPlayer(new FollowEnemyAlgorithm());
            cup.AddPlayer(new FunctionAlgorithm());
            cup.AddPlayer(new ToCentreAlgorithm());
            cup.AddPlayer(new ToEdgeBoardAlgorithm());
            cup.AddPlayer(new ToFarthestCellAlgorithm());
            cup.AddPlayer(new PotentialAlgorithm());
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
            
            string path = textBox1.Text + "\\" + file + ".html";
            // сохраняем текст в файл
            File.WriteAllText(path, text);
        }
    }
}
