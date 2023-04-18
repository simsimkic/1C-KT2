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
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for MoveReservation.xaml
    /// </summary>
    public partial class MoveReservationView : Window
    {
        private readonly MoveReservationRepository moveReservationRepository;
        private readonly ReservationService reservationService;
        public MoveReservation SelectedReservation { get; set; }
        public MoveReservationView()
        {
            moveReservationRepository = new MoveReservationRepository();
            reservationService = new ReservationService();
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = moveReservationRepository.GetAll();
        }

        private void AcceptMoveReservation(object sender, RoutedEventArgs e)
        {
            if (this.SelectedReservation != null) 
            {
                SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
                reservationService.MoveReservation(SelectedReservation.ReservationId, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
                DataPanel.ItemsSource = moveReservationRepository.GetAll();
            }
            else { }       
        }

        private void DeclineMoveReservation(object sender, RoutedEventArgs e)
        {
            if(SelectedReservation != null) 
            {
                SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
                moveReservationRepository.Delete(SelectedReservation);
                DataPanel.ItemsSource = moveReservationRepository.GetAll();
            }
            else { }   
        }

        private void ReservationInfo(object sender, SelectionChangedEventArgs e)
        {
            SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
            ReservationInfoLabel.Content = reservationService.TextForReservationInfo(SelectedReservation.ReservationId, SelectedReservation.HotelName,SelectedReservation.NewStartDate,SelectedReservation.NewEndDate);
        }
    }
}
