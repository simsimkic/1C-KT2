using System.Windows;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for ReviewForm.xaml
    /// </summary>
    public partial class ReviewForm : Window
    {
        private readonly GradeService gradeService;
        public ReviewForm(User user)
        {
            InitializeComponent();
            gradeService = new GradeService();
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {
            ReviewData.ItemsSource = gradeService.ShowReviewsForOwner();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
