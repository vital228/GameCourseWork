using GameСourseWork.algorithms;
using GameСourseWork.games;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameСourseWork.other
{
    public static class NeuralTournament 
    {
        class Players : IComparable<Players>
        {
            public int index;
            public int points;
            public Players(int index, int points)
            {
                this.index = index;
                this.points = points;
            }

            public int CompareTo(Players other)
            {
                return points - other.points;
            }
        }

        public static int PlayTournament(ref List<NNalgorithm> bots, int n)
        {
            List<Players> players = new List<Players>();
            int max = 0;
            List<IArtificialIntelligence> opponents = new List<IArtificialIntelligence>
            {
                new PotentialAlgorithm()//,
                //new FunctionAlgorithm(),
                //new ToFarthestCellAlgorithm(),
                //new ToCentreAlgorithm()
            };
            for (int i = 0; i<bots.Count; i++)
            {
                players.Add(new Players(i, 0));
            }
            Game bestGame = null;
            int maxGame = 0;
            for (int i = 0; i < bots.Count; i++)
            {
                int iBest = 0;
                int sum = 0;
                
                for (int j = 0; j < opponents.Count; j++)
                {
                    opponents[j].Reset();
                    Game game = new Game(bots[i], opponents[j], GeneratorBoard.Board.MultiLayered);
                    int winner = game.playGame();
                    int infocount = game.informationBoards.Count;
                    if (winner == 1)
                    {
                        players[i].points += 1;
                        bots[i].position += infocount/2 + game.onePlayer() + (opponents.Count - j) * 50;
                        Console.WriteLine("Победа!!!!");
                    }
                    else if (winner == 2)
                    {
                        bots[i].position += infocount/2 - 5;
                    }
                    else
                    {
                        Console.WriteLine("The match between " + bots[i].Name + " and " + bots[j].Name + " ended in a draw");
                    }
                    sum += game.informationBoards.Count;
                    if (maxGame < game.informationBoards.Count)
                    {
                        bestGame = game;
                        maxGame = game.informationBoards.Count;
                    }
                    if (iBest < game.informationBoards.Count)
                    {
                        iBest = game.informationBoards.Count;
                    }
                }
                bots[i].position += iBest/2;
                if (max*opponents.Count < sum)
                {
                    max = sum / opponents.Count;
                }
            }
            bestGame.saveGame(@"NeuralNetworkTrainer\neuron-neuron_" + n);
            players.Sort();
           /* for (int i=0; i < players.Count; i++)
            {
                bots[players[i].index].position += i + 1;
            }*/
            return max;
        }


    }
}
