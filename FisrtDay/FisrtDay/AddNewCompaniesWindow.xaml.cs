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
using FisrtDay.md;

namespace FisrtDay
{
    /// <summary>
    /// Логика взаимодействия для AddNewCompaniesWindow.xaml
    /// </summary>
    public partial class AddNewCompaniesWindow : Window
    {
        Entity db = new Entity();

        public AddNewCompaniesWindow()
        {
            InitializeComponent();

            btnBack.Click += (o, e) =>
            {
                this.Close();
            };

            cbxCountry.ItemsSource = db.Countries.ToList();
            cbxCountry.DisplayMemberPath = "Name";
            cbxCountry.SelectedIndex = 0;
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Companies.Add(new Companies() {
                    Name = tbxName.Text,
                    INN = tbxINN.Text,
                    CountryID = cbxCountry.SelectedIndex + 1,
                    Cheif = tbxCheif.Text,
                    Address = tbxAddress.Text,
                    CheifPhone = tbxCheifPhone.Text,
                    Manager = tbxManager.Text,
                    PhonePlus = tbxManager.Text,
                    Bank = tbxBank.Text,
                    BankAccount = tbxBankAccount.Text
                });

                db.SaveChanges();
                MessageBox.Show("Новая компания добавлена!", "Perfect", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
