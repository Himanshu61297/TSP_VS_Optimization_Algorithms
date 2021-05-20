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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace optimization_algos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int startX, startY, endX, endY;     

        public MainPage()
        {            
            this.InitializeComponent();

            startX = 50;
            startY = 50;

            endX = (int)Window.Current.Bounds.Width - startX;
            endY = (int)Window.Current.Bounds.Height - startY;

            CitySlider.Maximum = 1000;
            CitySlider.Minimum = 4;
            CitySlider.StepFrequency = 1;
            CitySlider.Value = 4;

            startingTempSlider.Maximum = (endX + endY) * 4;
            startingTempSlider.Minimum = (endX + endY);
            startingTempSlider.StepFrequency = 1;
            startingTempSlider.Value = startingTempSlider.Maximum;

            tempRateSlider.Maximum = 0.9999f;
            tempRateSlider.Minimum = 0.9000f;
            tempRateSlider.StepFrequency = 0.0010f;
            tempRateSlider.Value = 0.9900f;

            populationSlider.Maximum = 1000;
            populationSlider.Minimum = 50;
            populationSlider.StepFrequency = 10;
            populationSlider.Value = 50;

            mutationSlider.Maximum = 0.1;
            mutationSlider.Minimum = 0.01;
            mutationSlider.StepFrequency = 0.01;
            mutationSlider.Value = 0.05;

            elitismSlider.Maximum = 10;
            elitismSlider.Minimum = 0;
            elitismSlider.StepFrequency = 1;
            elitismSlider.Value = 0;         
        }      

        void InitializeLexicoPanel()
        {
            //Panels for genetic algorithm
            populationSizePanel.Visibility = Visibility.Collapsed;
            mutationRatePanel.Visibility = Visibility.Collapsed;
            elitismPanel.Visibility = Visibility.Collapsed;

            //Panels for simulated annealing
            tempPanel.Visibility = Visibility.Collapsed;
            tempReductionPanel.Visibility = Visibility.Collapsed;

            //Opacity
            lexBtn.Opacity = 0.85;
            twoOptBtn.Opacity = 0.5;
            saBtn.Opacity = 0.5;
            gaBtn.Opacity = 0.5;

            //infoPanels
            genInfoPanel.Visibility = Visibility.Collapsed;
            swapInfoPanel.Visibility = Visibility.Collapsed;
            tempInfoPanel.Visibility = Visibility.Collapsed;
            iterationInfoPanel.Visibility = Visibility.Visible;
            totalIterationInfoPanel.Visibility = Visibility.Visible;
        }

        void InitalizeTwoOptPanel()
        {
            //Panels for genetic algorithm
            populationSizePanel.Visibility = Visibility.Collapsed;
            mutationRatePanel.Visibility = Visibility.Collapsed;
            elitismPanel.Visibility = Visibility.Collapsed;

            //Panels for simulated annealing
            tempPanel.Visibility = Visibility.Collapsed;
            tempReductionPanel.Visibility = Visibility.Collapsed;

            //Opacity
            lexBtn.Opacity = 0.5;
            twoOptBtn.Opacity = 0.85;
            saBtn.Opacity = 0.5;
            gaBtn.Opacity = 0.5;

            //infoPanels
            genInfoPanel.Visibility = Visibility.Collapsed;
            swapInfoPanel.Visibility = Visibility.Visible;
            tempInfoPanel.Visibility = Visibility.Collapsed;
            iterationInfoPanel.Visibility = Visibility.Collapsed;
            totalIterationInfoPanel.Visibility = Visibility.Collapsed;
        }

        void InitializeSimulatedAnnealingPanel()
        {
            //Panels for genetic algorithm
            populationSizePanel.Visibility = Visibility.Collapsed;
            mutationRatePanel.Visibility = Visibility.Collapsed;
            elitismPanel.Visibility = Visibility.Collapsed;

            //Panels for simulated annealing
            tempPanel.Visibility = Visibility.Visible;
            tempReductionPanel.Visibility = Visibility.Visible;

            //Opacity
            lexBtn.Opacity = 0.5;
            twoOptBtn.Opacity = 0.5;
            saBtn.Opacity = 0.85;
            gaBtn.Opacity = 0.5;

            //infoPanels
            genInfoPanel.Visibility = Visibility.Collapsed;
            swapInfoPanel.Visibility = Visibility.Collapsed;
            tempInfoPanel.Visibility = Visibility.Visible;
            iterationInfoPanel.Visibility = Visibility.Collapsed;
            totalIterationInfoPanel.Visibility = Visibility.Collapsed;

        }

        void InitializeGeneticAlgoPanel()
        {
            //Panels for genetic algorithm
            populationSizePanel.Visibility = Visibility.Visible;
            mutationRatePanel.Visibility = Visibility.Visible;
            elitismPanel.Visibility = Visibility.Visible;

            //Panels for simulated annealing
            tempPanel.Visibility = Visibility.Collapsed;
            tempReductionPanel.Visibility = Visibility.Collapsed;

            //Opacity
            lexBtn.Opacity = 0.5;
            twoOptBtn.Opacity = 0.5;
            saBtn.Opacity = 0.5;
            gaBtn.Opacity = 0.85;

            //infoPanels
            genInfoPanel.Visibility = Visibility.Visible;
            swapInfoPanel.Visibility = Visibility.Collapsed;
            tempInfoPanel.Visibility = Visibility.Collapsed;
            iterationInfoPanel.Visibility = Visibility.Collapsed;
            totalIterationInfoPanel.Visibility = Visibility.Collapsed;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InitializeLexicoPanel();
            frame.Navigate(typeof(LexicoPage));
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            switch(btn.Tag)
            {
                case "0":
                    if(SharedMethods.isRunnnig==false)
                    {
                        InitializeLexicoPanel();
                        frame.Navigate(typeof(LexicoPage));
                    }
                   
                    break;

                case "1":
                    if (SharedMethods.isRunnnig == false)
                    {
                        InitalizeTwoOptPanel();
                        frame.Navigate(typeof(TwoOptPage));
                    }
                    break;

                case "2":
                    if (SharedMethods.isRunnnig == false)
                    {
                        InitializeSimulatedAnnealingPanel();
                        frame.Navigate(typeof(SAPage));
                    }             
                    break;

                case "3":
                    if (SharedMethods.isRunnnig == false)
                    {
                        InitializeGeneticAlgoPanel();
                        frame.Navigate(typeof(GAPage));
                    }
                    break;
            }

        }

        private void CitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SharedMethods.TotalCities = (int)e.NewValue;

            startingTempSlider.Maximum = (endX + endY) * SharedMethods.TotalCities;
        }

        private void startingTempSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SharedMethods.Temperature = e.NewValue;
        }

        private void tempRateSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SharedMethods.TemperatureRate = e.NewValue;
        }

        private void populationSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SharedMethods.PopulationSize = (int)e.NewValue;
        }

        private void mutationSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SharedMethods.MutationRate = (float)e.NewValue;
        }

        private void elitismSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SharedMethods.Elitism = (int)e.NewValue;
        }

    }
}
