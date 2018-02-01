using FisrtDay.md;
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

namespace FisrtDay
{
    /// <summary>
    /// Логика взаимодействия для SaleWindow.xaml
    /// </summary>
    public partial class SaleWindow : Window
    {
        static public Entity db = new Entity();
        static public SaleWindow sw;
        public SaleWindow()
        {
            InitializeComponent();
            sw = this;

            cbxGoods.ItemsSource = db.Goods.ToList();
            cbxGoods.DisplayMemberPath = "Name";
            cbxGoods.SelectedIndex = 0;

            SelectCBXSeller();
        }

        static public void SelectCBXSeller()
        {
            var list = db.Companies.Where(w => w.ID != 6).ToList();
            sw.cbxSeller.ItemsSource = list;
            sw.cbxSeller.DisplayMemberPath = "Name";
            sw.cbxSeller.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void click_AddNewCompanies(object sender, RoutedEventArgs e)
        {
            new AddNewCompaniesWindow().Show();
        }

        private void click_AddInTheList(object sender, RoutedEventArgs e)
        {
            var good = (Goods)cbxGoods.SelectedValue;
            good.Count2 = int.Parse(tbxCount.Text);
            lv.Items.Add(good);
        }

        private void click_Oforml(object sender, RoutedEventArgs e)
        {
            if (lv.Items.Count == 0)
            {
                MessageBox.Show("Выберите товар на продажу!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            foreach(var i in lv.Items)
            {
                var good = (Goods)i;
                if (good.Count < good.Count2)
                {
                    MessageBox.Show("Продажа не может быть произведена из-за нехватки товара на складе!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            db.Buy.Add(new Buy
            {
                SellerID = 6,
                ClientID = cbxSeller.SelectedIndex + 1,
                BuyDate = DateTime.Now
            });
            db.SaveChanges();

            foreach (var i in lv.Items)
            {
                var good = (Goods)i;
                var buyGoods = new BuyAndGoods();
                buyGoods.BuyID = db.Buy.ToList().Last().ID;

                buyGoods.GoodsID = good.ID;
                buyGoods.Count = good.Count2;
                db.BuyAndGoods.Add(buyGoods);
                good.Count -= good.Count2;
                
            }

            db.SaveChanges();
            MessageBox.Show("Покупка оформлена!", "Perfect", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void click_Delete(object sender, RoutedEventArgs e)
        {
            var sdf = (Goods)(((Button)sender).DataContext);
            lv.Items.Remove(sdf);
        }
    }
}
