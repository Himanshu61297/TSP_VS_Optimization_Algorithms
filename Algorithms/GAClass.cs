using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace optimization_algos
{
    class GAClass
    {
        public int PopulationSize { get; private set; }
        List<Individual> newPopulation;

        public int Genration { get; private set; } = 0;

        public int Elitism { get; private set; }

        public List<Individual> Population;
        Random random;

        public float BestFitnes;
        public int[] BestGenes { get; private set; }

        List<float> cumuProportion;

        float mutationRate;


        public GAClass(int size, int totalCities, int elitism, Random random, float mutationRate)
        {
            cumuProportion = new List<float>();
            this.random = random;

            BestGenes = new int[totalCities];
            BestFitnes = float.PositiveInfinity;

            this.mutationRate = mutationRate;

            Elitism = elitism;

            PopulationSize = size;
            Population = new List<Individual>(PopulationSize);
            newPopulation = new List<Individual>(PopulationSize);

            for (int i = 0; i < PopulationSize; i++)
            {
                Population.Add(new Individual(totalCities, random));
            }
        }

        public void NewGenration()
        {
            SetFitness();            

            for (int i = 0; i < PopulationSize; i++)
            {
                if (i < Elitism)
                {
                    newPopulation.Add(Population[i]);
                }
                else
                {
                    var parent1 = ChooseParent();
                    var parent2 = ChooseParent();

                    var child = parent1.CorssOver(parent2);
                    Mutation(child);

                    newPopulation.Add(child);
                }
            }

            Population.Clear();
            Population.AddRange(newPopulation);

            newPopulation.Clear();

            Genration++;
        }     

        private Individual ChooseParent()
        {
            var randomNumber = (float)random.NextDouble();
            for (int i = 0; i < cumuProportion.Count; i++)
            {
                if (randomNumber < cumuProportion[i])
                {
                    return Population[i];
                }
            }

            return null;
        }

        void SetFitness()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Population[i].Fitness = (float)SharedMethods.RoundTripDistance(
                    SharedMethods.Locations, Population[i].Genes);

                if (Population[i].Fitness < BestFitnes)
                {
                    BestFitnes = Population[i].Fitness;
                    Population[i].Genes.CopyTo(BestGenes, 0);
                }
            }

            Population = Population.OrderBy(x => x.Fitness).ToList();

            var inverse = Population.Select(x => 1 / x.Fitness).ToList();
            var sum = inverse.Sum();
            var inverseProportion = inverse.Select(x => x / sum).ToList();
            var proportionSum = inverseProportion.Sum();
            var normalizedProportion = inverseProportion.Select(x => x / proportionSum).ToList();

            cumuProportion.Clear();
            var cumuTotal = 0f;

            foreach (var item in normalizedProportion)
            {
                cumuTotal += item;
                cumuProportion.Add(cumuTotal);
            }

            cumuProportion[PopulationSize - 1] = 1;
        }

        void Mutation(Individual child)
        {
            if (random.NextDouble() < mutationRate)
            {
                child.Mutation();
            }
        }
    }

    class Individual
    {
        public int[] Genes { get; private set; }
        public float Fitness { get; set; }
        public int Rank { get; set; }

        int size;

        Random random;

        public Individual(int size, Random random)
        {
            Genes = new int[size];
            this.random = random;

            this.size = size;

            var temp = SharedMethods.GetRandomList(random);
            temp.CopyTo(Genes, 0);
        }

        public Individual CorssOver(Individual otherParent)
        {
            List<int> childGenes = new List<int>();

            var x = random.Next(1, otherParent.Genes.Length - 1);
            var pGenes = Genes.Take(x).ToArray();

            childGenes.AddRange(pGenes);

            for (int i = 0; i < otherParent.Genes.Length; i++)
            {
                if (!childGenes.Contains(otherParent.Genes[i]))
                {
                    childGenes.Add(otherParent.Genes[i]);
                }
            }

            var child = new Individual(size, random);
            childGenes.CopyTo(child.Genes, 0);

            childGenes = null;

            return child;
        }

        public void Mutation()
        {
            var i = random.Next(1, Genes.Length - 1);
            var j = random.Next(1, Genes.Length - 1);

            while (i == j)
                j = random.Next(1, Genes.Length - 1);

            if (i < j)
                Genes = two_opt_swap(Genes, i, j);
            else
                Genes = two_opt_swap(Genes, j, i);
        }

        int[] two_opt_swap(int[] order, int i, int j)
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
    }
}
