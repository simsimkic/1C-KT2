using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for OneTour.xaml
    /// </summary>
    public partial class OneTour : Window
    {
        User LogedUser = new Domain.Model.User();
        const string FilePath = "../../../Resources/Data/tours.csv";
        const string FilePath1 = "../../../Resources/Data/vouchers.csv";

        public event PropertyChangedEventHandler PropertyChanged;

        public Tour selectedTour;

        private readonly TourRepository _repository;
        private readonly VoucherRepository _vouchersRepository;

        public OneTour(User logedUser, Tour tour)
        {
            InitializeComponent();
            LogedUser = logedUser;
            selectedTour = tour;
            _repository = new TourRepository();
            _vouchersRepository = new VoucherRepository();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPeopleOnSelectedTour(object sender, RoutedEventArgs e)
        {

            const string FilePath1 = "../../../Resources/Data/guestOnTour.csv";
            int numGuests = Convert.ToInt32(txtNumOfGuests.Text);

            if (numGuests <= 0)
            {
                MessageBox.Show("Please select how many guests want to go on the tour.");
            }
            else
            {
                if (selectedTour.MaxNumberOfGuests < selectedTour.CurentNumberOfGuests + numGuests)
                {
                    MessageBox.Show("Selected tour doesn't have that many free places." +
                        "Here are some similar tours for that many people.");
                }
                else
                {

                    if (_repository.ReserveTour(selectedTour.Id, LogedUser, FilePath1, numGuests))
                    {
                        selectedTour.CurentNumberOfGuests = selectedTour.CurentNumberOfGuests + numGuests;
                        _repository.Update(selectedTour);
                        if (Vouchers.SelectedItem != null)
                        {
                            MessageBox.Show("Reserved with voucher.");

                        }
                        else
                        {
                            MessageBox.Show("Reserved.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not reserved.");
                    }

                }
            }
        }

        public void LoadData(object sender, RoutedEventArgs e)
        {
            txtName.Text = selectedTour.Name.ToString();
            txtCity.Text = selectedTour.City.ToString();
            txtCountry.Text = selectedTour.Country.ToString();
            txtDescription.Text = selectedTour.Description.ToString();
            txtLenguage.Text = selectedTour.Lenguage.ToString();
            txtMaxNumberOfGuests.Text = selectedTour.MaxNumberOfGuests.ToString();
            txtCurentNumberOfGuests.Text = selectedTour.CurentNumberOfGuests.ToString();
            txtDuration.Text = selectedTour.TourDuration.ToString();
            List < CheckPoint >  checkPoints = selectedTour.CheckPoints;
            CheckPoints.ItemsSource = checkPoints;
            List<string> dates = new List<string>();
            //dates[1] = selectedTour.StartTime.ToString();
            //Dates.ItemsSource = dates;
        }

        private void Vouchers_Loaded(object sender, RoutedEventArgs e)
        {
            List<Voucher> vouchers = _vouchersRepository.ReadFromVouchersCsv(FilePath1);
            for (int i = 0; i < vouchers.Count; i++) {
                Vouchers.Items.Add(vouchers[i]);
            }
        }
    }
}
