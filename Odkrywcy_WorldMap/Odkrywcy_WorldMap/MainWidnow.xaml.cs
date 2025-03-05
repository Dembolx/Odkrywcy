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

namespace Odkrywcy_WorldMap
{
    /// <summary>
    /// Logika interakcji dla klasy MainWidnow.xaml
    /// </summary>
    public partial class MainWidnow : Window
    {
        public MainWidnow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new WorldMap(MainFrame));
        }
    }
}
