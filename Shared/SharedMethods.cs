using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace optimization_algos
{
    static class SharedMethods
    {
        public static Vector2[] Locations { get; private set; }
        public static int[] Order { get; private set; }

        public static int TotalCities;

        public static double Temperature;
        public static double TemperatureRate;

        public static int PopulationSize;
        public static float MutationRate;
        public static int Elitism;

        public static bool isRunnnig = false;

        public static (Vector2[], int[]) InitializeLocations(int totalCities, Random random)
        {
            int startX = 200;
            int startY = 50;

            int endX = (int)Window.Current.Bounds.Width - 400;
            int endY = (int)Window.Current.Bounds.Height - startY;

            Locations = new Vector2[totalCities];
            Order = new int[totalCities];

            for (int i = 0; i < totalCities; i++)
            {
                var point = new Vector2(random.Next(startX, endX), random.Next(startY, endY));
                Locations[i] = point;
                Order[i] = i;
            }

            return (Locations, Order);
        }

        public static double Distance(Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow((point1.X - point2.X), 2)
                + Math.Pow((point1.Y - point2.Y), 2));
        }

        public static double RoundTripDistance(Vector2[] locations, int[] order)
        {
            double d = 0;
            for (int i = 0; i < order.Length - 1; i++)
            {
                d += Distance(locations[order[i]], locations[order[i + 1]]);
            }

            d += Distance(locations[order[0]], locations[order[order.Length - 1]]);

            return d;
        }

        public static double Factorial(int i)
        {
            if (i == 1)
                return 1;

            return i * Factorial(i - 1);
        }

        public static void DrawPath(CanvasDrawingSession args, Vector2[] vectors, int[] order, Color color, float stroke)
        {
            for (int i = 0; i < order.Length - 1; i++)
            {
                args.DrawLine(vectors[order[i]], vectors[order[i + 1]], color, stroke);
            }
            args.DrawLine(vectors[order[0]], vectors[order[order.Length - 1]], color, stroke);

        }

        public static void DrawPoints(Grid grid)
        {
            grid.Children.Clear();

            CanvasControl canvasControl = new CanvasControl();
            canvasControl.Draw += CanvasControl_Draw;

            grid.Children.Add(canvasControl);
        }

        private static void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            CanvasTextFormat z = new CanvasTextFormat();
            z.FontSize = 12;
            z.FontWeight = Windows.UI.Text.FontWeights.Normal;
            z.WordWrapping = CanvasWordWrapping.WholeWord;

            for (int i = 0; i < Locations.Length; i++)
            {               
                args.DrawingSession.DrawCircle(Locations[i], 10f, Color.FromArgb(100,255,255,255), 2f);
                args.DrawingSession.FillCircle(Locations[i], 5f, Color.FromArgb(100, 255, 255, 255));
            }
        }

        public static int[] GetRandomList(Random random)
        {
            return Shuffle(Order, random);
        }

        static int[] Shuffle(int[] order, Random random)
        {
            for (int i = 0; i < order.Length; i++)
            {
                var randomIndex = random.Next(order.Length);

                var temp = order[i];
                order[i] = order[randomIndex];
                order[randomIndex] = temp;
            }

            return order;
        }


    }
}
