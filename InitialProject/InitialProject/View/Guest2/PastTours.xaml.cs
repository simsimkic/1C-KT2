using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for PastTours.xaml
    /// </summary>
    public partial class PastTours : Window
    {
        private readonly TourRepository _repository;
        private const string FilePath = "../../../Resources/Data/tours.csv";
        User LogedUser = new Domain.Model.User();
        private Tour selectedTour;

        public PastTours(User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
            _repository = new TourRepository();
        }


        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Tour> tour = new List<Tour>();
            tour = _repository.ReadMyPastToursCsv(FilePath, LogedUser.Id);

            DataPanel.ItemsSource = tour;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MakeReview(object sender, RoutedEventArgs e)
        {
            if (DataPanel.SelectedItem != null)
            {
                selectedTour = (Tour)DataPanel.SelectedItem;
                TourReview createTourReview = new TourReview(LogedUser, selectedTour);
                createTourReview.Show();
            }
            else
            {
                MessageBox.Show("Please select a tour you want to review.");
            }
        }
    }
}
