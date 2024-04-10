using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace FinalTirthKumarSaud
{
    public partial class MainWindow : Window
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
        private List<string> categories = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            PopulateCategoriesComboBox();


        }

        private void PopulateCategoriesComboBox()
        {
            try
            {
                string query = $"SELECT * FROM categories";
                FetchProductsbyCombo(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllProductsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Products";
                FetchProducts(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FetchProducts(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //SqlCommand command = new SqlCommand(query, connection);
                    //connection.Open();

                    SqlDataAdapter da = new SqlDataAdapter(query, ConnectionString);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds, "Products");

                    DataTable dt = new DataTable("Products");
                    da.Fill(dt);

                    productsDataGrid.ItemsSource = dt.DefaultView;
                    productsDataGrid.AutoGenerateColumns = true;
                    productsDataGrid.CanUserAddRows = false;

                    //SqlDataReader reader = command.ExecuteReader();
                    //productsDataGrid.ItemsSource = reader;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            productsDataGrid.ItemsSource = null;
            categoriesComboBox.SelectedIndex = -1;
            Searchtext.Clear();
        }

        //private void SearchProductButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string productName = productNameTextBox.Text;
        //        string query = $"SELECT * FROM Products WHERE ProductName LIKE '%{productName}%'";
        //        FetchProducts(query);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string? selectedCategory = categoriesComboBox.SelectedItem?.ToString();
                if (selectedCategory != null)
                {
                   
                    string query = $"SELECT * FROM categories";
                    FetchProductsbyCombo(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Assuming AddProductWindow is a separate window in your project
                Add_Product addProduct = new Add_Product();
                addProduct.Owner = this;
                addProduct.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void productsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var inputSearchString = Searchtext.Text;

            string query = "SELECT * FROM Products where ProductName like '%" + inputSearchString + "%'";
            FetchProducts(query);
            
        }

        private void FetchProductsbyCombo(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //SqlCommand command = new SqlCommand(query, connection);
                    //connection.Open();

                    SqlDataAdapter da = new SqlDataAdapter(query, ConnectionString);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds, "Products");

                    DataTable dt = new DataTable("Products");
                    da.Fill(dt);
                   

                    categoriesComboBox.ItemsSource = dt.DefaultView;
                    categoriesComboBox.DisplayMemberPath = "CategoryName";

                    //SqlDataReader reader = command.ExecuteReader();
                    //productsDataGrid.ItemsSource = reader;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuOpemEvent(object sender, ContextMenuEventArgs e)
        {
            var test = "amittt";

        }

        private void comboclosingevent(object sender, ContextMenuEventArgs e)
        {
            var test = "amittt";
        }

       
    }

}