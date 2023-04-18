using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using TravelAgency.Services;
using Microsoft.Win32;
using TravelAgency.Domain.Model;

namespace TravelAgency.Forms
{
    public partial class OwnerForm : Window
    {
        private readonly HotelService hotelService;
        public static OwnerForm ownerForm;
        private User LogedUser;
        public OwnerForm(User user)
        {
            InitializeComponent();
            hotelService = new HotelService();
            LogedUser = user;
            ownerForm = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            hotelService.SaveHotel(ButtonActivator(), LogedUser.Username);
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            hotelService.AddHotelImage();
        }

        private void DeleteImage(object sender, RoutedEventArgs e)
        {
            hotelService.DeleteHotelImage();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            hotelService.ClearListOfImage();
            this.Close();
        }

        //Validation 
        public bool ButtonActivator()
        {
            if (
                LabelNameValidator.Content == "" &&
                LabelCityValidator.Content == "" &&
                LabelCountryValidator.Content == "" &&
                LabelMaxGuestValidator.Content == "" &&
                LabelMinDaysValidator.Content == "" &&
                LabelCancelDaysValidator.Content == "" &&
                LabelTypeValidator.Content == "" &&
                LabelImgValidator.Content == "" &&
                txtName.Text != "" &&
                txtCity.Text != "" &&
                txtCountry.Text != "" &&
                brMax.Text != "" &&
                brMin.Text != "" &&
                brDaysLeft.Text != "" &&
                Type.Text != "")
            {
                return true;
            }
            else
            { return false; }
        }

        private void NameValidation(object sender, TextChangedEventArgs e)
        {
            string name = txtName.Text;
            if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                LabelNameValidator.Content = "";
            }
            else
            {
                LabelNameValidator.Content = "Please enter valid value";
            }
        }

        private void CityValidation(object sender, TextChangedEventArgs e)
        {
            string city = txtCity.Text;
            if (Regex.IsMatch(city, @"^[a-zA-Z\s]+$"))
            {
                LabelCityValidator.Content = "";
            }
            else
            {
                LabelCityValidator.Content = "Please enter valid value";
            }
        }

        private void CountryValidation(object sender, TextChangedEventArgs e)
        {
            string country = txtCountry.Text;
            if (Regex.IsMatch(country, @"^[a-zA-Z\s]+$"))
            {
                LabelCountryValidator.Content = "";
            }
            else
            {
                LabelCountryValidator.Content = "Please enter valid value";
            }
        }

        private void MaxGuestValidation(object sender, TextChangedEventArgs e)
        {
            string maxGuests = brMax.Text;
            if (Regex.IsMatch(maxGuests, @"^[0-9]+$"))
            {
                LabelMaxGuestValidator.Content = "";
            }
            else
            {
                LabelMaxGuestValidator.Content = "Please enter valid value";
            }
        }

        private void MinDaysValidation(object sender, TextChangedEventArgs e)
        {
            string minDays = brMin.Text;
            if (Regex.IsMatch(minDays, @"^[0-9]+$"))
            {
                LabelMinDaysValidator.Content = "";
            }
            else
            {
                LabelMinDaysValidator.Content = "Please enter valid value";
            }
        }
        private void CancelDaysValidation(object sender, TextChangedEventArgs e)
        {
            string cancelDays = brDaysLeft.Text;
            if (Regex.IsMatch(cancelDays, @"^[0-9]+$"))
            {
                LabelCancelDaysValidator.Content = "";
            }
            else
            {
                LabelCancelDaysValidator.Content = "Please enter valid value";
            }
        }

        private void UrlValidation(object sender, TextChangedEventArgs e)
        {
            string url = txtImg.Text;
            if (Regex.IsMatch(url, @"(?: ([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:jpg|gif|png))(?:\?([^#]*))?(?:#(.*))?"))
            {
                LabelUrlValidator.Content = "";
                AddImgButton.IsEnabled = true;
            }
            else
            {
                LabelUrlValidator.Content = "Please input valid ulr address";
                AddImgButton.IsEnabled = false;
            }
        }

        private void DataFill(object sender, RoutedEventArgs e)
        {
            Type.Items.Add("Hotel");
            Type.Items.Add("Hut");
            Type.Items.Add("House");
            Type.Items.Add("Apartment");
        }

        private void ValidationType(object sender, SelectionChangedEventArgs e)
        {
            LabelTypeValidator.Content = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            txtImg.Text = file.FileName;
        }
    }
}
