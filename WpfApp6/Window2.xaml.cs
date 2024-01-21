using Microsoft.EntityFrameworkCore;
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
using static WpfApp6.MainWindow;

namespace WpfApp6
{
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = logTB.Text;
            var password = passwordBox.Password;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => (x.Login == login || x.Email == login));

            if (user is null)
            {
                MessageBox.Show("Неправильный логин или пароль!");
                return;
            }

            
            if (user.Pass != password)
            {
                MessageBox.Show("Неправильный логин или пароль!");
                return;
            }

            MessageBox.Show("Вы успешно вошли в аккаунт!");
            Window1 taskWindow = new Window1();
            taskWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow taskWindow = new MainWindow();
            taskWindow.Show();
            this.Close();
        }
    }
}
