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

namespace CrapsTableWPF.Views
{
    /// <summary>
    /// Interaction logic for PlayerSlotView.xaml
    /// </summary>
    public partial class PlayerSlotView : UserControl
    {
        public PlayerSlotView()
        {
            InitializeComponent();
        }

        private void CreatePlayer(object sender, RoutedEventArgs e)
        {
            //should this really go here?
        }
    }
}
