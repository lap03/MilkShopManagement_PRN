using MilkShopManagementDisplay.UserDisplay;
using Reprository.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MilkShopManagementDisplay.AdminDisplay
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Window
    {
        private AdminService _service = new AdminService();

        private Order _selected = null;

        public OrderPage()
        {
            InitializeComponent();
        }

        private void OrderList_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            dgvOrderList.ItemsSource = null;
            dgvOrderList.ItemsSource = _service.GetAllOrders();
        }

        private void dgvOrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvOrderList.SelectedItems.Count > 0)
            {
                if (dgvOrderList.SelectedItems[0] is Order selectedOrder)
                {
                    _selected = selectedOrder;
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null)
            {
                System.Windows.Forms.MessageBox.Show("Please choose a certain Order to delete",
                    "Select one Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MessageBoxResult result = System.Windows.MessageBox.Show("Do you really want to delete this Order?",
    "Delete Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            _service.DeleteOrder(_selected);
            LoadDataToGrid();
            _selected = null;
        }
        private double CalculateTotal(ICollection<TblOrderDetail> orderDetails)
        {
            double total = 0;

            foreach (var detail in orderDetails)
            {
                total += detail.Price * detail.Quantity.GetValueOrDefault(0);
            }

            return total;
        }
        private void btnOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null)
            {
                System.Windows.Forms.MessageBox.Show("Please choose a certain Order to see its Details",
                    "Select one Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            OrderDetailPage f = new OrderDetailPage();

            f.SelectedOrder = _selected;
            f.LoadSelectedOrder();
            f.Show();
        }
    }
}
