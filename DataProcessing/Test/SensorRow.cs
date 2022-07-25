using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Test
{
    internal class SensorRow
    {
        public double colA { get; set; }
        public double colB { get; set; }

        public SensorRow(double a, double b)
        {
            this.colA = a;
            this.colB = b;
        }
    }
}
