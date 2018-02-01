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
    /// Логика взаимодействия для AddNewUnitWindow.xaml
    /// </summary>
    public partial class AddNewUnitWindow : Window
    {
        Entity db = new Entity();
        public AddNewUnitWindow()
        {
            InitializeComponent();
        }

        private void click_AddNewUnit(object sender, RoutedEventArgs e)
        {
            db.Units.Add(new Units() {
                FullName = tbxName.Text,
                ShortName = tbxShortName.Text
            });
            db.SaveChanges();
            MessageBox.Show("Новая еденица измерения добавлена!","Perfect",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();
            BuyWindow.SelectCBXUnit();
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
