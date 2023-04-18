using TravelAgency.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TravelAgency.View;
using TravelAgency.View.Guest2;
using TravelAgency.View.Owner;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.UserRepo;

namespace TravelAgency
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    if(user.LoginRole == "Owner")
                    {
                        
                        OwnerHome ownerHome = new OwnerHome(user);
                        ownerHome.Show();
                        Close();
                    }
                    if (user.LoginRole == "Guide")
                    {
                        GuideOverview guideOverview = new GuideOverview(user);
                        guideOverview.Show();
                        Close();
                    }
                    if (user.LoginRole == "Guest1")
                    {
                        Guest1Overview guest1Overview = new Guest1Overview(user);
                        guest1Overview.Show();
                        Close();
                    }
                    if (user.LoginRole == "Guest2")
                    {
                        Guest2Overview guest2Overview = new Guest2Overview(user);
                        guest2Overview.Show();
                        Close();
                    }
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
