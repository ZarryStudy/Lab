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
    /// Логика взаимодействия для Laboratorian_ResearcherPage.xaml
    /// </summary>
    public partial class Laboratorian_ResearcherPage : Page
    {
        int time;
        int hour, minute, second;
        public Laboratorian_ResearcherPage()
        {
            InitializeComponent();
            time = 9000;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

            hour = time / 3600;
            minute = (time - (3600 * hour)) / 60;
            second = time - (3600 * hour + minute * 60);
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
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
                TemporaryStorage.Time = 600;
                NavigationService.Navigate(new AuthPage());
            }
        }
    }
}
