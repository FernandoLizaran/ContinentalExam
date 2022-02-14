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
using System.Text.RegularExpressions;
using ContinentalExam.Entity.Catalogs;
using ContinentalExam.Entity.Transactions;
using ContinentalExam.Business.Catalogs;
using System.Diagnostics;

namespace ContinentalExam.Pages.Transactions
{
    /// <summary>
    /// Lógica de interacción para Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        public Sales()
        {
            InitializeComponent();
        }

        private string MessageBoxCaption = "Sales";
        private EntProduct CurrentProduct = null;

        private bool FindProductByCode(string ProductName)
        {
            BLProducts tmpProducts = new BLProducts();
            List<EntProduct> LstProducts = tmpProducts.GetProducts();
            int Code = Convert.ToInt32(Txt_ProductCode.Text);

            CurrentProduct = LstProducts.FirstOrDefault<EntProduct>(c => c.Code == Code);

            return (CurrentProduct != null);
        }

        private void AddProductByCode()
        {
            List<EntSale> LstSales = AddRowToGrid(CurrentProduct);
            Clean();
        }

        private List<EntSale> AddRowToGrid(EntProduct newProduct)
        {
            List<EntSale> LstSales = null;

            if (Dgr_Sales.Items.Count > 0)
                LstSales = Dgr_Sales.ItemsSource as List<EntSale>;
            else
                LstSales = new List<EntSale>();

            EntSale newSale = new EntSale();
            newSale.Product = newProduct;
            newSale.Quantity = Convert.ToInt32(Txt_Quantity.Text);

            LstSales.Add(newSale);

            Dgr_Sales.ItemsSource = null;
            Dgr_Sales.ItemsSource = LstSales;

            return LstSales;
        }

        private void Clean()
        {
            Txt_ProductCode.Text = string.Empty;
            Txt_Quantity.Text = string.Empty;
            Lbl_Product.Content = string.Empty;
            Txt_ProductCode.Focus();
        }

        private void CalculateTotals()
        {
            List<EntSale> Sales = Dgr_Sales.ItemsSource as List<EntSale>;

            EntTicket tmpTicket = new EntTicket();
            tmpTicket.Sales = Sales;

            Lbl_Total.Content = string.Format("Total Sale: {0}", tmpTicket.Total);
        }

        private TargetType GetParent<TargetType>(DependencyObject o)
            where TargetType : DependencyObject
        {
            if (o == null || o is TargetType) return (TargetType)o;
            return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddProductByCode();
            CalculateTotals();
        }

        private void Txt_ProductCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(Txt_ProductCode.Text))
            {
                if (e.Key == Key.Return)
                {
                    Txt_Quantity.Focus();
                }
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Txt_Quantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(Txt_Quantity.Text))
            {
                if (e.Key == Key.Return)
                {
                    Btn_Add_Click(this, new RoutedEventArgs());
                }
            }
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {

            ModalSearch tmpSearch = new ModalSearch();
            if (tmpSearch.ShowDialog() == true)
            {
                CurrentProduct = tmpSearch.GetSelectedProduct();
                Txt_ProductCode.Text = CurrentProduct.Code.ToString();
                Lbl_Product.Content = CurrentProduct.Product;
                Txt_Quantity.Focus();
            }
        }

        private void Txt_ProductCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Txt_ProductCode.Text))
            {
                string Code = Txt_ProductCode.Text;
                if (FindProductByCode(Code))
                {
                    Lbl_Product.Content = CurrentProduct.Product;
                    Txt_Quantity.Focus();
                }
                else
                {
                    Txt_ProductCode.Text = string.Empty;
                    MessageBox.Show(string.Format("The product code: {0}; does not exist in the database", Code), MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            var index = Dgr_Sales.Items.IndexOf(row.Item);
            EntProduct objProduct = ((FrameworkElement)sender).DataContext as EntProduct;

            List<EntSale> LstSales = Dgr_Sales.ItemsSource as List<EntSale>;
            LstSales.RemoveAt(index);

            Dgr_Sales.ItemsSource = null;
            Dgr_Sales.ItemsSource = LstSales;
            CalculateTotals();
        }

        private void Btn_CancelSale_Click(object sender, RoutedEventArgs e)
        {
            Dgr_Sales.ItemsSource = null;
            this.CurrentProduct = null;
            Lbl_Quantity.Content = "Total Sale: 0";
            Clean();
        }

        private void Btn_Finish_Click(object sender, RoutedEventArgs e)
        {
            List<EntSale> Sales = Dgr_Sales.ItemsSource as List<EntSale>;
            EntTicket tmpTicket = new EntTicket();
            string ErrorMsg = string.Empty;
            string Path = @".\TmpTicket.pdf";

            tmpTicket.Sales = Sales;


            if (tmpTicket.MakeTicket(Path, ref ErrorMsg))
            {
                Process.Start(Path);
            }
            else
            {
                MessageBox.Show(string.Format("Cannot make Ticket because this error: {0}", ErrorMsg), MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_ProductsCatalog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow tmpCatalog = new MainWindow();
            tmpCatalog.ShowDialog();
        }

    }
}
