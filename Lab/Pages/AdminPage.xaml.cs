using Lab.Classes;
using Lab.Models;
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

namespace Lab.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        LabEntities db =  DBConnect.GetContext();
        public AdminPage()
        {
            InitializeComponent();
            if (LogsTab.IsSelected == true)
            {
                LogsLV.ItemsSource = db.Logs.ToList();
            }
            else if (ConsumbalesTab.IsSelected == true)
            {
                ConsumablesLV.ItemsSource = db.Consumables.ToList();
            }
        }

        private void LoginFiltTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginFiltTB.Text == "")
            {
                LogsLV.ItemsSource = db.Logs.ToList();
            }
            else
            {
                LogsLV.ItemsSource = (System.Collections.IEnumerable)db.Logs.ToList().FirstOrDefault(x => x.Users.UserLogin.Contains(LoginFiltTB.Text));
            }
        }

        private void DateEnterFiltTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DateEnterFiltTB.Text == "")
            {
                LogsLV.ItemsSource = db.Logs.ToList();
            }
            else
            {
                LogsLV.ItemsSource = (System.Collections.IEnumerable)db.Logs.ToList().FirstOrDefault(x => x.LogDate.ToString().Contains(DateEnterFiltTB.Text));
            }
        }

        private void AddBioBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMatBio.Content = "Добавить";
            BioGrid.Visibility = Visibility.Visible;
            ConsGrid.Visibility = Visibility.Hidden;
        }



        private void MatAddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMatBio.Content = "Добавить";
            BioGrid.Visibility = Visibility.Hidden;
            ConsGrid.Visibility = Visibility.Visible;
        }



        private void AddMatBio_Click(object sender, RoutedEventArgs e)
        {

            if (BioGrid.Visibility == Visibility.Hidden)
            {
                if (TemporaryStorage.SelectedID != null)
                {

                } else
                {
                    Consumables consumables = new Consumables
                    {
                        ConsumablesID = db.Consumables.Max(x => x.ConsumablesID) + 1,
                        ConsumablesTitle = Title.Text,
                        ConsumableCount = Convert.ToInt32(Count.Text),
                        LaboratorianID = 1
                    };
                    db.Consumables.Add(consumables);
                    db.SaveChanges();
                }
            }
            else
            {
                Bio bio = new Bio
                {
                    BioID = db.Bio.Max(x => x.BioID) + 1,
                    BioTitle = Title.Text,
                    BioCount = Convert.ToInt32(Count.Text),
                    BioPatientID = 1
                };
                db.Bio.Add(bio);
                db.SaveChanges();
            }
        }

        private void ConsumablesLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TemporaryStorage.SelectedID = BioLV.SelectedIndex + 1;
            BioGrid.Visibility = Visibility.Hidden;
            ConsGrid.Visibility = Visibility.Visible;
            AddMatBio.Content = "Изменить";
            BioLB.Content = "Редактирование биоматериала №" + TemporaryStorage.SelectedID;
            Consumables consumables = db.Consumables.ToList().FirstOrDefault(x => x.ConsumablesID == TemporaryStorage.SelectedID);
            Title.Text = consumables.ConsumablesTitle;
        }

        private void BioLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TemporaryStorage.SelectedID = BioLV.SelectedIndex + 1;
            BioGrid.Visibility = Visibility.Visible;
            ConsGrid.Visibility = Visibility.Hidden;
            AddMatBio.Content = "Изменить";
            ConsLb.Content = "Редатирование расходного материала №" + TemporaryStorage.SelectedID;
            Bio bio = db.Bio.ToList().FirstOrDefault(x => x.BioID == TemporaryStorage.SelectedID);
            TitleBio.Text = bio.BioTitle;
        }

        private void BioLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TemporaryStorage.SelectedID = ConsumablesLV.SelectedIndex + 1;
            MessageBoxResult result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Bio bio = db.Bio.ToList().FirstOrDefault(x => x.BioID == TemporaryStorage.SelectedID);
                db.Bio.Remove(bio);
                db.SaveChanges();
            }
        }

        private void ConsumablesLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TemporaryStorage.SelectedID = ConsumablesLV.SelectedIndex + 1;
            MessageBoxResult result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Consumables consumables = db.Consumables.ToList().FirstOrDefault(x => x.ConsumablesID == TemporaryStorage.SelectedID);
                db.Consumables.Remove(consumables);
                db.SaveChanges();
            }
            ConsumablesLV.ItemsSource = db.Consumables.ToList();
        }
    }
}

