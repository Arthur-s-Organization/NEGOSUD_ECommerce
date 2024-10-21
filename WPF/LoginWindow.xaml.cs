using System.Windows;

namespace WPF
{
    public partial class LoginWindow : Window
    {
        private const string correctPassword = "admin";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == correctPassword)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBox.Clear(); 
            }
        }
    }
}
