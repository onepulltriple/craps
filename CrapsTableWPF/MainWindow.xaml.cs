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
using CrapsLibrary.Bets;

namespace CrapsTableWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CrapsTable crapsTable;

        Player currentPlayer;

        public MainWindow()
        {
            InitializeComponent();
            this.crapsTable = new(5, 5);
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

        }

        private void AddMoneyButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void PlaceBetMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement tempSender)
                return;

            betType tempBetType = Enum.Parse<betType>(tempSender.Tag.ToString());

            // where should the player live?

        }
    }
}