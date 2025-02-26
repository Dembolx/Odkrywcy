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
    public partial class Kontynent_Page : Window
    {
        string Nazwa;

        private List<string> Slajd = new List<string>();

        private List<string> Film_do_Slajdu = new List<string>();
        public Kontynent_Page(string nazwa)
        {
            InitializeComponent();
            this.Title = nazwa; // Poprawne ustawienie tytułu okna
            this.Nazwa = nazwa;
        }
    }
}
