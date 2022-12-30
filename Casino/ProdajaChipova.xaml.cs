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

namespace Casino
{
    /// <summary>
    /// Interaction logic for ProdajaChipova.xaml
    /// </summary>
    public partial class ProdajaChipova : Window
    {
        public double TrenutniChipovi;
        double prodaniChipovi;
        public ProdajaChipova()
        {
            InitializeComponent();
            TrenutnoStanje();
        }

        //Metoda pomoću koje dohvaćamo trenutno stanje čipova
        private void TrenutnoStanje()
        {
            TrenutniChipovi = double.Parse(((GlavniMeni)Application.Current.MainWindow).Chipovi.Text.Replace("€", ""));
            Console.WriteLine("Trenutno imate " + TrenutniChipovi + " čipova.");
        }

        //Događaj pomoću kojeg provjeravamo unos te da li igrač ima toliko čipova koliko hoće da proda, te samu prodaju
        private void Prodaj_Click(object sender, RoutedEventArgs e)
        {
            if (ProdaniChipovi.Text.Length < 1)
            {
                MessageBox.Show("Niste ništa unijeli.");
                return;
            }
            try
            {
                prodaniChipovi = double.Parse(ProdaniChipovi.Text);
            }
            catch
            {
                MessageBox.Show("Niste dobro unijeli broj.");
                return;
            }
            if (prodaniChipovi > TrenutniChipovi)
            {
                MessageBox.Show("Nemate toliko čipova.");
                return;
            }
            TrenutniChipovi -= prodaniChipovi;
            this.Close();
        }
    }
}
