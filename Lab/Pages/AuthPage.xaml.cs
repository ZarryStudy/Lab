using Lab.Classes;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        Models.LabEntities db = Classes.DBConnect.GetContext();
        int time;
        int hour, minute, second;
        public AuthPage()
        {
            InitializeComponent();
            if (TemporaryStorage.Time != 0)
            {
                time = TemporaryStorage.Time;
                System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

                hour = time / 3600;
                minute = (time - (3600 * hour)) / 60;
                second = time - (3600 * hour + minute * 60);
                timer.Tick += new EventHandler(timerTick);
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();

            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            time--;
            hour = time / 3600;
            minute = (time - (3600 * hour)) / 60;
            second = time - (3600 * hour + minute * 60);
            Timer.Content = $"{hour}:{minute}:{second}";
            if (time <= 900)
            {
                Timer.Foreground = Brushes.Red;
            }
            if (time == 0)
            {
                NavigationService.Navigate(new AuthPage());
                TemporaryStorage.Time = 600;
            }
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTB.Text != "" && PasswordTB.Text != "" && LoginTB.Text != "Введите логин" && PasswordTB.Text != "Введите пароль")
            {
                //WrongTb.Visibility = Visibility.Hidden;
                try
                {
                    Models.Users user = db.Users.ToList().FirstOrDefault(x => x.UserLogin == LoginTB.Text);
                    string check_login = user.UserLogin;
                    if (LoginTB.Text == check_login)
                    {
                        if (PasswordTB.Text == user.UserPasswoed)
                        {
                            if (user.UserRole == "A")
                            {
                                NavigationService.Navigate(new AdminPage());

                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window.GetType() == typeof(MainWindow))
                                    {
                                        (window as MainWindow).Timer.Visibility = Visibility.Hidden;
                                        (window as MainWindow).AbsName.Content = "";
                                        (window as MainWindow).AbsSurn.Content = "";
                                        (window as MainWindow).AbsRole.Content = "Admin";

                                    }
                                }

                            } else if (user.UserRole == "R")
                            {
                                NavigationService.Navigate(new Laboratorian_ResearcherPage());


                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window.GetType() == typeof(MainWindow))
                                    {
                                        (window as MainWindow).Timer.Visibility = Visibility.Hidden;
                                        (window as MainWindow).AbsSurn.Content = db.Laboratorians.ToList().Find(x => x.LaboratorianUserID == user.UserID).Surname;
                                        (window as MainWindow).AbsName.Content = db.Laboratorians.ToList().Find(x => x.LaboratorianUserID == user.UserID).Name;
                                        (window as MainWindow).AbsRole.Content = "Resarcher";
                                        TemporaryStorage.Role = "Resarcher";
                                    }
                                }

                            } else if (user.UserRole == "B")
                            {
                                NavigationService.Navigate(new AccountantPage());

                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window.GetType() == typeof(MainWindow))
                                    {
                                        (window as MainWindow).Timer.Visibility = Visibility.Hidden;
                                        (window as MainWindow).AbsSurn.Content = db.Accountants.ToList().Find(x => x.AccountantUserID == user.UserID).Surname;
                                        (window as MainWindow).AbsName.Content = db.Accountants.ToList().Find(x => x.AccountantUserID == user.UserID).Name;
                                        (window as MainWindow).AbsRole.Content = "Accountants";

                                    }
                                }

                            } else if (user.UserRole == "P")
                            {
                                NavigationService.Navigate(new PatientPage());

                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window.GetType() == typeof(MainWindow))
                                    {
                                        (window as MainWindow).Timer.Visibility = Visibility.Hidden;
                                        (window as MainWindow).AbsSurn.Content = db.Patients.ToList().Find(x => x.PatientUserID == user.UserID).Surname;
                                        (window as MainWindow).AbsName.Content = db.Patients.ToList().Find(x => x.PatientUserID == user.UserID).Name;
                                        (window as MainWindow).AbsRole.Content = "Patients";

                                    }
                                }

                            } else
                            {
                                NavigationService.Navigate(new LaboratorianPage());

                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window.GetType() == typeof(MainWindow))
                                    {
                                        (window as MainWindow).Timer.Visibility = Visibility.Hidden;
                                        (window as MainWindow).AbsSurn.Content = db.Laboratorians.ToList().Find(x => x.LaboratorianUserID == user.UserID).Surname;
                                        (window as MainWindow).AbsName.Content = db.Laboratorians.ToList().Find(x => x.LaboratorianUserID == user.UserID).Name;
                                        (window as MainWindow).AbsRole.Content = "Laboratorians";
                                        TemporaryStorage.Role = "Laboratorians";

                                    }
                                }

                            }
                        }
                        else
                        {
                            //WrongTb.Visibility = Visibility.Visible;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //WrongTb.Visibility = Visibility.Visible;
                }
            }
            else
            {
             //   WrongTb.Visibility = Visibility.Visible;
            }
        }
    }
}

/*< PasswordBox
                x: Name = "PasswordPB"
                HorizontalAlignment = "Left"
                Height = "24"
                FontSize = "20"
                Background = "{x:Null}"
                BorderBrush = "#1f4e79"
                BorderThickness = "0,0,0,1"
                VerticalAlignment = "Top"
                Margin = "20,219,0,0"
                FontFamily = "Calibri"
                Width = "349"
                />*/
