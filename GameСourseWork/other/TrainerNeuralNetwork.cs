using GameСourseWork.algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neat.Framework;
using Neat.Config;



namespace GameСourseWork.other
{
    public class TrainerNeuralNetwork
    {
        List<NNalgorithm> bots;
        public List<NeuralNetwork> networks;
        public int populationSize = 100;//creates population size
        int[] layers = new int[5] { 85, 85, 64, 32, 4 };

        public float MutationChance = 0.01f;

        public float MutationStrength = 0.3f;

        public TrainerNeuralNetwork() { }

        void Start()// Start is called before the first frame update
        {
            if (populationSize % 2 != 0)
                populationSize = 100;//if population size is not even, sets it to fifty
            InitNetworks();
        }

        public void InitNetworks()
        {
            networks = new List<NeuralNetwork>();
            for (int i = 0; i < populationSize; i++)
            {
                NeuralNetwork net = new NeuralNetwork(layers);
                //net.Load("Save.txt");//on start load the network save
                networks.Add(net);
            }
        }

        public void CreateBots()
        {
            if (bots != null)
            {
                SortNetworks();//this sorts networks and mutates them
            }
            bots = new List<NNalgorithm>();
            for (int i = 0; i < populationSize; i++)
            {
                NNalgorithm bot = new NNalgorithm();//create botes
                bot.network = networks[i];//deploys network to each learner
                bots.Add(bot);
            }
        }


        public void LoadNetworks(string[] path)
        {
            int n = bots.Count;
            for (int i = 0; i < path.Length; i++)
            {
                bots.Add(new NNalgorithm());
                bots[n+i].network.Load(path[i]);
                string name = Path.GetFileName(path[i]);
                bots[n+i].Name = name;
            }
        }

        public void SaveNetwork()
        {
            for (int i = 0; i<= bots.Count; i++)
            {
                bots[i].network.Save($"\\networks\\" + bots[i].Name);
            }
        }


        public void Cup(int i)
        {
            Console.WriteLine("max="+ NeuralTournament.PlayTournament(ref bots, i));
        }

        public void Trainer(int n)
        {
            Start();
            for (int i = 0; i < n; i++)
            {
                Console.Write("n="+i+", ");
                CreateBots();
                Cup(i);
            }
        }

        public void SortNetworks()
        {
            for (int i = 0; i < populationSize; i++)
            {
                bots[i].UpdateFitness();//gets bots to set their corrosponding networks fitness
            }
            networks.Sort();
            networks[populationSize - 1].Save("Save.txt");//saves networks weights and biases to file, to preserve network performance
            for (int i = 0; i < populationSize; i++)
            {
                MutateLastPopulation(i);
            }
        }
        public void MutateLastPopulation(int i)
        {
            int top = 10;
            if (populationSize - top<= i)
            {

            }
            else if (populationSize * 0.6f <= i)
            {
                NeuralNetwork copy = networks[i].copy(new NeuralNetwork(layers));
                copy.Mutate((int)(1 / (1f * MutationChance)), MutationStrength);
                networks[i] = copy;
            }
            else if (populationSize * 0.4f <= i)
            {
                NeuralNetwork copy = networks[i].copy(new NeuralNetwork(layers));
                copy.Mutate((int)(1 / (2f * MutationChance)), MutationStrength);
                networks[i] = copy;
            }
            else
            {
                NeuralNetwork copy = networks[populationSize - i - 1].copy(new NeuralNetwork(layers));
                copy.Mutate((int)(1 / (1f * MutationChance)), MutationStrength);
                networks[i] = copy;
            }
        } 
    }
}
