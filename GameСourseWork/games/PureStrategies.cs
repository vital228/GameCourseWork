using GameСourseWork.algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.games
{
    public static class PureStrategies
    {
        private static List<IArtificialIntelligence> listStandartStrategy = new List<IArtificialIntelligence>
            {
                new FollowEnemyAlgorithm(),
                new FunctionAlgorithm(),
                new ToCentreAlgorithm(),
                new ToEdgeBoardAlgorithm(),
                new ToFarthestCellAlgorithm(),
                new PotentialAlgorithm()
            };
        public static IArtificialIntelligence Get(int i)
        { 
            if (i>=0 && i<listStandartStrategy.Count)
                return (IArtificialIntelligence)listStandartStrategy[i].Clone();
            else
                throw new ArgumentOutOfRangeException(); 
        }
        public static int Count { get { return listStandartStrategy.Count;} }

    }
}
