using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public static class RandomExtensions
    {
        public static int NextDouble(this Random RandGenerator, double MinValue, double MaxValue)
        {
            var t = RandGenerator.NextDouble();
            int q = Convert.ToInt32(t * (MaxValue - MinValue) + MinValue);
            return q;
        }
    }
}
