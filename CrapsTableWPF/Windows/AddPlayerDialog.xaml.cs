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

namespace CrapsTableWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddPlayerDialog.xaml
    /// </summary>
    public partial class AddPlayerDialog : Window
    {
        public AddPlayerDialog()
        {
            InitializeComponent();
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
