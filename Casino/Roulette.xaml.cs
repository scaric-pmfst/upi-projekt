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
        List<string> SvojstvaDobivenogBroja = new List<string>();
        double ulozeniNovac, trenutniChipovi;
        double dobiveniUlog = 0;
        double dobitak = 0;
        int brojUloga;
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
            Brojevi.Items.Add("");
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
                Brojevi.Items.Add(i);
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
                Brojevi.Items.Add(i);
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
                Brojevi.Items.Add(i);
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
                Brojevi.Items.Add(i);
            }
            foreach (int broj in brojevi.Keys)
            {
                Console.WriteLine(broj + " " + brojevi[broj]);
            }
            Console.WriteLine("Metoda DodavanjeBrojeva izvršena.");
        }

        //Metoda pomoću koje provjeravamo na što sve igrač želi uložiti novac
        private int BrojUloga()
        {
            Console.WriteLine("Metoda ulogNovca.");
            ulozeneStvari.Clear();
            int brojUloga = 0;
            if (Brojevi.SelectedIndex > 0)
            {
                ulozeneStvari.Add(Brojevi.SelectedItem.ToString());
                if (!ulog.ContainsKey(Brojevi.SelectedItem.ToString()))
                {
                    ulog.Add(Brojevi.SelectedItem.ToString(), 0);
                }
                brojUloga++;
            }
            if (Crvena.IsChecked == true)
            {
                ulozeneStvari.Add("Crvena");
                if (!ulog.ContainsKey("Crvena"))
                {
                    ulog.Add("Crvena", 0);
                }
                brojUloga++;
            }
            if (Crna.IsChecked == true)
            {
                if (!ulozeneStvari.Contains("Crna"))
                {
                    ulozeneStvari.Add("Crna");
                    if (!ulog.ContainsKey("Crna"))
                    {
                        ulog.Add("Crna", 0);
                    }
                }
                brojUloga++;
            }
            if (Zelena.IsChecked == true)
            {
                ulozeneStvari.Add("Zelena");
                if (!ulog.ContainsKey("Zelena"))
                {
                    ulog.Add("Zelena", 0);
                }
                brojUloga++;
            }
            if (Paran.IsChecked == true)
            {
                ulozeneStvari.Add("Paran");
                if (!ulog.ContainsKey("Paran"))
                {
                    ulog.Add("Paran", 0);
                }
                brojUloga++;
            }
            if (Neparan.IsChecked == true)
            {
                ulozeneStvari.Add("Neparan");
                if (!ulog.ContainsKey("Neparan"))
                {
                    ulog.Add("Neparan", 0);
                }
                brojUloga++;
            }
            if (Low.IsChecked == true)
            {
                ulozeneStvari.Add("Low");
                if (!ulog.ContainsKey("Low"))
                {
                    ulog.Add("Low", 0);
                }
                brojUloga++;
            }
            if (High.IsChecked == true)
            {
                ulozeneStvari.Add("High");
                if (!ulog.ContainsKey("High"))
                {
                    ulog.Add("High", 0);
                }
                brojUloga++;
            }
            Console.WriteLine("Metoda ulogNovca izvršena.");
            return brojUloga;
        }
        //Metoda kojom provjeravamo broj i pronalazimo njegova svojstva
        private void ProvjeraBroja(int broj)
        {
            if (broj == 0)
            {
                SvojstvaDobivenogBroja.Add("Nula");
            }
            else if (broj == 1)
            {
                SvojstvaDobivenogBroja.Add("Neparan");
            }
            else if (broj % 2 == 0)
            {
                SvojstvaDobivenogBroja.Add("Paran");
            }
            else if (broj % 2 != 0)
            {
                SvojstvaDobivenogBroja.Add("Neparan");
            }
            SvojstvaDobivenogBroja.Add(brojevi[broj]);
            if (broj >= 0 && broj <= 19)
            {
                SvojstvaDobivenogBroja.Add("Low");
            }
            else
            {
                SvojstvaDobivenogBroja.Add("High");
            }
            Console.WriteLine("Pobjednicki Broj je : " + broj);
            foreach (string item in SvojstvaDobivenogBroja)
            {
                Console.WriteLine(item);
            }
        }
        //Metoda pomoću koje ulažemo na sve što je igrač odabrao
        private void Ulozi(double raspodjeljeniUlog)
        {
            foreach (string item in ulozeneStvari)
            {
                ulog[item] += raspodjeljeniUlog;
            }

            foreach (string item in ulog.Keys)
            {
                Console.WriteLine(item + ":" + ulog[item]);
            }
        }
        //Metoda kojom provjeravamo uloge sa svojstvima broja
        private void ProvjeraUloga(int broj)
        {
            foreach (string svojstvo in SvojstvaDobivenogBroja)
            {
                if (ulog.ContainsKey(svojstvo))
                {
                    dobiveniUlog += ulog[svojstvo];
                    ProvjeraDobitka(svojstvo);
                }
            }
            trenutniChipovi = Math.Round(trenutniChipovi + dobiveniUlog + dobitak, 3);
            Stanje.Text = trenutniChipovi.ToString() + "€";
        }

        //Metoda pomoću koje izračunavano dobitak za svako pogoođeno svojstvo pobjedničkog broja
        private void ProvjeraDobitka(string svojstvo)
        {
            switch (svojstvo)
            {
                case "Paran":
                case "Neparan":
                case "Low":
                case "High":
                    dobitak += ulog[svojstvo] * 0.5;
                    break;
                case "Crvena":
                case "Crna":
                    dobitak += ulog[svojstvo] * 0.33;
                    break;
                case "Zelena":
                    dobitak += ulog[svojstvo] * 0.66;
                    break;
                default:
                    dobitak += ulog[svojstvo] * 0.27;
                    break;
            }
        }

        //Metoda pomoću koje čistimo sve varijable za ulaganje nakon završetka igre
        private void Ocisti()
        {
            dobiveniUlog = 0;
            dobitak = 0;
            ulog.Clear();
            ulozeneStvari.Clear();
            SvojstvaDobivenogBroja.Clear();
            PobjednickiBroj.Inlines.Clear();
        }

        private void Dugme_Ulog_Click(object sender, RoutedEventArgs e)
        {
            if (Ulog.Text.Length < 1)
            {
                MessageBox.Show("Niste ništa uložili.");
                return;
            }
            try
            {
                ulozeniNovac = double.Parse(Ulog.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Niste dobro unijeli novac.");
                return;
            }
            if (ulozeniNovac == 0)
            {
                MessageBox.Show("Ulog ne može biti 0.");
                return;
            }
            if (ulozeniNovac > trenutniChipovi)
            {
                MessageBox.Show("Nemate toliko chipova.");
                return;
            }
            brojUloga = BrojUloga();
            if (brojUloga > 0)
            {
                double raspodjeljeniUlog = ulozeniNovac / brojUloga;
                trenutniChipovi -= ulozeniNovac;
                Stanje.Text = trenutniChipovi.ToString() + "€";
                Console.WriteLine("Raspodjeljeni Ulog: " + raspodjeljeniUlog.ToString());
                Ulozi(raspodjeljeniUlog);
            }
        }
        private void Igraj_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int broj = r.Next(0, 37);
            switch (brojevi[broj])
            {
                default:
                    PobjednickiBroj.Inlines.Add(new Run(broj.ToString()) { Foreground = Brushes.Black });
                    break;
                case "Crvena":
                    PobjednickiBroj.Inlines.Add(new Run(broj.ToString()) { Foreground = Brushes.Red });
                    break;
                case "Zelena":
                    PobjednickiBroj.Inlines.Add(new Run(broj.ToString()) { Foreground = Brushes.Green });
                    break;
            }
            ProvjeraBroja(broj);
            ProvjeraUloga(broj);
            Ocisti();
        }
    }
}
