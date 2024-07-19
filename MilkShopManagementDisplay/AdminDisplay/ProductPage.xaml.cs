using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reprository;
using Reprository.Models;
using Reprository.Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MilkShopManagementDisplay.AdminDisplay
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Window
    {
        private ProductService productservice = new ProductService();
        private productCategoriesService _categoriesService = new productCategoriesService();
        private MilkShopDbContext _dbContext = new MilkShopDbContext();


        public ProductPage()
        {
            InitializeComponent();
            LoadProduct();
            LoadCategory();
            txtID.IsReadOnly = true; // Disable editing
        }


        private void ProductManagementFrom_Load(object sender, RoutedEventArgs e)
        {
            LoadProduct();

            LoadCategory();

        }
        private void LoadProduct()
        {
            var result = productservice.GetAllProducts();
            dgvProductList.ItemsSource = null;
            dgvProductList.ItemsSource = result;


        }
        private void LoadCategory()
        {
            cbCategories.ItemsSource = _categoriesService.getAllCategories();
            cbCategories.DisplayMemberPath = "Name";
            cbCategories.SelectedIndex = 0;

        }

        private void Create_Product(object sender, RoutedEventArgs e)
        {
            txtID.IsReadOnly = true; // Disable editing
            List<Product> existingProducts = productservice.GetAllProducts();
            Product product = new Product();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!");
                return;
            }
            if (existingProducts.Any(p => p.Name.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("A product with this name already exists!");
                return;
            }
            product.Name = txtName.Text;
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description cannot be empty!");
                return;
            }
            if (txtDescription.Text.Length < 10)
            {
                MessageBox.Show("Description must be at least 10 characters long!");
                return;
            }
            product.Description = txtDescription.Text;

            // Validate and set product price
            if (string.IsNullOrEmpty(txtPrice.Text) || !double.TryParse(txtPrice.Text, out double price))
            {
                MessageBox.Show("Please enter a valid price!");
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("Price must be a positive number!");
                return;
            }
            product.Price = price;

            // Validate and set product quantity
            if (string.IsNullOrEmpty(txQuantity.Text) || !int.TryParse(txQuantity.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity!");
                return;
            }
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity In the Stock must be a positive number!");
                return;
            }
            product.QuantityInStock = quantity;

            // Set product active status
            product.IsActive = ckbActive.IsChecked ?? false;

            // Validate and set product category
            if (cbCategories.SelectedItem == null)
            {
                MessageBox.Show("Please select a category!");
                return;
            }
            Category selectedCategory = (Category)cbCategories.SelectedItem;
            product.CategoryId = selectedCategory.CategoryId;


            productservice.AddProduct(product);
            LoadProduct();
            MessageBox.Show("Add new product successfully");



        }

        private void Update_Product(object sender, RoutedEventArgs e)
        {

            txtID.IsReadOnly = true; // Disable editing
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Product ID is required.");
                return;
            }

            if (!int.TryParse(txtID.Text, out int productId))
            {
                MessageBox.Show("Invalid Product ID.");
                return;
            }

            try
            {
                Product product = _dbContext.Products.FirstOrDefault(x => x.ProductId == productId);
                if (product != null)
                {
                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        MessageBox.Show("Name cannot be empty!");
                        return;
                    }
                    product.Name = txtName.Text;

                    if (string.IsNullOrEmpty(txtDescription.Text))
                    {
                        MessageBox.Show("Description cannot be empty!");
                        return;
                    }
                    if (txtDescription.Text.Length < 10)
                    {
                        MessageBox.Show("Description must be at least 10 characters long!");
                        return;
                    }
                    product.Description = txtDescription.Text;

                    if (string.IsNullOrEmpty(txtPrice.Text) || !double.TryParse(txtPrice.Text, out double price))
                    {
                        MessageBox.Show("Please enter a valid price!");
                        return;
                    }
                    if (price <= 0)
                    {
                        MessageBox.Show("Price must be a positive number!");
                        return;
                    }
                    product.Price = price;

                    if (string.IsNullOrEmpty(txQuantity.Text) || !int.TryParse(txQuantity.Text, out int quantity))
                    {
                        MessageBox.Show("Please enter a valid quantity!");
                        return;
                    }
                    if (quantity <= 0)
                    {
                        MessageBox.Show("Quantity in the Stock must be a positive number!");
                        return;
                    }
                    product.QuantityInStock = quantity;

                    if (cbCategories.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a category!");
                        return;
                    }
                    product.CategoryId = (cbCategories.SelectedItem as Category).CategoryId;

                    product.IsActive = ckbActive.IsChecked ?? false;

                    productservice.UpdateProduct(product);

                    MessageBox.Show("Product updated successfully.");

                    LoadProduct();
                    clearValue();
                }
                else
                {
                    MessageBox.Show("The product with ID " + txtID.Text + " does not exist.");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An error occurred while updating the product: " + ex.Message);
            }
        }




        private void dgvProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            txtID.IsReadOnly = true; // Disable editing
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
                    ckbActive.IsChecked = items.IsActive;
                    cbCategories.SelectedItem = items.Category;
                    btnAdd.Visibility = Visibility.Collapsed;

                }

                else
                {
                    
                    btnAdd.Visibility = Visibility.Visible;
                }




                //else
                //{
                //    MessageBox.Show("No product selected.");
                //}
            }
            else
            {
                MessageBox.Show("Unexpected error: sender is not a ListView.");
            }
        }

        private void Delete_Product(object sender, RoutedEventArgs e)
        {
            txtID.IsReadOnly = true; // Disable editing
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show(txtID.Text + " doesn't exist ");
                return;
            }
            try
            {
                productservice.RemoveProduct(int.Parse(txtID.Text));
                MessageBox.Show("Delete product successfully");
                LoadProduct();
                clearValue();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(" product doesn't existed");
            }
        }

        private void Clear_Input(object sender, RoutedEventArgs e)
        {
            clearValue();
        }
        private void clearValue()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txQuantity.Text = "";
            ckbActive.IsChecked = false;
            cbCategories.SelectedIndex = 0; // Clear the selection of the combo box
            btnAdd.Visibility = Visibility.Visible;





        }




        private void txtKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtID.IsReadOnly = true; // Disable editing
            var keyword = txtKeyword.Text;
            double? minPrice = null;
            double? maxPrice = null;
           
          

            if (double.TryParse(txtPriceFrom.Text, out double parsedMinPrice))
            {
                if (parsedMinPrice < 0)
                {
                    MessageBox.Show("The value of Price must be possitive");
                    txtPriceFrom.Text = null;
                    return;
                }
                minPrice = parsedMinPrice;
            }

            if (double.TryParse(txtPriceTo.Text, out double parsedMaxPrice))
            {
                if (parsedMaxPrice < 0)
                {
                    MessageBox.Show("The value of Price must be possitive");
                    txtPriceTo.Text = null;
                    return;
                }
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

