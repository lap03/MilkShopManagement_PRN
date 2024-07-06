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
    public partial class OrderDetailPage : Window
    {
        public Order SelectedOrder { get; set; } = null;

        private OrderDetailService _service = new OrderDetailService();

        private TblOrderDetail _selected = null;

        public OrderDetailPage()
        {
            InitializeComponent();
            ProductService services = new ProductService();


            cboProductList.ItemsSource = services.GetAllProducts();

            cboProductList.DisplayMemberPath = "Name";

            cboProductList.SelectedValuePath = "ProductId";

            cboProductList.SelectionChanged += cboProductList_SelectionChanged;
        }

        public void LoadSelectedOrder()
        {
            if (SelectedOrder != null)
            {
                txtOrderId.Text = SelectedOrder.OrderId.ToString();
                txtOrderId.IsReadOnly = true;
                FillOrderDetailDataGridView(SelectedOrder.OrderId);
            }
        }

        private void FillOrderDetailDataGridView(int id)
        {
            dgvOrderDetailList.ItemsSource = null;
            dgvOrderDetailList.ItemsSource = _service.GetAllOrders(id);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
       

        

        private void dgvOrderDetailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvOrderDetailList.SelectedItems.Count > 0)
            {
                if (dgvOrderDetailList.SelectedItems[0] is TblOrderDetail selectedAirConditioner)
                {
                    _selected = selectedAirConditioner;
                    txtQuantity.Text = selectedAirConditioner.Quantity.ToString();
                    cboProductList.SelectedItem = cboProductList.Items.OfType<Product>()
                    .FirstOrDefault(product => product.ProductId == _selected.ProductId);
                }

            }
            else
            {
            }
        }

        private void cboProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboProductList.SelectedItem is Product selectedProduct)
            {
                txtPrice.Text = selectedProduct.Price.ToString();
                txtPrice.IsReadOnly = true;
            }
        }

        private void btnRemoveSelection_Click(object sender, RoutedEventArgs e)
        {
            _selected = null;
            dgvOrderDetailList.SelectedItem = null;
            txtQuantity.Text = null;
            cboProductList.SelectedItem = null;
        }
    }
}
