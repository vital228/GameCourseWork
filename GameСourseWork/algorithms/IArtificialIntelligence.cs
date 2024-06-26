﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameСourseWork
{
    public interface IArtificialIntelligence : ICloneable
    {
        string Name { get; set; }
        char step(int[,] board, Point player, Point opponent);
        void Reset();
        void ReportGameEnd(bool win);
    }
}
