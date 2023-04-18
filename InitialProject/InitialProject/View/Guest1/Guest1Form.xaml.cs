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
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for Guest1Form.xaml
    /// </summary>
    public partial class Guest1Form : Window
    {
        private readonly HotelRepository hotelRepository;
        private readonly HotelService hotelService;
        private readonly OwnerService ownerService;

        const string FilePath = "../../../Resources/Data/hotels.csv";
        public Guest1Form()
        {
            InitializeComponent();
            Title = "Search hotel";
            DataContext = this;
            hotelRepository = new HotelRepository();
            hotelService = new HotelService();
            ownerService = new OwnerService();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelRepository.GetAll();
            List<User> superOwners = ownerService.GetAllSuperOwners();
            foreach (Hotel hotel in hotels)
            {
                foreach(User superOwner in superOwners)
                {
                    if (hotel.OwnerUsername == superOwner.Username)
                        hotel.OwnerUsername = hotel.OwnerUsername + " Super-Owner";
                }
                
            }
            
            DataPanel.ItemsSource = hotelService.SortBySuperOwner(hotels);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            string RadioChoice = null;
            if (RadioHouse.IsChecked == true) { RadioChoice = "House"; }
            else if (RadioHotel.IsChecked == true) { RadioChoice = "Hotel"; }
            else if (RadioHut.IsChecked == true) { RadioChoice = "Hut"; }
            else if (RadioApartment.IsChecked == true) { RadioChoice = "Apartment"; }
            hotels = hotelService.FindHotel(txtName.Text, txtCity.Text, txtCountry.Text, RadioChoice, txtNoGuests.Text, txtNoDays.Text);
            DataPanel.ItemsSource = hotels;
        }
        private void DataPanel_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "NumberOfDaysToCancel")
            {
                e.Cancel = true;
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
