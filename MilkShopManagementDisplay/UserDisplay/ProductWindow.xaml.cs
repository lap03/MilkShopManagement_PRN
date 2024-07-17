using Reprository.Models;
using Reprository;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MilkShopManagementDisplay.UserDisplay
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
        }

        private ProductService productservice = new ProductService();
        private productCategoriesService _categoriesService = new productCategoriesService();
        private MilkShopDbContext _dbContext = new MilkShopDbContext();





        private void ProductManagementFrom_Load(object sender, RoutedEventArgs e)
        {
            LoadProduct();

        }
        private void LoadProduct()
        {
            var result = productservice.GetAllProductsWithIsActive();
            dgvProductList.ItemsSource = null;
            dgvProductList.ItemsSource = result;


        }
        private void dgvProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var listView = sender as System.Windows.Controls.DataGrid;
            if (listView != null)
            {
                Product items = listView.SelectedItem as Product;
                if ((items != null))
                {
                    txtID.Text = items.ProductId.ToString();
                    txtName.Text = items.Name;
                    txtDescription.Text = items.Description;
                    txtPrice.Text = items.Price.ToString();
                    txQuantity.Text = items.QuantityInStock.ToString();

                }
            }
        }

        private void txtKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = txtSearch.Text;
            double? minPrice = null;
            double? maxPrice = null;

            if (double.TryParse(txtPriceFrom.Text, out double parsedMinPrice))
            {
                minPrice = parsedMinPrice;
            }

            if (double.TryParse(txtPriceTo.Text, out double parsedMaxPrice))
            {
                maxPrice = parsedMaxPrice;
            }

            var result = productservice.SearchProductByKeywordAndPriceRange(keyword, minPrice, maxPrice);
            if (result != null)
            {
                dgvProductList.ItemsSource = null;
                dgvProductList.ItemsSource = result;
            }
        }
    }
}

