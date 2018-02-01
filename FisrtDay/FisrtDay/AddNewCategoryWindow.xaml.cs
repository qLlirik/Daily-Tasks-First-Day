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
    /// Логика взаимодействия для AddNewCategoryWindow.xaml
    /// </summary>
    public partial class AddNewCategoryWindow : Window
    {
        Entity db = new Entity();
        public AddNewCategoryWindow()
        {
            InitializeComponent();
        }

        private void click_AddNewCategory(object sender, RoutedEventArgs e)
        {
            db.Categories.Add(new Categories()
            {
                Name = tbxName.Text
            });
            db.SaveChanges();
            MessageBox.Show("Новая категория добавлена!", "Perfect", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            BuyWindow.SelectCBXCategory();
            
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
