using GameСourseWork.algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameСourseWork.games
{
    public class StandingGeneticAlgorithm
    {
        public MixedStrategy Strategy { get; private set; }
        public int FirstGen { get; private set; }
        public int LastGen { get; private set; }
        public int countWin { get; set; }
        public int CountTop3 { get; private set; }
        public int CountTop10 { get; private set; }
        public int CountTop1 { get; private set; }
        public int CountTop20 { get; private set; }
        public StandingGeneticAlgorithm(MixedStrategy strategy, int generation)
        {
            Strategy = (MixedStrategy)strategy.Clone();
            FirstGen= generation;
            LastGen= generation;
            countWin = 0;
        }

        public void NextGen(int place)
        {
            LastGen++;
            countWin = 0;
            if (place <= 20)
            {
                CountTop20++;
            }
            if (place <= 10)
            {
                CountTop10++;
            }
            if (place <= 3)
            {
                CountTop3++;
            }
            if (place <= 1)
            {
                CountTop1++;
            }
        }
    }

    public class Strategy
    {
        public MixedStrategy Genes { get; private set; }
        public int Fitness { get; set; }
        public int idInStanding { get; set; }

        public Strategy(MixedStrategy strategy)
        {
            Genes = (MixedStrategy)strategy.Clone();
            Fitness = 0;
            idInStanding = -1;
        }
    }


    public class GeneticAlgorithm
    {
        public List<StandingGeneticAlgorithm> standing = new List<StandingGeneticAlgorithm>();
        private int _countRound;

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


        String path = "GeneticAlgorithm";
        String[] heading = { "Algorithm Name", "First_Gen", "Last_Gen"
                , "Count_TOP_1", "Count_TOP_3", "Count_TOP_10", "Count_TOP_20"
                , "Fitness"}; 

        public event EventHandler<NEventArgs> RoundChange;

        private List<Strategy> population;
        private int populationSize;
        private int generations;
        private Random random;

        private int countGame = 7;

        public int Generations { get { return generations; } }

        public GeneticAlgorithm(int populationSize, int generations, int countGame = 7)
        {
            this.populationSize = populationSize;
            this.generations = generations;
            this.countGame = countGame;
            this.random = new Random();
            this.population = new List<Strategy>();
        }

        public void InitializePopulation(int start = 0)
        {
            for (int i = start; i < populationSize; i++)
            {
                population.Add(createRandomStrategy(0));
            }
        }
        public void InitializePopulation(List<MixedStrategy> strategies)
        {
            for (int i = 0; i< strategies.Count; i++)
            {
                Strategy strategy = new Strategy(strategies[i]);
                int count = standing.Count;
                standing.Add(new StandingGeneticAlgorithm(strategies[i], 0));
                strategy.idInStanding = count;
                population.Add(strategy);
            }
            InitializePopulation(strategies.Count);
        }

        private Strategy createRandomStrategy(int generationNow)
        {
            List<WeightStrategy> listGenes = new List<WeightStrategy>();
            for (int j = 0; j < PureStrategies.Count; j++)
            {
                listGenes.Add(new WeightStrategy(PureStrategies.Get(j), random.Next(0, 101)));
            }
            int count = standing.Count;
            Strategy strategy = new Strategy(new MixedStrategy(listGenes));
            standing.Add(new StandingGeneticAlgorithm(strategy.Genes, generationNow));
            strategy.idInStanding = count;
            return strategy;
        }


        public void Evolve()
        {
            for (int i = 0; i < generations; i++)
            {
                CountRound = i;
                Console.WriteLine(i);
                UpdateFitness();
                
                List<Strategy> newPopulation = new List<Strategy>();
                newPopulation.AddRange(population.OrderByDescending(s => s.Fitness).Take(populationSize/3).ToList());
                for (int j = 0; j<newPopulation.Count;j++)
                {
                    standing[newPopulation[j].idInStanding].NextGen(j+1);
                }
                for (int j=0; j < populationSize / 10; j++)
                {
                    newPopulation.Add(createRandomStrategy(i+1));
                }
                while (newPopulation.Count < populationSize)
                {
                    Strategy parent1 = TournamentSelection();
                    Strategy parent2 = TournamentSelection();
                    Strategy child = Crossover(parent1, parent2);
                    Mutate(child);
                    int count = standing.Count;
                    standing.Add(new StandingGeneticAlgorithm(child.Genes, i+1));
                    child.idInStanding = count;
                    newPopulation.Add(child);
                }
                population = newPopulation;
               
            }
            SavePopulationCSV();
            SaveAllCSV();
            CountRound = generations;
        }

        private void SavePopulationCSV()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), this.path);
            Directory.CreateDirectory(path);
            String file = path + @"\LastPopulation.csv";
            String separator = ",";
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(separator, heading));
            foreach (Strategy strategy in population)
            {
                int id = strategy.idInStanding;
                String[] newLine = { strategy.Genes.Name, standing[id].FirstGen.ToString(), standing[id].LastGen.ToString(),
                    standing[id].CountTop1.ToString(), standing[id].CountTop3.ToString(), standing[id].CountTop10.ToString(), standing[id].CountTop20.ToString(),
                    strategy.Fitness.ToString()};
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
        private void SaveAllCSV()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), this.path);
            Directory.CreateDirectory(path);
            String file = path + @"\All.csv";
            String separator = ",";
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(separator, heading));
            for (int id =0; id<standing.Count; id++)
            {
                String[] newLine = { standing[id].Strategy.Name, standing[id].FirstGen.ToString(), standing[id].LastGen.ToString(),
                    standing[id].CountTop1.ToString(), standing[id].CountTop3.ToString(), standing[id].CountTop10.ToString(), standing[id].CountTop20.ToString(),
                    "None"};
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


        private void UpdateFitness()
        {
            foreach (var p in population) p.Fitness = 0;
            string folderName = "GeneticAlgo_generations_" + CountRound;
            Tournament cup = new Tournament(folderName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Directory.CreateDirectory(path);
            foreach (var strategy in population)
            {
                cup.AddPlayer(strategy.Genes);
            }
            cup.PlayTournamentWithParallel(countGame);

            for (int i = 0; i < population.Count; i++)
            {
                for (int j =0; j < population.Count; j++)
                {
                    if (cup[i, j].X > cup[i, j].Y)
                    {
                        population[i].Fitness += countGame;
                    }
                    else
                    {
                        population[i].Fitness += cup[i,j].X;
                    }
                    if (cup[j, i].Y > cup[j, i].X)
                    {
                        population[i].Fitness += countGame;
                    }
                    else
                    {
                        population[i].Fitness += cup[j, i].Y;
                    }
                }
            }
        }

        private Strategy TournamentSelection()
        {
            int tournamentSize = 3;
            List<Strategy> tournamentPopulation = new List<Strategy>();
            for (int i = 0; i < tournamentSize; i++)
            {
                tournamentPopulation.Add(population[random.Next(populationSize)]);
            }
            return tournamentPopulation.OrderByDescending(s => s.Fitness).First();
        }

        private Strategy Crossover(Strategy parent1, Strategy parent2)
        {
            List<WeightStrategy> childGenes = new List<WeightStrategy>();
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    childGenes.Add(new WeightStrategy(PureStrategies.Get(i), parent1.Genes[i]));
                }
                else
                {
                    childGenes.Add(new WeightStrategy(PureStrategies.Get(i), parent2.Genes[i]));
                }
            }
            return new Strategy(new MixedStrategy(childGenes));
        }

        private void Mutate(Strategy individual)
        {
            double mutationRate = 0.01;
            for (int i = 0; i < PureStrategies.Count; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    individual.Genes[i] = random.Next(0, 101);
                }
            }
        }


    }
}
