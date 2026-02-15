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
            this.Table = new(5, 5);
            DataContext = this;
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Region_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Region_Drop(object sender, DragEventArgs e)
        {

        }

        private void Region_Click(object sender, MouseButtonEventArgs e)
        {
            ;
        }

        private void NewPlayerButtonClick(object sender, RoutedEventArgs e)
        {
            Table.NewPlayer(PlayerNameInputTextBox.Text);
        }

        private void AddMoneyButtonClick(object sender, RoutedEventArgs e)
        {
            Table.players.First().purse += uint.Parse(MoneyInputTextBox.Text);
        }
    }
}