using GameСourseWork.algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.games
{
    public class StandingMixedStrategies
    {
        public int AVG { get; private set; }
        public int MaxWin { get; private set; }
        public int MinWin { get; private set; }
        public int Dispersion { get; private set; }
        public MixedStrategy strategy { get; private set; }
        public StandingMixedStrategies(MixedStrategy strategy)
        {
            this.strategy = strategy;
            AVG = 0;
            MaxWin = 0;
            MinWin = 0;
            Dispersion = 0;
        }

        public void Update(List<int> countWin)
        {
            if (countWin.Count > 0)
            {
                int sum = 0;
                MaxWin = 0;
                foreach (int win in countWin)
                {
                    sum += win;
                    if (win > MaxWin)
                    {
                        MaxWin = win;
                    }
                }
                AVG = sum / countWin.Count;
                sum = 0;
                MinWin = MaxWin;
                foreach (int win in countWin)
                {
                    if (win < MinWin)
                    {
                        MinWin = win;
                    }
                    sum += (win - AVG) * (win - AVG);
                }
                Dispersion = sum / countWin.Count;
            }
            else
            {
                AVG = 0; MaxWin = 0; MinWin = 0; Dispersion = 0;
            }
        }
    }
    public class NashEquilibrium
    {
        List<MixedStrategy> mixedStrategies;
        public event EventHandler<NEventArgs> RoundChange;
        private int _countRound;
        

        public string Name
        {
            get;
            private set;
        }
        private int CountRound
        {
            get { return _countRound; }
            set
            {
                if (_countRound != value)
                {
                    _countRound = value;
                    if (RoundChange != null)
                    {
                        var nea = new NEventArgs();
                        nea.N = value;
                        RoundChange(this, nea);
                    }
                }
            }
        }

        private TableCup<Point> standing;

        public int CountWeights { get; private set; }
        public int CountGames { get; private set; }

        string path = "NashEquilibrium";
        string[] heading = { "Algorithm Name ", "AVG", "Dispersions", "MaxWin", "MinWin" }; 

        public NashEquilibrium(IArtificialIntelligence a1, IArtificialIntelligence a2, int countWeights = 100, int countGames = 100)
        {
            mixedStrategies = new List<MixedStrategy>();
            CountWeights = countWeights;
            Name = a1.Name + "_" + a2.Name +"_"+ countWeights;
            CountGames = countGames;
            string path = Path.Combine(Directory.GetCurrentDirectory(), Name);
            Directory.CreateDirectory(path);
            for (int i=0; i<= countWeights; i++)
            {
                mixedStrategies.Add(new MixedStrategy((IArtificialIntelligence)a1.Clone(), i, (IArtificialIntelligence)a2.Clone(), CountWeights - i));
            }
            standing = new TableCup<Point>(mixedStrategies.Count, mixedStrategies.Count);
        }

        Tournament cup;

        public void PlayTournament()
        {
            cup = new Tournament(Name);
            cup.RoundChange += Cup_RoundChange;
            foreach (MixedStrategy i in mixedStrategies)
            {
                cup.AddPlayer(i);
            }
            cup.PlayTournamentWithParallel(CountGames);
            for (int i = 0; i < mixedStrategies.Count; i++)
            {
                for (int j = 0; j < mixedStrategies.Count; j++)
                {
                    standing[i, j] = cup[i, j];
                }
            }
            if (cup.GetCountPlayer() < 20)
            {

                string text = cup.ToHtmlTable();
                string path = @"Nash\"+ Name + "_standings.html";
                // сохраняем текст в файл
                File.WriteAllText(path, text);
            }
        }
        private void Cup_RoundChange(object sender, NEventArgs e)
        {
            CountRound = e.N;
        }
        public List<StandingMixedStrategies> Find(bool isFirst, bool onlyWin)
        {
            List<StandingMixedStrategies> standingsMixedStrategies = new List<StandingMixedStrategies>();
            for (int i =0; i<mixedStrategies.Count; i++)
            {
                StandingMixedStrategies standingStrategies = new StandingMixedStrategies(mixedStrategies[i]);
                if (isFirst) {
                    List<int> w1 = new List<int>();
                    for (int j = 0; j < mixedStrategies.Count; j++)
                    {
                        w1.Add(standing[i,j].X);
                    }
                    standingStrategies.Update(w1);
                }
                else
                {
                    List<int> w2 = new List<int>();
                    for (int j = 0; j < mixedStrategies.Count; j++)
                    {
                        w2.Add(standing[j, i].Y);
                    }
                    standingStrategies.Update(w2);
                }
                if (onlyWin)
                {
                    if (standingStrategies.AVG >= CountGames/2)
                    {
                        standingsMixedStrategies.Add(standingStrategies);
                    }
                }
                else
                {
                    standingsMixedStrategies.Add(standingStrategies);
                }
            }

        
            return standingsMixedStrategies;
        }
        public void SaveStanding(string fileName, List<StandingMixedStrategies> standingsMixedStrategies)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), this.path);
            Directory.CreateDirectory(path);
            string file = path + @"\" + fileName;
            String separator = ",";
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(separator, heading));
            foreach (var strategy in standingsMixedStrategies)
            {
                String[] newLine = { strategy.strategy.Name, strategy.AVG.ToString(),
                 strategy.Dispersion.ToString(), strategy.MaxWin.ToString(), strategy.MinWin.ToString(),};
                output.AppendLine(string.Join(separator, newLine));
            }
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
            Console.WriteLine("The data has been successfully saved to the CSV file");
        }
    }
}
