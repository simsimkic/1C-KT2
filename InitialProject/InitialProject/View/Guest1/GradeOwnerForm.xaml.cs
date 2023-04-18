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
using Microsoft.Win32;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository;
using TravelAgency.Services;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.HotelRepo;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for GradeOwnerForm.xaml
    /// </summary>
    public partial class GradeOwnerForm : Window
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly ReservationRepository reservationRepository;
        private readonly HotelRepository hotelRepository;
        private readonly GradeService gradeService;
        private readonly HotelService hotelService;
        private readonly ReservationService reservationService;
        private User LogedUser { get; set; }
        public GradeOwnerForm(User user)
        {
            InitializeComponent();
            Title = "Grade owner";
            DataContext = this;
            ownerGradeRepository = new OwnerGradeRepository();
            reservationRepository = new ReservationRepository();
            hotelRepository = new HotelRepository();
            gradeService = new GradeService();
            hotelService = new HotelService();
            reservationService = new ReservationService();
            LogedUser = user;
        }

        
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            txtImg.Text = file.FileName;
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            string imageUrl = txtImg.Text;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                ListViewItem item = new ListViewItem
                {
                    Content = imageUrl
                };

                ListViewImg.Items.Add(item);

                txtImg.Text = "";
            }
        }

        private void Grade(object sender, RoutedEventArgs e)
        {
            object selectedItem = cbHotelName.SelectedItem;
            Reservation reservation = new Reservation();
            
            int id;
            string line = selectedItem.ToString();
            string[] fields = line.Split(' ');
            id = Convert.ToInt32(fields[0]);

            int hotelRating = 0;
            if (rbHotelOption1.IsChecked == true)
            {
                hotelRating = 1;
            }
            else if (rbHotelOption2.IsChecked == true)
            {
                hotelRating = 2;
            }
            else if (rbHotelOption3.IsChecked == true)
            {
                hotelRating = 3;
            }
            else if (rbHotelOption4.IsChecked == true)
            {
                hotelRating = 4;
            }
            else if (rbHotelOption5.IsChecked == true)
            {
                hotelRating = 5;
            }

            int ownerRating = 0;
            if (rbOwnerOption1.IsChecked == true)
            {
                ownerRating = 1;
            }
            else if (rbOwnerOption2.IsChecked == true)
            {
                ownerRating = 2;
            }
            else if (rbOwnerOption3.IsChecked == true)
            {
                ownerRating = 3;
            }
            else if (rbOwnerOption4.IsChecked == true)
            {
                ownerRating = 4;
            }
            else if (rbOwnerOption5.IsChecked == true)
            {
                ownerRating = 5;
            }

            List<string> images = new List<string>();
            foreach (object item in ListViewImg.Items)
            {
                if (item is string)
                {
                    images.Add(item as string);
                }
            }

            Hotel selectedOwnerUsername = hotelService.GetHotelByName(cbHotelName.Text);

            OwnerGrade newGrade = new OwnerGrade(
                LogedUser.Username,
                selectedOwnerUsername.OwnerUsername,
                id,
                hotelRating,
                ownerRating,
                txtComment.Text
            );
            ownerGradeRepository.Save(newGrade);

            txtComment.Clear();
            rbHotelOption1.IsChecked = false;
            rbHotelOption2.IsChecked = false;
            rbHotelOption3.IsChecked = false;
            rbHotelOption4.IsChecked = false;
            rbHotelOption5.IsChecked = false;
            rbOwnerOption1.IsChecked = false;
            rbOwnerOption2.IsChecked = false;
            rbOwnerOption3.IsChecked = false;
            rbOwnerOption4.IsChecked = false;
            rbOwnerOption5.IsChecked = false;
            ListViewImg.Items.Clear();
        }



        private void LoadHotels(object sender, RoutedEventArgs e)
        {
            List<string> hotelNames = new List<string>();
            List<Reservation> reservations = reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.EndDate >= DateTime.Today.AddDays(-5) && reservation.EndDate <= DateTime.Today)
                {
                    if (!hotelNames.Contains(reservation.HotelName))
                    {
                        hotelNames.Add(reservation.HotelName);
                    }
                }
            }

            cbHotelName.ItemsSource = hotelNames;
        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewImg.SelectedItem != null)
            {
                ListViewImg.Items.Remove(ListViewImg.SelectedItem);
            }
        }
    }
}
