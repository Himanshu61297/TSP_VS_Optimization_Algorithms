using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace optimization_algos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SAPage : Page
    {
        public SAPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            bestTourDistance = float.PositiveInfinity;
            random = new Random();

        }

        int totalCities;

        int[] tourOrder;
        Vector2[] tourLocations;

        double Temperature = 0;
        double TemperatureRate = 0.999;
        double initialDistance;
      
        double bestTourDistance;

        

        Random random;
        int count = 0;

        void InitializeValues()
        {
            totalCities = SharedMethods.TotalCities;

            tourOrder = new int[totalCities];
            tourLocations = new Vector2[totalCities];
            
            (tourLocations, tourOrder) = SharedMethods.InitializeLocations(totalCities, random);

            SharedMethods.Order.CopyTo(tourOrder, 0);
            SharedMethods.Locations.CopyTo(tourLocations, 0);

            SharedMethods.DrawPoints(pointsGrid);

            initialDistance = SharedMethods.RoundTripDistance(tourLocations, tourOrder);

            Temperature = SharedMethods.Temperature;
            TemperatureRate = SharedMethods.TemperatureRate;

            count = 0;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            startBtn.Visibility = Visibility.Collapsed;
            stopBtn.Visibility = Visibility.Visible;

            InitializeValues();
            InfoPanel.Visibility = Visibility.Visible;

            SharedMethods.isRunnnig = true;
            canvasAnimatedControl.Paused = false;
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            SharedMethods.isRunnnig = false;
        }

        async void UpdateUI()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                text1.Text = Temperature.ToString();
                text2.Text = Math.Round(initialDistance, 6).ToString();
                text3.Text = Math.Round(bestTourDistance, 6).ToString();
            });
        }

        private async void CanvasAnimatedControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            if (SharedMethods.isRunnnig)
            {
                var curDistance = SharedMethods.RoundTripDistance(tourLocations, tourOrder);
               
                bestTourDistance = curDistance;
                
                tourOrder = SimulatedAnnealing.PerformSimulatedAnnealing(tourOrder, Temperature, random);

                Temperature = Temperature * TemperatureRate;
                UpdateUI();

                count++;

                SharedMethods.DrawPath(args.DrawingSession, tourLocations, tourOrder, Color.FromArgb(85, 255, 255, 255), 2f);

            }
            else
            {
                if (count > 0)
                {
                    SharedMethods.DrawPath(args.DrawingSession, tourLocations, tourOrder, Color.FromArgb(85, 255, 255, 255), 4f);
                }
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    startBtn.Visibility = Visibility.Visible;
                    stopBtn.Visibility = Visibility.Collapsed;
                });

                sender.Paused = true;
            }


        }
    }
}
