using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    public class WeightStrategy : ICloneable
    {
        public WeightStrategy(IArtificialIntelligence strategy, int weight)
        {
            Strategy = (IArtificialIntelligence)strategy.Clone();
            Weight = weight;
        }
        public IArtificialIntelligence Strategy { get; private set; }
        public int Weight { get; set; }

        public object Clone()
        {
            return new WeightStrategy((IArtificialIntelligence)Strategy.Clone(), Weight);
        }
    }


    public class MixedStrategy : IArtificialIntelligence
    {

        List<WeightStrategy> strategies;
        int sumWeight = 0;

        public MixedStrategy(IArtificialIntelligence str1, int weigth1, IArtificialIntelligence str2, int weight2) : this()
        {
            strategies.Add(new WeightStrategy((IArtificialIntelligence)str1.Clone(), weigth1));
            strategies.Add(new WeightStrategy((IArtificialIntelligence)str2.Clone(), weight2));
            sumWeight = weigth1 + weight2;
        }
        private MixedStrategy()
        {
            strategies = new List<WeightStrategy>();
        }


        public MixedStrategy(params WeightStrategy[] weightStrategies) : this()
        {
            foreach (var strategy in weightStrategies)
            {
                strategies.Add(strategy);
                sumWeight += strategy.Weight;
            }
        }
        public MixedStrategy(List<WeightStrategy> weightStrategies) : this()
        {
            foreach (var strategy in weightStrategies)
            {
                strategies.Add(strategy);
                sumWeight += strategy.Weight;
            }
        }

        public int this[int i]
        {
            get { return strategies[i].Weight; }
            set { strategies[i].Weight = value; }
        }



        
        public string Name { 
            get {
                string name = "";
                foreach (WeightStrategy strategy in strategies)
                {
                    name += strategy.Strategy.Name[0] +"W"+strategy.Weight.ToString()+"_";
                }
                return name;
            }
            set {}
        }


        

        public void ReportGameEnd(bool win)
        {
            return;
        }

        public void Reset()
        {
            foreach (WeightStrategy strategy in strategies)
            {
                strategy.Strategy.Reset();
            }
        }

        public char step(int[,] board, Point player, Point opponent)
        {
            Random r = new Random();
            int rInt = r.Next(1, sumWeight+1);
            int lim = 0;
            foreach (WeightStrategy strategy in strategies)
            {
                lim += strategy.Weight;
                if (rInt <= lim)
                {
                    if (strategy.Strategy.Name != "Potential")
                    {
                        foreach (WeightStrategy str in strategies)
                        {
                            if (str.Strategy.Name =="Potential")
                            {
                                ((PotentialAlgorithm)str.Strategy).update(player, opponent);
                            }
                        }
                    }
                    return strategy.Strategy.step(board, player, opponent);
                }
            }
            return strategies[0].Strategy.step(board, player, opponent);
        }

        public object Clone()
        {
            WeightStrategy[] weightStrategies = new WeightStrategy[strategies.Count];
            for(int i = 0; i< strategies.Count; i++)
            {
                weightStrategies[i] = (WeightStrategy) strategies[i].Clone();
            }
            return new MixedStrategy(weightStrategies);
        }
    }
}
