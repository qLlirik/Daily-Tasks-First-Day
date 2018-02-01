using FisrtDay.md;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для BuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        static public Entity db = new Entity();
        static public BuyWindow bw;

        public BuyWindow()
        {
            InitializeComponent();
            bw = this;

            btnBack.Click += (o, e) =>
            {
                this.Close();
            };

            SelectCBXCategory();
            SelectCBXSeller();
            SelectCBXUnit();

            cbxGoods.ItemsSource = db.Goods.ToList();
            cbxGoods.DisplayMemberPath = "Name";
            cbxGoods.SelectedIndex = 0;

            dpDateStart.SelectedDate = DateTime.Now;
        }
        //заполнение cbxSeller
        private void SelectCBXSeller()
        {
            var list = db.Companies.Where(w => w.ID != 6).ToList();
            cbxSeller.ItemsSource = list;
            cbxSeller.DisplayMemberPath = "Name";
            cbxSeller.SelectedIndex = 0;
        }
        //заполнение cbxCategory
        static public void SelectCBXCategory()
        {
            bw.cbxCategory.ItemsSource = db.Categories.ToList();
            bw.cbxCategory.DisplayMemberPath = "Name";
            bw.cbxCategory.SelectedIndex = 0;
        }
        //заполнение cbxUnit
        static public void SelectCBXUnit()
        {
            bw.cbxUnit.ItemsSource = db.Units.ToList();
            bw.cbxUnit.DisplayMemberPath = "ShortName";
            bw.cbxUnit.SelectedIndex = 0;
        }
        //добавление в ListView
        private void click_AddGoodInTheList(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tc.SelectedIndex == 0)
                {
                    lv.Items.Add(new Goods()
                    {
                        Name = tbxName.Text,
                        Picture = ImageToByte(tbxImage.Text),
                        CategoryID = cbxCategory.SelectedIndex + 1,
                        DateStart = dpDateStart.SelectedDate.Value,
                        Period = int.Parse(tbxPeriod.Text),
                        Manufacturer = tbxManufacturer.Text,
                        UnitID = cbxUnit.SelectedIndex + 1,
                        CostUnit = decimal.Parse(tbxCostUnit.Text),
                        Count = double.Parse(tbxCount.Text),
                    });
                }
                else
                {
                    var good = (Goods)cbxGoods.SelectedItem;
                    good.Count += int.Parse(tbxCount2.Text);
                    lv.Items.Add(good);
                }
            }
            catch
            { MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error); }
        }
        //перевод Image в Byte[]
        private Byte[] ImageToByte(string uri)
        {
            try
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(uri, UriKind.Absolute))));
                MemoryStream ms = new MemoryStream();
                encoder.Save(ms);
                return ms.ToArray();
            }
            catch { return null; }
        }
        //добавление новой компании
        private void click_AddNewCompanies(object sender, RoutedEventArgs e)
        {
            new AddNewCompaniesWindow().Show();
        }
        //добавление новой категории
        private void click_AddNewCategory(object sender, RoutedEventArgs e)
        {
            new AddNewCategoryWindow().Show();
        }
        //добавление новой ед.изм-я
        private void click_AddNewUnit(object sender, RoutedEventArgs e)
        {
            new AddNewUnitWindow().Show();
        }
        //выбор изображения
        private void click_SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.png;*.jpg;*.bmp";
            if (ofd.ShowDialog() == true)
            {
                tbxImage.Text = ofd.FileName;
            }
        }
        //оформление покупки
        private void click_Oforml(object sender, RoutedEventArgs e)
        {
            if (lv.Items.Count == 0)
            {
                MessageBox.Show("Выберите товар на покупку!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.Buy.Add(new Buy {
                SellerID = cbxSeller.SelectedIndex + 1,
                ClientID = 6,
                BuyDate = DateTime.Now
            });
            db.SaveChanges();

            foreach(var i in lv.Items)
            {
                var good = (Goods)i;

                var buyGoods = new BuyAndGoods();
                buyGoods.BuyID = db.Buy.ToList().Last().ID;

                if (good.ID != 0)
                {
                    var count = int.Parse(tbxCount2.Text);
                    buyGoods.GoodsID = good.ID;
                    buyGoods.Count = good.Count - (good.Count - count);
                }
                else
                {
                    buyGoods.Goods = good;
                    buyGoods.Count = good.Count;
                }
                db.BuyAndGoods.Add(buyGoods);
            }

            db.SaveChanges();
            MessageBox.Show("Покупка оформлена!","Perfect",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();
        }

        private void click_Delete(object sender, RoutedEventArgs e)
        {
            var sdf = (Goods)(((Button)sender).DataContext);
            lv.Items.Remove(sdf);
        }
    }
}
