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
    /// Interaction logic for KupiChipove.xaml
    /// </summary>
    public partial class KupiChipove : Window
    {
        public double TrenutniChipovi;
        double kupljeniChipovi;
        public KupiChipove()
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

        //Događaj pomoću kojeg provjeravamo unos te isplatu čipova ako je unos dobar
        private void Kupi_Click(object sender, RoutedEventArgs e)
        {
            if (KupljeniChipovi.Text.Length < 1)
            {
                MessageBox.Show("Niste nista unijeli.");
                return;
            }
            try
            {
                kupljeniChipovi = double.Parse(KupljeniChipovi.Text);
            }
            catch
            {
                MessageBox.Show("Niste dobro unijeli broj.");
                return;
            }
            TrenutniChipovi += kupljeniChipovi;
            this.Close();
        }
    }
}
