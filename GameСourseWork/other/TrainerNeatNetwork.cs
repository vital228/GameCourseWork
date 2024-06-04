using GameСourseWork.algorithms;
using Neat.Config;
using Neat.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.other
{
    internal class TrainerNeatNetwork
    {
        public TrainerNeatNetwork() { }

        public NeatBox best;
        public int maxBox = 0;


        public void Training(int k)
        {
            NeatConfig config= new NeatConfig();
            int noOfIntputNodes = 85;
            int noOfOutputNodes = 4;
            // Initial population (number of neural nets)
            int initPopulation = 200;


            // set config params accordingly
            config.minRequiredScoreToMaxScore = 0.3;
            config.populationSruvivalPercentagePerSpecies = 40;
            config.weightDeltaOnMutation = 0.3;


            // instantiate the NeatMain class before you begin

            NeatMain.initInstance(config, noOfIntputNodes, noOfOutputNodes, initPopulation);
            if (File.Exists("NeatMain"))
            {
                NeatMain.setInstance(NeatMain.Deserialize("NEATMAIN"));
            }
            //NeatMainSerializer.DeserializeFromJsonFile("neatMain.json");
            NeatBox box;
            List<String> bestBoxLog = new List<string>();
            List<IArtificialIntelligence> opponents = new List<IArtificialIntelligence>
            {
                new FunctionAlgorithm(),
                new PotentialAlgorithm(),
                new ToFarthestCellAlgorithm(),
                new ToCentreAlgorithm()
            };
            int mx = 0;
            int isgeneration = 0;
            for (int i = 1; i < 16; i++)
            {
                List<IArtificialIntelligence> tOpponents = new List<IArtificialIntelligence>();
                for (int t = 1, a = 0; t<16; t *= 2, a++)
                {
                    if ((i&t) > 0)
                    {
                        tOpponents.Add(opponents[a]);
                    }
                }
                Console.WriteLine("Новый I=" + i);
                int p = 0;
                mx = 0;
                while (mx < k * tOpponents.Count)
                {
                    box = NeatMain.getInstance().getNextNeatBox();
                    int t = trainingToWin(box, tOpponents);
                    if (mx < t)
                    {
                        mx = t;
                        bestBoxLog = box.getGenomeLog();
                    }
                    if (mx > maxBox)
                    {
                        maxBox = mx;
                        box.Serialize("NEATBOX");
                    }
                    if (isgeneration < box.generation)
                    {
                        isgeneration = box.generation;
                        Console.WriteLine("n=" + isgeneration + " max=" + mx);
                        Console.WriteLine(MultiString(bestBoxLog));
                        mx = 0;
                    }
                }
            }
            mx = 0;
            while (mx < 2000)
            {
                box = NeatMain.getInstance().getNextNeatBox();
                int t = trainingToDominate(box);
                if (mx < t)
                {
                    mx = t;
                    bestBoxLog = box.getGenomeLog();
                }

                if (mx > maxBox)
                {
                    maxBox = mx;
                    box.Serialize("NEATBOX");
                }
                if (isgeneration < box.generation)
                {
                    isgeneration = box.generation;
                    Console.WriteLine("n=" + isgeneration + " max=" + mx);
                    Console.WriteLine(MultiString(bestBoxLog));
                    mx = 0;
                }
            }
        }

        int trainingToDominate(NeatBox box)
        {
            int k = 0;
            double mx = 0;
            List<IArtificialIntelligence> opponents = new List<IArtificialIntelligence>
            {
                new PotentialAlgorithm(),
                new FunctionAlgorithm(),
                new ToFarthestCellAlgorithm(),
                new ToCentreAlgorithm()
            };
            NeatAlgorithm neatAlgorithm = new NeatAlgorithm(box);
            int win = 0;
            for (int i = 0; i< opponents.Count; i++)
            {
                Game game = new Game(neatAlgorithm, opponents[i], games.GeneratorBoard.Board.SingleLayered);
                win += game.playGame();
                mx += (double)(game.informationBoards.Count) / opponents.Count;
            }
            while (win <= opponents.Count && mx < 2000)
            {
                k++;
                win = 0;
                for (int i = 0; i < opponents.Count; i++)
                {
                    opponents[i].Reset();
                    neatAlgorithm.Reset();
                    Game game = new Game(neatAlgorithm, opponents[i], games.GeneratorBoard.Board.SingleLayered);
                    win += game.playGame();
                    mx += (double)(game.informationBoards.Count) / opponents.Count;
                }
            }
            box.setFitnessScore(k + opponents.Count*2 - win + mx);
            return (int)(k + opponents.Count*2 - win + mx);
        }

        int trainingToWin(NeatBox box, List<IArtificialIntelligence> opponents)
        {
            int fit = 0;
            foreach (var ai in opponents)
            {
                NeatAlgorithm neatAlgorithm = new NeatAlgorithm(box);
                ai.Reset();
                Game game = new Game(neatAlgorithm, ai, games.GeneratorBoard.Board.SingleLayered);
                if (game.playGame() == 1)
                {
                    neatAlgorithm.fit += 100;
                    game.onePlayer();
                }
                fit += neatAlgorithm.fit;
            }
            box.setFitnessScore(fit);
            return fit;
        }
        String MultiString(List<String> list)
        {
            string s="";
            for (int i = 0; i<list.Count; i++)
            {
                s += list[i] + '\n';
            }
            return s;
        }

        

    }
}
