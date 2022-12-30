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
    /// Interaction logic for Roulette.xaml
    /// </summary>
    public partial class Roulette : Window
    {
        Dictionary<int, string> brojevi = new Dictionary<int, string>();
        Dictionary<string, double> ulog = new Dictionary<string, double>();
        List<string> ulozeneStvari = new List<string>();
        double ulozeniNovac;
        double TrenutniChipovi;
        public Roulette()
        {
            InitializeComponent();
            TrenutnoStanje();
            DodavanjeBrojeva();
        }

        //Metoda pomoću koje dohvaćamo trenutno stanje čipova
        private void TrenutnoStanje()
        {
            Stanje.Text = ((GlavniMeni)Application.Current.MainWindow).Chipovi.Text;
            TrenutniChipovi = double.Parse(((GlavniMeni)Application.Current.MainWindow).Chipovi.Text.Replace("€", ""));
        }

        //Metode
        //Metoda pomoću koje dodajemo brojeve i njihove boje u Dictionary
        private void DodavanjeBrojeva()
        {
            Console.WriteLine("Metoda DodavanjeBrojeva.");
            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    brojevi.Add(0, "Zelena");
                }
                else if (i == 1)
                {
                    brojevi.Add(1, "Crvena");
                }
                else if (i % 2 != 0)
                {
                    brojevi.Add(i, "Crvena");
                }
                else
                {
                    brojevi.Add(i, "Crna");
                }
            }
            for (int i = 11; i < 20; i++)
            {
                if (i % 2 != 0)
                {
                    brojevi.Add(i, "Crna");
                }
                else
                {
                    brojevi.Add(i, "Crvena");
                }
            }
            for (int i = 20; i < 29; i++)
            {
                if (i % 2 != 0)
                {
                    brojevi.Add(i, "Crvena");
                }
                else
                {
                    brojevi.Add(i, "Crna");
                }
            }
            for (int i = 29; i < 37; i++)
            {
                if (i % 2 != 0)
                {
                    brojevi.Add(i, "Crna");
                }
                else
                {
                    brojevi.Add(i, "Crvena");
                }
            }
            foreach (int broj in brojevi.Keys)
            {
                Console.WriteLine(broj + " " + brojevi[broj]);
            }
            Console.WriteLine("Metoda DodavanjeBrojeva izvršena.");
        }


        private void Igraj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dugme_Ulog_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
