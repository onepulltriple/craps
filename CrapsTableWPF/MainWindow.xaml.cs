using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Services;
using CrapsTableWPF.ViewModels;

namespace CrapsTableWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(CrapsTableService service)
        {
            InitializeComponent();
            DataContext = new CrapsTableViewModel(service.crapsTable!);
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

            //betType tempBetType = Enum.Parse<betType>(tempSender.Tag.ToString());

            // where should the player live?

        }
    }
}