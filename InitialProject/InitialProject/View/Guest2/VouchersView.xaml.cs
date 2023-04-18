using Cake.Core.IO;
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

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for VouchersView.xaml
    /// </summary>
    public partial class VouchersView : Window
    {

        private readonly VoucherRepository _repository;
        private const string FilePath = "../../../Resources/Data/vouchers.csv";
        User LogedUser = new Domain.Model.User();

        public VouchersView()
        {
            InitializeComponent();
            _repository = new VoucherRepository();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Voucher> vouchers = new List<Voucher>();
            vouchers = _repository.ReadFromVouchersCsv(FilePath);
            DataPanel.ItemsSource = vouchers;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    
    }
}
