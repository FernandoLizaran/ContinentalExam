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
using ContinentalExam.Entity.Catalogs;
using ContinentalExam.Business.Catalogs;

namespace ContinentalExam.Pages.Transactions
{
    /// <summary>
    /// Lógica de interacción para ModalBusqueda.xaml
    /// </summary>
    public partial class ModalSearch : Window
    {
        public ModalSearch()
        {
            InitializeComponent();
            FillLsb();
            Txt_Search.Focus();
        }

        EntProduct SelectedProduct = null;
        private string MessageBoxCaption = "Sales";

        private void FillLsb()
        {
            BLProducts tmpProducts = new BLProducts();
            List<EntProduct> LstProducts = tmpProducts.GetProducts();
            Lsb_Products.ItemsSource = LstProducts;
            Lsb_Products.DisplayMemberPath = "Product";
        }

        public EntProduct GetSelectedProduct()
        {
            return this.SelectedProduct;
        }

        private void SearchProduct(string Product)
        {
            FillLsb();
            List<EntProduct> tmpProducts = Lsb_Products.ItemsSource as List<EntProduct>;

            tmpProducts = (from item in tmpProducts
                          where item.Product.ToUpper().Contains(Product.ToUpper())
                          select new EntProduct(item.ProductId, item.Product, item.Code, item.Price, item.Imported, item.ProductType)).ToList<EntProduct>();

            Lsb_Products.ItemsSource = null;
            Lsb_Products.ItemsSource = tmpProducts;
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Txt_Search.Text))
                SearchProduct(Txt_Search.Text);
            else
                FillLsb();
        }

        private void Txt_Search_KeyUp(object sender, KeyEventArgs e)
        {
            Btn_Search_Click(this, new RoutedEventArgs());
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Btn_Select_Click(object sender, RoutedEventArgs e)
        {
            if (Lsb_Products.SelectedIndex >= 0)
            {
                this.SelectedProduct = Lsb_Products.SelectedItem as EntProduct;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please select a product from the list", MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                Lsb_Products.Focus();
            }
        }
    }
}
