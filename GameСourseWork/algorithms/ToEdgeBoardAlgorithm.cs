using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    public class ToEdgeBoardAlgorithm : AbstractToCellAlgorithm
    {
        public override string Name { get => "Walk along the edge"; set { } }

        public ToEdgeBoardAlgorithm() : base() { }

        protected override int FunctionSort(Cell c1, Cell c2)
        {
            int distToCentre = Math.Abs(c1.X - 4) + Math.Abs(c1.Y - 4) - (Math.Abs(c2.X - 4) + Math.Abs(c2.Y - 4));
            if (distToCentre != 0)
                return -distToCentre;
            else
                return c2.distance - c1.distance;
        }
    }
}
