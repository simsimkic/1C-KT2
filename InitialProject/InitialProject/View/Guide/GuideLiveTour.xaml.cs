using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideLiveTour.xaml
    /// </summary>
    public partial class GuideLiveTour : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Tour CurrentSelectedTour = new Tour();

        private readonly TourRepository tourRepository;

        private readonly CheckPointRepository checkPointRepository;

        public GuideLiveTour()
        {
            InitializeComponent();
            Title = "Create new tour";
            DataContext = this;
            tourRepository = new TourRepository();
            checkPointRepository = new CheckPointRepository();
        }

        private void ShowTours(object sender, RoutedEventArgs e)
        {
            const string FilePath = "../../../Resources/Data/tours.csv";
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.GetTodaysTours(FilePath);
            DataPanel.ItemsSource = tours;

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            const string FilePath = "../../../Resources/Data/tours.csv";
            DateTime dateTimeNow = DateTime.Now;
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.ReadFromToursCsv(FilePath);
            DataPanel.ItemsSource = tours;
        }

        private void StartTour(object sender, RoutedEventArgs e)
        {
            bool IsTourStarted = tourRepository.IsStarted();

            if(IsTourStarted == true)
            {
                MessageBox.Show("Tour already started");
            }
            else
            {
                Tour selectedTour = new Tour();
                if (selectedTour == null)
                {
                    MessageBox.Show("Please select tour");
                }
                else
                {
                    selectedTour = DataPanel.SelectedItem as Tour;
                    selectedTour.TourStatus = "Zapoceta";
                    selectedTour = tourRepository.Update(selectedTour);
                    MessageBox.Show("Done");
                    CurrentSelectedTour = selectedTour;
                    List<CheckPoint> checkPoints = new List<CheckPoint>();
                    int NumOfPoint = 0;
                    foreach (CheckPoint point in selectedTour.CheckPoints)
                    {
                        if(NumOfPoint == 0)
                        {
                            point.Status = "Active";
                            //checkPointRepository.Update(point);
                        }
                        ListCheckPoints.Items.Add(point);
                        NumOfPoint++;
                    }

                }
            }
        }

        private void ActivateCheckPoint(object sender, RoutedEventArgs e)
        {
            CheckPoint selectedCheckPoint = new CheckPoint();
            if(selectedCheckPoint == null)
            {
                MessageBox.Show("Please select checkPoint");
            }
            else
            {
                selectedCheckPoint = ListCheckPoints.SelectedItem as CheckPoint;
                selectedCheckPoint.Status = "Active";
                //selectedCheckPoint = checkPointRepository.Update(selectedCheckPoint);
                MessageBox.Show("Done");
            }
            int NumOfActivatePoints = 0;
            foreach (CheckPoint point in ListCheckPoints.Items)
            {
                if (point.Status == "Active")
                    NumOfActivatePoints++;
            }

            
            if(NumOfActivatePoints == ListCheckPoints.Items.Count)
            {
                CurrentSelectedTour.TourStatus = "Finished";
                CurrentSelectedTour = tourRepository.Update(CurrentSelectedTour);
                CurrentSelectedTour = null;
                foreach (CheckPoint point in ListCheckPoints.Items)
                {
                    if (point.Status == "Active")
                    {
                        point.Status = "Neaktivna";
                        checkPointRepository.Update(point);
                    }
                }
                MessageBox.Show("Tour finished");
                ListCheckPoints.Items.Clear();
            }
        }

        private void FinishToruForced(object sender, RoutedEventArgs e)
        {
            CurrentSelectedTour.TourStatus = "Finished";
            CurrentSelectedTour = tourRepository.Update(CurrentSelectedTour);
            CurrentSelectedTour = null;
            foreach (CheckPoint point in ListCheckPoints.Items)
            {
                if (point.Status == "Active")
                {
                    point.Status = "Neaktivna";
                    checkPointRepository.Update(point);
                }
            }
            MessageBox.Show("Tour finished");
            ListCheckPoints.Items.Clear();
        }

        private void CancelTour(object sender, RoutedEventArgs e)
        {
            Tour selectedTour = new Tour();
            selectedTour = DataPanel.SelectedItem as Tour;
            CurrentSelectedTour = selectedTour;
            DateTime currentDate = DateTime.Now;

            if ((selectedTour.StartTime - currentDate).TotalHours < 48)
            {
                MessageBox.Show("You cannot cancel this tour");
            }
            else
            {
                if (CurrentSelectedTour.TourStatus == "Cancelled")
                {
                    MessageBox.Show("This tour is already cancelled");
                }
                else
                {
                    CurrentSelectedTour.TourStatus = "Cancelled";
                    CurrentSelectedTour = tourRepository.Update(CurrentSelectedTour);
                    MessageBox.Show("Tour cancelled");
                }
            }
        }

        /*private void LoadCheckPoints(object sender, RoutedEventArgs e)
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            string FilePath = "../../../Resources/Data/checkPoints.csv";
            checkPoints = checkPointRepository.ReadFromCheckPointsCsv(FilePath);

            for (int i = 0; i < checkPoints.Count; i++)
            {

                CheckPointsCB.Items.Add(checkPoints[i].Name);
            }

        }
        */
    }
}
