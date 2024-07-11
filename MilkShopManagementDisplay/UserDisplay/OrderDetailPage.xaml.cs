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

namespace MilkShopManagementDisplay.UserDisplay
{
    /// <summary>
    /// Interaction logic for OrderDetailPage.xaml
    /// </summary>
    public partial class OrderDetailPage : Window
    {
        public Order SelectedOrder { get; set; } = null;

        private OrderDetailService _service = new OrderDetailService();

        private TblOrderDetail _selected = null;

        public OrderDetailPage()
        {
            InitializeComponent();
            ProductService services = new ProductService();

            btnRemoveSelection.IsEnabled = false;

            cboProductList.ItemsSource = services.GetAllProductsWithIsActive();

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

        private void btnAddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder.Status == true)
            {
                System.Windows.Forms.MessageBox.Show("This order has been confirmed and cannot be edited anymore",
                        "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboProductList.SelectedItem is Product selectedProduct)
            {
                if (int.TryParse(txtQuantity.Text, out int quantity))
                {
                    TblOrderDetail orderDetail = new TblOrderDetail
                    {
                        OrderId = SelectedOrder.OrderId,
                        ProductId = selectedProduct.ProductId,
                        Price = selectedProduct.Price,
                        Quantity = quantity
                    };

                    _service.AddOrderDetail(orderDetail);
                    FillOrderDetailDataGridView(SelectedOrder.OrderId);

                    cboProductList.SelectedItem = null;
                    txtPrice.Text = string.Empty;
                    txtQuantity.Text = string.Empty;

                    System.Windows.Forms.MessageBox.Show("Order Detail Created Successfully!.",
                       "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Please enter a valid quantity.",
                        "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a product.",
                    "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUpdateOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder.Status == true)
            {
                System.Windows.Forms.MessageBox.Show("This order has been confirmed and cannot be edited anymore",
                        "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_selected == null)
            {
                System.Windows.Forms.MessageBox.Show("Please choose a certain Order Detail to Update",
                    "Select one Order Detail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cboProductList.SelectedItem is Product selectedProduct)
            {
                if (int.TryParse(txtQuantity.Text, out int quantity))
                {
                    int oldQuantity = _selected.Quantity.GetValueOrDefault(0);
                    _selected.Quantity = quantity;

                    _service.UpdateOrderDetail(_selected,oldQuantity);
                    FillOrderDetailDataGridView(SelectedOrder.OrderId);

                    _selected = null;
                    dgvOrderDetailList.SelectedItem = null;
                    cboProductList.SelectedItem = null;
                    txtQuantity.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    btnAddOrderDetail.IsEnabled = true;
                    btnRemoveSelection.IsEnabled = false;

                    System.Windows.Forms.MessageBox.Show("Order Detail Updated Successfully!.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Please enter a valid quantity.",
                        "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a product.",
                    "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder.Status == true)
            {
                System.Windows.Forms.MessageBox.Show("This order has been confirmed and cannot be edited anymore",
                        "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_selected == null)
            {
                System.Windows.Forms.MessageBox.Show("Please choose a certain Order Detail to delete",
                    "Select one Order Detail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MessageBoxResult result = System.Windows.MessageBox.Show("Do you really want to delete this Order Detail?",
    "Delete Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            _service.DeleteOrderDetail(_selected);
            FillOrderDetailDataGridView(SelectedOrder.OrderId);

            _selected = null;
            dgvOrderDetailList.SelectedItem = null;
            cboProductList.SelectedItem = null;
            txtQuantity.Text = string.Empty;
            txtPrice.Text = string.Empty;
            btnAddOrderDetail.IsEnabled = true;
            btnRemoveSelection.IsEnabled = false;
        }

        private void dgvOrderDetailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvOrderDetailList.SelectedItems.Count > 0)
            {
                if (dgvOrderDetailList.SelectedItems[0] is TblOrderDetail selectedAirConditioner)
                {
                    _selected = selectedAirConditioner;
                    btnAddOrderDetail.IsEnabled = false;
                    btnRemoveSelection.IsEnabled = true;
                    txtQuantity.Text = selectedAirConditioner.Quantity.ToString();
                    cboProductList.SelectedItem = cboProductList.Items.OfType<Product>()
                    .FirstOrDefault(product => product.ProductId == _selected.ProductId);
                }

            }
            else
            {
                btnAddOrderDetail.IsEnabled = true;
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
            txtPrice.Text = null;
            cboProductList.SelectedItem = null;
            btnAddOrderDetail.IsEnabled = true;
            btnRemoveSelection.IsEnabled = false;
        }
    }
}
