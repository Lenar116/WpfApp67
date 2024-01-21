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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class User
        {
            public int id { get; set; }
            public string Login { get; set; }
            public string Pass { get; set; }
            public string Email { get; set; }
        }

        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Yarullin;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = log.Text;
            var pass = pass1.Password;
            var mail = Mail1.Text;

            
            if (!IsValidEmail(mail))
            {
                MessageBox.Show("Некорректный адрес электронной почты");
                return;
            }

            
            if (pass.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов");
                return;
            }

            
            if (!ContainsSpecialCharacter(pass))
            {
                MessageBox.Show("Пароль должен содержать хотя бы один специальный символ");
                return;
            }

            var context = new AppDbContext();
            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists is not null)
            {
                MessageBox.Show("Такой пользователь уже зарегистрирован");
                return;
            }

            var user = new User { Login = login, Pass = pass, Email = mail };
            context.Users.Add(user);
            context.SaveChanges();
            MessageBox.Show("Вы успешно зарегистрировались");

            Window2 taskWindow = new Window2();
            taskWindow.Show();
            this.Close();
        }

        
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

       
        private bool ContainsSpecialCharacter(string input)
        {
            return input.Any(c => !Char.IsLetterOrDigit(c));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 taskWindow = new Window2();
            taskWindow.Show();
            this.Close();
        }
    }
}
