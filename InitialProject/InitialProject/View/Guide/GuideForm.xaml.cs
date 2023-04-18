using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using static Azure.Core.HttpHeader;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for GuideForm.xaml
    /// </summary>
    public partial class GuideForm : Window
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TourRepository tourRepository;

        private readonly CheckPointRepository checkPointRepository;

        private readonly TourImageRepository tourImageRepository;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GuideForm()
        {
            InitializeComponent();
            Title = "Create new tour";
            DataContext = this;
            tourRepository = new TourRepository();
            checkPointRepository = new CheckPointRepository();
            tourImageRepository = new TourImageRepository();
        }

        private void SaveTour(object sender, RoutedEventArgs e)
        {
            string FilePath = "../../../Resources/Data/checkPoints.csv";           

            List<CheckPoint> checkPoints = new List<CheckPoint>();
            foreach (string item in ListCheckPoints.Items)
            {
                CheckPoint checkPoint = new CheckPoint();
                checkPoint.Name = item.ToString();
                checkPoint.Id = checkPointRepository.GetByName(FilePath, checkPoint.Name).Id;
                checkPoint.Status = "Neaktivna";
                checkPoints.Add(checkPoint);
            }
            if (ListCheckPoints.Items.Count < 2)
                MessageBox.Show("Morate uneti bar dve Kljucne tacke Pocetnu i krajnju");

            for (int i = 0; i < DateList.Items.Count; i++)
            {
                Tour newTour = new Tour(
                    tourRepository.NextId(),
                    txtName.Text,
                    txtCity.Text,
                    txtCountry.Text,
                    txtDescription.Text,
                    txtLangueg.Text,
                    Convert.ToInt32(txtMaxNumberOfGuests.Text),
                    Convert.ToDateTime(DateList.Items[i]),
                    Convert.ToInt32(txtTourDuration.Text),
                    checkPoints);
                Tour savedTour = tourRepository.Save(newTour);
                
            }

            foreach(string image  in ImageList.Items)
            {
                HotelImage img = new HotelImage();
                img.Url = image;
                tourImageRepository.Save(img);
            }


            MessageBox.Show("Uspesno uneta tura");
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddCheckPointToList(object sender, RoutedEventArgs e)
        {
            ListCheckPoints.Items.Add(CheckPointsCB.Text);
        }

        private void LoadCheckPoints(object sender, RoutedEventArgs e)
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            string FilePath = "../../../Resources/Data/checkPoints.csv";
            checkPoints = checkPointRepository.ReadFromCheckPointsCsv(FilePath);

            for (int i = 0; i < checkPoints.Count; i++)
            {
                
                CheckPointsCB.Items.Add(checkPoints[i].Name);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateList.Items.Add(StartDateBox.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ImageList.Items.Add(ImageTxt.Text);
        }
    }
}
