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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContinentalExam.Entity.Catalogs;
using ContinentalExam.Business.Catalogs;
using ContinentalExam.Pages.Catalogs;

namespace ContinentalExam
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillGrid();
        }

        private string MessageBoxCaption = "Products Catalog";

        private void FillGrid()
        {
            try
            {
                BLProducts tmpProducts = new BLProducts();
                List<EntProduct> tmpList = tmpProducts.GetProducts();
                Dgr_Products.ItemsSource = tmpList;
            }
            catch (Exception ex)
            { }
        }

        private void Clean()
        {
            Dgr_Products.ItemsSource = null;
            FillGrid();
        }

        private void Search(string Value)
        {
            FillGrid();
            List<EntProduct> LstProducts = Dgr_Products.ItemsSource as List<EntProduct>;

            LstProducts = (from item in LstProducts
                     where item.Product.ToUpper().Contains(Value.ToUpper())
                     select new EntProduct(item.ProductId, item.Product, item.Code, item.Price, item.Imported, item.ProductType)).ToList<EntProduct>();

            Dgr_Products.ItemsSource = LstProducts;
        }

        private List<int> GetCodes()
        {
            FillGrid();
            List<EntProduct> LstProducts = Dgr_Products.ItemsSource as List<EntProduct>;

            List<int> currentCodes = LstProducts.Select(c => c.Code).ToList<int>();

            return currentCodes;
        }

        private void Btn_Editar_Click(object sender, RoutedEventArgs e)
        {
            EntProduct objProduct = ((FrameworkElement)sender).DataContext as EntProduct;
            ModalProduct tmpProduct = new ModalProduct("Update Product", 2, GetCodes());
            tmpProduct.FillEdit(objProduct);

            if (tmpProduct.ShowDialog() == true)
                MessageBox.Show("The product was updated successfully", this.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show("Product update failed", this.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Warning);

            Clean();
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Txt_Search.Text))
                Search(Txt_Search.Text);
            else
                FillGrid();
        }

        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        private void Txt_Search_KeyUp(object sender, KeyEventArgs e)
        {
            Btn_Search_Click(this, new RoutedEventArgs());
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            ModalProduct mProduct = new ModalProduct("Add Product", 1, GetCodes());

            if (mProduct.ShowDialog() == true)
                MessageBox.Show("New product added successfully", this.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show("Failed the addition of the new product", this.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Warning);

            Clean();
        }
    }
}
