using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace optimization_algos
{
    class TwoOpt
    {
        static int[] two_opt_swap(int[] order, int i, int j)
        {
            int[] newOrder = new int[order.Length];

            var head = order.Take(i).ToArray();
            var mid = order.Skip(i).ToArray();
            mid = mid.Take(j - i + 1).ToArray();
            mid = mid.Reverse().ToArray();
            var tail = order.Skip(j + 1);

            newOrder = head.Concat(mid).ToArray().Concat(tail).ToArray();           

            return newOrder;
        }

        public static (int[], int) two_opt(int[] order, int count)
        {
            var bestdistance = SharedMethods.RoundTripDistance(SharedMethods.Locations, order);
            int flag = 0;
            for (int i = 0; i < order.Length - 1; i++)
            {
                for (int j = i + 1; j < order.Length; j++)
                {                   
                    var newRout = two_opt_swap(order, i, j);
                    var newdis = SharedMethods.RoundTripDistance(SharedMethods.Locations, newRout);
                    if (bestdistance > newdis)
                    {
                        bestdistance = newdis;
                        order = newRout;
                        flag = 1;
                        count++;
                        break;
                    }
                }
                if (flag == 1)
                {
                    break;
                }
            }

            return (order, count);
        }
    }
}
