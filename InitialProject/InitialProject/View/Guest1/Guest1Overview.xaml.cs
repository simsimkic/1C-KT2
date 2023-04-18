using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Graph.Models.Security;
using TravelAgency.Domain.Model;
using TravelAgency.Forms;
using TravelAgency.Repository;
using TravelAgency.View.Guest1;

namespace TravelAgency.View
{
    public partial class Guest1Overview : Window
    {
        public User LoggedInUser { get; set; }
        public Guest1Overview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        private void OpenGuest1Form(object sender, RoutedEventArgs e)
        {
            Guest1Form createGuest1Form = new Guest1Form();
            createGuest1Form.Show();
        }

        private void OpenReserveForm(object sender, RoutedEventArgs e)
        {
            ReservationForm createReservationForm = new ReservationForm(LoggedInUser);
            createReservationForm.Show();
        }

        private void OpenAccount(object sender, RoutedEventArgs e)
        {
            Guest1AccountForm createAccountForm = new Guest1AccountForm(LoggedInUser);
            createAccountForm.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoveReservationForm moveReservationForm = new MoveReservationForm(LoggedInUser);
            moveReservationForm.Show();
        }
    }
}

