using GameСourseWork.games;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameСourseWork.forms
{
    public partial class ProgressBarForm : Form
    {
        Thread thread;
        public ProgressBarForm()
        {
            InitializeComponent();
            _closing= false;
            CloseHandler += ProgressBarForm_CloseHandler;
            
        }

        private void ProgressBarForm_CloseHandler(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadData(Tournament cup, int n)
        {
            progressBar.Maximum = n * cup.GetCountPlayer() * cup.GetCountPlayer();
            progressBar.Value = 0;
            new Thread(() => {
                cup.RoundChange += Cup_RoundChange;
                cup.PlayTournamentWithParallel(n);
                }
            ).Start();
        }

        public void LoadData(NashEquilibrium cup)
        {
            progressBar.Maximum = cup.CountGames * (cup.CountWeights +1) * (cup.CountWeights +1);
            progressBar.Value = 0;
            new Thread(() =>
            {
                cup.RoundChange += Cup_RoundChange;
                cup.PlayTournament();
            }).Start();
        }

        public void LoadData(GeneticAlgorithm cup)
        {
            progressBar.Maximum = cup.Generations;
            progressBar.Value = 0;
            new Thread(() =>
            {
                cup.RoundChange += Cup_RoundChange;
                cup.Evolve();
            }).Start();

        }

        private bool _closing;
        private bool Closing
        {
            get { return _closing; }
            set
            {
                if (_closing != value)
                {
                    _closing = value;
                    if (_closing != false)
                    {
                        CloseHandler(this, null);
                    }
                }
            }
        }
        public event EventHandler<EventArgs> CloseHandler;


        private void Cup_RoundChange(object sender, NEventArgs e)
        {
            progressBar.BeginInvoke(
                new Action(() =>
                {
                    progressBar.Value = e.N;
                    if (progressBar.Value == progressBar.Maximum)
                    {
                        Closing = true;
                    }
                }));
            //progressBar.
            //Console.WriteLine(progressBar.Value);
            //if (progressBar.Value == progressBar.Maximum)
            //{
            //    Console.WriteLine(progressBar.Value);
            //    this.Close();
            //}
        }


    }
}
