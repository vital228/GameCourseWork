using GameСourseWork.algorithms;
using GameСourseWork.games;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameСourseWork.forms
{
    public partial class ConfigurationGeneticAlgorithmForm : Form
    {
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
        private List<GroupBox> groupBoxStrategies;
        public ConfigurationGeneticAlgorithmForm()
        {
            groupBoxStrategies= new List<GroupBox>();
            InitializeComponent();
            
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
            for (int k = 0; k< PureStrategies.Count; k++)
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

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int n = (int)numericUpDownGeneration.Value;
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
            GeneticAlgorithm algorithm = new GeneticAlgorithm((int)numericUpDownPopulations.Value, (int)numericUpDownGeneration.Value);
            algorithm.InitializePopulation(strategies);
            ProgressBarForm form = new ProgressBarForm();
            form.LoadData(algorithm);
            form.ShowDialog();
        }
    }
}
