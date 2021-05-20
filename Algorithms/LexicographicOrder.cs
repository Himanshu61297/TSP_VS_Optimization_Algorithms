using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace optimization_algos
{
    static class LexicographicOrder
    {
        public static int[] Lexicography(int[] order)
        {
            int largestI = 0;
            int largestJ = 0;

            for (int i = 0; i < order.Length - 1; i++)
            {
                if (order[i] < order[i + 1])
                {
                    largestI = i;
                }
            }

            for (int j = 0; j < order.Length; j++)
            {
                if (order[largestI] < order[j])
                {
                    largestJ = j;
                }
            }

            var temp = order[largestI];
            order[largestI] = order[largestJ];
            order[largestJ] = temp;

            var t = order.Skip(largestI + 1).ToArray();
            t = t.Reverse().ToArray();
            t.CopyTo(order, largestI + 1);

            return order;
        }
    }
}
