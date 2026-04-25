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


namespace CrapsTableWPF
{
    /// <summary>
    /// Interaction logic for CRUDWindowCrapsTable.xaml
    /// </summary>
    public partial class CRUDWindowCrapsTable : Window
    {
        public string enteredTableMinimum;

        DealerWPF dealerWPF;

        public CRUDWindowCrapsTable()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void QuitBottonClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ProcBottonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
