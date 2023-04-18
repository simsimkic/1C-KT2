using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerProfil.xaml
    /// </summary>
    public partial class OwnerProfil : Window
    {
        private User LogedOwner { get; set; }
        private readonly OwnerService ownerService;
        public OwnerProfil(User user)
        {
            InitializeComponent();
            LogedOwner = user;
            ownerService = new OwnerService();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            App.Current.Windows[0].Close();
            this.Close();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            UsernameTXT.Text = LogedOwner.Username;
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SuperOwnerLabel_Loaded(object sender, RoutedEventArgs e)
        {
            SuperOwnerLabel.Content = ownerService.SuperOwner(LogedOwner.Username);
        }
    }
}
