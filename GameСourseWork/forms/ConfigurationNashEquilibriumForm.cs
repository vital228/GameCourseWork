using GameСourseWork.games;
using System;
using System.IO;
using System.Windows.Forms;

namespace GameСourseWork.forms
{
    public partial class ConfigurationNashEquilibriumForm : Form
    {
        public ConfigurationNashEquilibriumForm()
        {
            InitializeComponent();
            for (int i = 0; i< PureStrategies.Count; i++) {
                checkedListBox1.Items.Add(PureStrategies.Get(i));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedIndices.Count > 1)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Nash");
                Directory.CreateDirectory(path);
                NashEquilibrium nash = new NashEquilibrium(PureStrategies.Get(checkedListBox1.CheckedIndices[0]),
                    PureStrategies.Get(checkedListBox1.CheckedIndices[1]), (int)numericUpDownWeight.Value, (int)numericUpDownGame.Value);
                ProgressBarForm form = new ProgressBarForm();
                form.LoadData(nash);

                form.ShowDialog();
                nash.SaveStanding(nash.Name+"_All_1.csv", nash.Find(true, false));
                nash.SaveStanding(nash.Name + "_All_2.csv", nash.Find(false, false));
                nash.SaveStanding(nash.Name + "_Win_1.csv", nash.Find(true, true));
                nash.SaveStanding(nash.Name + "_Win_2.csv", nash.Find(false, true));
                
            }

        }
        
    }
}
