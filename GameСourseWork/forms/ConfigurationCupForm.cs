using GameСourseWork.algorithms;
using GameСourseWork.forms;
using GameСourseWork.games;
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

namespace GameСourseWork
{
    public partial class ConfigurationCupForm : Form
    {
        private List<GroupBox> groupBoxStrategies;
        public ConfigurationCupForm()
        {
            groupBoxStrategies = new List<GroupBox>();
            InitializeComponent();
            comboBoxBoard.SelectedIndex = 0;
        }
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


            List<MixedStrategy> strategies = new List<MixedStrategy>();
            foreach (var comboBoxStrategy in groupBoxStrategies)
            {
                int i = 0;
                int sum = 0;
                List<WeightStrategy> weightStrategies = new List<WeightStrategy>();
                foreach (var controls in comboBoxStrategy.Controls)
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
                else
                {
                    strategies.Add(new MixedStrategy(weightStrategies));
                }
            }

            foreach (var strategy in strategies)
            {
                cup.AddPlayer(strategy);
            }
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                cup.AddPlayer(PureStrategies.Get(i));
            }

            ProgressBarForm progressBarForm = new ProgressBarForm();
            progressBarForm.LoadData(cup, (int)numericUpDown1.Value);


            progressBarForm.ShowDialog();


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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newBox = Clone(groupBoxStrategy);
            setGroupBoxStrategies(newBox);
            groupBoxStrategies.Add(newBox);
            flowLayoutPanel1.Controls.Add(groupBoxStrategies.Last());
            groupBoxStrategies.Last().Visible = true;
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

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    string path = openFileDialog1.FileName;
                    List<int> list = new List<int>();
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            list = line.Split(' ').Select(x => int.Parse(x)).ToList();
                            AddPlayer(list);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                            ex.Message,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    return;
                }
            }
        }

        private void AddPlayer(List<int> list)
        {
            var newBox = Clone(groupBoxStrategy);
            setGroupBoxStrategies(newBox);
            int i = 0;
            foreach (var controls in newBox.Controls)
            {
                if (controls is NumericUpDown)
                {
                    ((NumericUpDown)controls).Value = list[i];
                    i++;
                }
            }
            groupBoxStrategies.Add(newBox);
            flowLayoutPanel1.Controls.Add(groupBoxStrategies.Last());
            groupBoxStrategies.Last().Visible = true;
        }
    }
}
