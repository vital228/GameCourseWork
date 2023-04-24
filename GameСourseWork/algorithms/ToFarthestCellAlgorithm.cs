using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork.algorithms
{
    public class ToFarthestCellAlgorithm : AbstractToCellAlgorithm
    {
        public override string Name { get => "go to farthest cell"; set { } }

        public ToFarthestCellAlgorithm() : base() { }

        protected override int FunctionSort(Cell c1, Cell c2)
        {
            return c2.distance - c1.distance;
        }
    }
}
