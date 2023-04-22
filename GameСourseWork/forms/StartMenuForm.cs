using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GameСourseWork
{
    public partial class StartMenuForm : Form
    {
        public StartMenuForm()
        {
            InitializeComponent();
        }

        private string getNameFile(string path)
        {
            string[] names = path.Split('\\');
            return names[names.Length - 1];
        }

        private void buttonLookGame_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                string fileName = getNameFile(file);
                string name1 = fileName.Split('-')[0];
                string name2 = fileName.Split('-')[1].Split('_')[0];
                Game game = new Game();
                string ex = game.loadGame(file);
                
                if (ex != null)
                {
                    MessageBox.Show(
                        ex,
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                GameForm gameForm = new GameForm(game, name1, name2);
                gameForm.ShowDialog();
            }
            Console.WriteLine(result);
        }

        private void buttonNewCup_Click(object sender, EventArgs e)
        {
            ConfigurationCupForm form = new ConfigurationCupForm();
            form.ShowDialog();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            ConfigurationPlayGameForm form = new ConfigurationPlayGameForm();
            form.ShowDialog();
        }
    }
}
