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
        //Globalne varijable
        Dictionary<int, string> brojevi = new Dictionary<int, string>();
        Dictionary<string, double> ulog = new Dictionary<string, double>();
        List<string> ulozeneStvari = new List<string>();
        List<string> SvojstvaDobivenogBroja = new List<string>();
        double ulozeniNovac, trenutniChipovi;
        double dobiveniUlog = 0;
        double dobitak = 0;
        int brojUloga;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
            trenutniChipovi = double.Parse(((GlavniMeni)Application.Current.MainWindow).Chipovi.Text.Replace("€", ""));
        }

        //Metode
        //Metoda pomoću koje dodajemo brojeve i njihove boje u Dictionary
        private void DodavanjeBrojeva()
        {
            Logger.Info("Metoda DodavanjeBrojeva pokrenuta.");
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
                Logger.Info(broj + " " + brojevi[broj]);
            }
            Logger.Info("Metoda DodavanjeBrojeva izvršena.");
        }

        //Metoda pomoću koje provjeravamo na što sve igrač želi uložiti novac
        private int BrojUloga()
        {
            Logger.Info("Metoda BrojUloga pokrenuta.");
            ulozeneStvari.Clear();
            int brojUloga = 0;
            if (Brojevi.SelectedIndex > 0)
            {
                Logger.Info("Broj " + Brojevi.SelectedItem.ToString() + " je odabran.");
                ulozeneStvari.Add(Brojevi.SelectedItem.ToString());
                if (!ulog.ContainsKey(Brojevi.SelectedItem.ToString()))
                {
                    ulog.Add(Brojevi.SelectedItem.ToString(), 0);
                }
                brojUloga++;
            }
            if (Crvena.IsChecked == true)
            {
                Logger.Info("Odabrana je crvena boja.");
                ulozeneStvari.Add("Crvena");
                if (!ulog.ContainsKey("Crvena"))
                {
                    ulog.Add("Crvena", 0);
                }
                brojUloga++;
            }
            if (Crna.IsChecked == true)
            {
                Logger.Info("Odabrana je crna boja.");
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
                Logger.Info("Odabrana je zelena boja.");
                ulozeneStvari.Add("Zelena");
                if (!ulog.ContainsKey("Zelena"))
                {
                    ulog.Add("Zelena", 0);
                }
                brojUloga++;
            }
            if (Paran.IsChecked == true)
            {
                Logger.Info("Odabran je paran broj.");
                ulozeneStvari.Add("Paran");
                if (!ulog.ContainsKey("Paran"))
                {
                    ulog.Add("Paran", 0);
                }
                brojUloga++;
            }
            if (Neparan.IsChecked == true)
            {
                Logger.Info("Odabran je neparan broj.");
                ulozeneStvari.Add("Neparan");
                if (!ulog.ContainsKey("Neparan"))
                {
                    ulog.Add("Neparan", 0);
                }
                brojUloga++;
            }
            if (Low.IsChecked == true)
            {
                Logger.Info("Odabran je Low.");
                ulozeneStvari.Add("Low");
                if (!ulog.ContainsKey("Low"))
                {
                    ulog.Add("Low", 0);
                }
                brojUloga++;
            }
            if (High.IsChecked == true)
            {
                Logger.Info("Odabran je High.");
                ulozeneStvari.Add("High");
                if (!ulog.ContainsKey("High"))
                {
                    ulog.Add("High", 0);
                }
                brojUloga++;
            }
            Logger.Info("Metoda BrojUloga izvršena.");
            return brojUloga;
        }

        //Metoda kojom provjeravamo broj i pronalazimo njegova svojstva
        private void ProvjeraBroja(int broj)
        {
            Logger.Info("Metoda ProvjeraBroja pokrenuta.");
            Logger.Info("Pobjednički Broj je " + broj);
            if (broj == 0)
            {
                Logger.Info("Broj je nula.");
                SvojstvaDobivenogBroja.Add("Nula");
            }
            else if (broj == 1)
            {
                Logger.Info("Broj je neparan.");
                SvojstvaDobivenogBroja.Add("Neparan");
            }
            else if (broj % 2 == 0)
            {
                Logger.Info("Broj je paran.");
                SvojstvaDobivenogBroja.Add("Paran");
            }
            else if (broj % 2 != 0)
            {
                Logger.Info("Broj je neparan.");
                SvojstvaDobivenogBroja.Add("Neparan");
            }
            SvojstvaDobivenogBroja.Add(brojevi[broj]);
            if (broj >= 0 && broj <= 19)
            {
                Logger.Info("Broj pada u Low grupu.");
                SvojstvaDobivenogBroja.Add("Low");
            }
            else
            {
                Logger.Info("Broj pada u High grupu.");
                SvojstvaDobivenogBroja.Add("High");
            }
            Logger.Info("Metoda ProvjeraBroja izvršena.");
        }

        //Metoda pomoću koje ulažemo na sve što je igrač odabrao
        private void Ulozi(double raspodjeljeniUlog)
        {
            Logger.Info("Metoda Ulozi pokrenuta.");
            foreach (string item in ulozeneStvari)
            {
                ulog[item] += raspodjeljeniUlog;
                Logger.Info("Uloženo je na " + item + ", a ulog je " + ulog[item] + " €.");
            }
            Logger.Info("Metoda Ulozi izvršena.");
        }
        //Metoda kojom provjeravamo uloge sa svojstvima broja
        private void ProvjeraUloga(int broj)
        {
            Logger.Info("Metoda ProvjeraUloga pokrenuta.");
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
            Logger.Info("Metoda ProvjeraUloga izvršena.");
        }

        //Metoda pomoću koje izračunavano dobitak za svako pogoođeno svojstvo pobjedničkog broja
        private void ProvjeraDobitka(string svojstvo)
        {
            Logger.Info("Metoda ProvjeraDobitka pokrenuta.");
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
            Logger.Info("Metoda ProvjeraDobitka izvršena.");
        }

        //Metoda pomoću koje čistimo sve varijable za ulaganje nakon završetka igre
        private void Ocisti()
        {
            Logger.Info("Metoda Ocisti pokrenuta.");
            dobiveniUlog = 0;
            dobitak = 0;
            ulog.Clear();
            ulozeneStvari.Clear();
            SvojstvaDobivenogBroja.Clear();
            Logger.Info("Metoda Ocisti izvršena.");
        }

        private void Dugme_Ulog_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Dugme Ulog pritisnuto.");
            if (Ulog.Text.Length < 1)
            {
                Logger.Info("Korisnik nije ništa uložio.");
                MessageBox.Show("Niste ništa uložili.");
                return;
            }
            try
            {
                ulozeniNovac = double.Parse(Ulog.Text);
            }
            catch (Exception)
            {
                Logger.Info("Korisnik nije dobro unio novac.");
                MessageBox.Show("Niste dobro unijeli novac.");
                return;
            }
            if (ulozeniNovac == 0)
            {
                Logger.Info("Korisnik je pokušao uložiti 0.");
                MessageBox.Show("Ulog ne može biti 0.");
                return;
            }
            if (ulozeniNovac > trenutniChipovi)
            {
                Logger.Info("Korisnik nema dovoljno čipova.");
                MessageBox.Show("Nemate toliko chipova.");
                return;
            }
            brojUloga = BrojUloga();
            if (brojUloga > 0)
            {
                double raspodjeljeniUlog = ulozeniNovac / brojUloga;
                trenutniChipovi -= ulozeniNovac;
                Stanje.Text = trenutniChipovi.ToString() + "€";
                Logger.Info("Raspodjeljeni ulog: " + raspodjeljeniUlog.ToString());
                Ulozi(raspodjeljeniUlog);
            }
        }

        private void Igraj_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Dugme Igraj pokrenuto.");
            PobjednickiBroj.Inlines.Clear();
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
