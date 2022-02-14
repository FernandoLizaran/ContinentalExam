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
using System.Text.RegularExpressions;

namespace ContinentalExam.Pages.Catalogs
{
    /// <summary>
    /// Lógica de interacción para ModalProduct.xaml
    /// </summary>
    public partial class ModalProduct : Window
    {
        public ModalProduct()
        {
            InitializeComponent();
        }

        private int Operation = 0;
        private string MessageBoxCaption = "Products Catalog";
        private List<int> ActiveCodes = null;

        public ModalProduct(string accionHeader, int operation, List<int> CurrentCodes)
        {
            InitializeComponent();
            LblAccion.Content = accionHeader;
            FillControls();
            Operation = operation;
            ActiveCodes = CurrentCodes;
            Txt_Product.Focus();
        }

        private void FillControls()
        {
            BLProducts tmpProducts = new BLProducts();
            List<EntProductType> lstProductTypes = tmpProducts.GetProductTypes();
            CmbPType.ItemsSource = lstProductTypes;

        }

        private bool ValidateSave()
        {
            string tmpFields = string.Empty;

            if (string.IsNullOrEmpty(Txt_Product.Text))
                tmpFields += "Product";

            if (Txt_Code.IsEnabled && string.IsNullOrEmpty(Txt_Code.Text))
                tmpFields += string.IsNullOrEmpty(tmpFields) ? "Code" : string.Format(", {0}", "Code");
            else if(Txt_Code.IsEnabled)
            {
                if (this.ActiveCodes.Contains(Convert.ToInt32(Txt_Code.Text)))
                {
                    MessageBox.Show(string.Format("The code: {0} already exists in the products table please change", Txt_Code.Text), MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                    Txt_Code.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(Txt_Price.Text))
                tmpFields += string.IsNullOrEmpty(tmpFields) ? "Price" : string.Format(", {0}", "Price");
            else
            {
                try
                {
                    Convert.ToDouble(Txt_Price.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("The Price: {0} does not contain the correct format, please correct it", Txt_Price.Text), MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                    Txt_Price.Focus();
                    return false;
                }
            }

            if (CmbPType.SelectedIndex < 0)
                tmpFields += string.IsNullOrEmpty(tmpFields) ? "Product Type" : string.Format(", {0}", "Product Type");

            

            if (!string.IsNullOrEmpty(tmpFields))
            {
                MessageBox.Show(string.Format("The following fields cannot be empty: {0}", tmpFields), MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else
                return true;
        }

        private EntProduct BuildProduct()
        {
            EntProduct tmpProduct = new EntProduct();
            tmpProduct.ProductId = string.IsNullOrEmpty(Txt_ProductId.Text) ? 0 : Convert.ToInt32(Txt_ProductId.Text);
            tmpProduct.Product = Txt_Product.Text;
            tmpProduct.Code = Convert.ToInt32(Txt_Code.Text);
            tmpProduct.Price = Convert.ToDouble(Txt_Price.Text);
            tmpProduct.Imported = (bool)Rdb_ImportedTrue.IsChecked;
            tmpProduct.ProductType = CmbPType.SelectedItem as EntProductType;

            return tmpProduct;
        }

        public void FillEdit(EntProduct Product)
        {
            Txt_ProductId.Text = Product.ProductId.ToString();
            Txt_Product.Text = Product.Product;
            Txt_Code.Text = Product.Code.ToString();
            Txt_Price.Text = Product.Price.ToString();
            Rdb_ImportedFalse.IsChecked = !(Rdb_ImportedTrue.IsChecked = Product.Imported);

            List<EntProductType> tmpPType = CmbPType.ItemsSource as List<EntProductType>;
            CmbPType.SelectedIndex = tmpPType.FindIndex(c => c.ProductTypeId == Product.ProductType.ProductTypeId);

            Txt_Code.IsEnabled = false;
        }

        private void Save()
        {
            try
            {
                BLProducts tmpProduct = new BLProducts();
                string ErrorMsg = string.Empty;

                if (tmpProduct.AddProduct(BuildProduct()))
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Cannot add the product to the database", MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update()
        {
            try
            {
                BLProducts tmpProduct = new BLProducts();
                string ErrorMsg = string.Empty;

                if (tmpProduct.UpdateProduct(BuildProduct()))
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Cannot update the product to the database", MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSave())
            {
                switch (Operation)
                {
                    case 1:
                        Save();
                        break;
                    case 2:
                        Update();
                        break;
                }
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreviewTextInputDouble(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
