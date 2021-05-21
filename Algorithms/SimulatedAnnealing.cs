using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace optimization_algos
{
    static class SimulatedAnnealing
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

        static int[] AnnealingSwap(int[] x, Random random)
        {
            int[] newX = new int[x.Length];
            x.CopyTo(newX, 0);

            var i = random.Next(1, x.Length - 1);
            var j = random.Next(1, x.Length - 1);
            while (i == j)
                j = random.Next(1, x.Length - 1);

            if (i < j)
            {
                newX = two_opt_swap(newX, i, j);
            }
            else
            {
                //abcd 
                while (i != j)
                {
                    var temp = newX[i];
                    newX[i] = newX[j];
                    newX[j] = temp;

                    if (i == newX.Length - 1)
                    {
                        i = 0;
                    }
                    else
                        i++;

                    if (i == j)
                    {
                        break;
                    }


                    if (j == 0)
                        j = newX.Length - 1;
                    else
                        j--;

                    if (i == j)
                    {
                        break;
                    }
                }
            }

            return newX;
        }

        public static int[] PerformSimulatedAnnealing(int[] x, double Temp, Random random)
        {
            var newOrder = AnnealingSwap(x, random);

            var d1 = SharedMethods.RoundTripDistance(SharedMethods.Locations, x);
            var d2 = SharedMethods.RoundTripDistance(SharedMethods.Locations, newOrder);

            var prob = probability(d1, d2, Temp);
            var rand = random.NextDouble();
            if (rand < prob)
            {
                x = newOrder;
            }

            //Temp = Temp * 0.999f;

            return x;
        }

        static double probability(double c, double n, double T)
        {
            if (n < c)
            {
                return 1;
            }
            else
            {
                return Math.Exp(-(n - c) / T);
            }
        }
    }
}
