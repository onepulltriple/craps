using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrapsLibrary;

namespace CrapsTableWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CrapsTable Table;

        public MainWindow()
        {
            InitializeComponent();
            Table = new(5, 5);
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrapsTable.betFactory.CreateBet(playerBetType: betType.Aces);
            Chase.Text = betType.Aces.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}