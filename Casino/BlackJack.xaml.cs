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
    /// Interaction logic for BlackJack.xaml
    /// </summary>
    public partial class BlackJack : Window
    {
        //Globalne varijable
        Karte karte = new Karte();
        Random r = new Random();
        List<string> Deck = new List<string>();
        List<string> RukaIgraca = new List<string>();
        List<string> RukaProtivnika = new List<string>();
        int JakostRukeIgraca, JakostRukeProtivnika;
        double ulozeniNovac, trenutniChipovi;
        bool IgrajBlackJack = false;
        bool doubleDown = false;
        bool IgracImaAsa, ProtivnikImaAsa;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public BlackJack()
        {
            InitializeComponent();
            TrenutnoStanje();
        }

        //Metoda pomoću koje dohvaćamo trenutno stanje čipova
        private void TrenutnoStanje()
        {
            Stanje.Text = ((GlavniMeni)Application.Current.MainWindow).Chipovi.Text;
            trenutniChipovi = double.Parse(((GlavniMeni)Application.Current.MainWindow).Chipovi.Text.Replace("€", ""));
        }
        //Metode

        //Metoda pomoću koje cistimo ruke igrača i protivnika
        private void OcistiRuke()
        {
            Logger.Info("Metoda OcistiRuke pokrenuta.");
            RukaProtivnika.Clear();
            RukaIgraca.Clear();
            ProtivnikovaRuka.Inlines.Clear();
            IgracevaRuka.Inlines.Clear();
            JakostRukeIgraca = 0;
            IgracImaAsa = false;
            ProtivnikImaAsa = false;
            doubleDown = false;
            Logger.Info("Metoda OcistiRuke izvršena.");
        }

        //Metoda pomoću koje pravimo novi dek
        private void NapraviDeck()
        {
            Logger.Info("Metoda NapraviDeck pokrenuta.");
            Deck.Clear();
            foreach (char boja in karte.Boje)
            {
                foreach (string broj in karte.Brojevi)
                {
                    Logger.Info(broj + boja);
                    Deck.Add(broj + boja);
                }
            }
            Logger.Info("Metoda NapraviDeck izvršena.");
        }

        //Metoda pomoću koje dijelimo početne karte igraču i protivniku
        private void PocetnaRuka()
        {
            Logger.Info("Metoda PocetnaRuka pokrenuta.");
            for (int i = 0; i < 3; i++)
            {
                List<string> tempDeck = Deck;
                string izvucenaKarta = tempDeck[r.Next(0, tempDeck.Count)];
                tempDeck.Remove(izvucenaKarta);
                if (i != 1)
                {
                    Logger.Info("Igračeva karta: " + izvucenaKarta);
                    if (izvucenaKarta.EndsWith("\u2665") || izvucenaKarta.EndsWith("\u2666"))
                    {
                        IgracevaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Red });
                    }
                    else
                    {
                        IgracevaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Black });
                    }
                    RukaIgraca.Add(izvucenaKarta);
                }
                else
                {
                    Logger.Info("Protivnikova karta: " + izvucenaKarta);
                    if (izvucenaKarta.EndsWith("\u2665") || izvucenaKarta.EndsWith("\u2666"))
                    {
                        ProtivnikovaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Red });
                    }
                    else
                    {
                        ProtivnikovaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Black });
                    }
                    RukaProtivnika.Add(izvucenaKarta);
                }
                Deck = tempDeck;
                Logger.Info("Trenutno je u deku " + Deck.Count + " karata.");
            }
            Logger.Info("Metoda PocetnaRuka izvršena.");
        }

        //Metoda pomoću koje vučemo kartu
        private void VuciKartu(string osoba)
        {
            Logger.Info("Metoda VuciKartu pokrenuta.");
            List<string> tempDeck = Deck;
            string izvucenaKarta = tempDeck[r.Next(0, tempDeck.Count)];
            tempDeck.Remove(izvucenaKarta);
            if (osoba == "Igrac")
            {
                Logger.Info("Igračeva karta: " + izvucenaKarta);
                if (izvucenaKarta.EndsWith("\u2665") || izvucenaKarta.EndsWith("\u2666"))
                {
                    IgracevaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Red });
                }
                else
                {
                    IgracevaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Black });
                }
                RukaIgraca.Add(izvucenaKarta);
            }
            else if (osoba == "Protivnik")
            {
                Logger.Info("Protivnikova karta: " + izvucenaKarta);
                if (izvucenaKarta.EndsWith("\u2665") || izvucenaKarta.EndsWith("\u2666"))
                {
                    ProtivnikovaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Red });
                }
                else
                {
                    ProtivnikovaRuka.Inlines.Add(new Run(izvucenaKarta) { Foreground = Brushes.Black });
                }
                RukaProtivnika.Add(izvucenaKarta);
            }
            Deck = tempDeck;
            Logger.Info("Trenutno je u deku " + Deck.Count + " karata.");
            Logger.Info("Metoda VuciKartu izvršena.");
        }

        //Metoda pomoću koje brojimo jakost ruke igrača i protivnika
        private void BrojacRuke(string osoba)
        {
            Logger.Info("Metoda BrojacRuke pokrenuta.");
            int temp = 0;
            if (osoba == "Igrac")
            {
                Logger.Info("Brojanje ruke igrača.");
                foreach (string karta in RukaIgraca)
                {
                    switch (karta)
                    {
                        default: //Ako je karta 2-9
                            char[] tempArray = karta.ToCharArray();
                            temp = temp + int.Parse(tempArray[0].ToString());
                            break;
                        case string As when karta.StartsWith("A"):
                            
                            if (temp > 10)
                            {
                                temp = temp + 1;
                            }
                            else
                            {
                                temp = temp + 11;
                                IgracImaAsa = true;
                            }
                            break;
                        case string Deset when karta.StartsWith("10"):
                        case string Jukac when karta.StartsWith("J"):
                        case string Kraljica when karta.StartsWith("Q"):
                        case string Kralj when karta.StartsWith("K"):
                            temp = temp + 10;
                            break;
                    }
                }
                JakostRukeIgraca = temp;
                Logger.Info("Karte igrača: " + string.Join(", ", RukaIgraca));
                Logger.Info("Jakost ruke nakon brojanja: " + JakostRukeIgraca);
            }
            else if (osoba == "Protivnik")
            {
                Logger.Info("Brojanje ruke protivnika.");
                foreach (string karta in RukaProtivnika)
                {
                    switch (karta)
                    {
                        default: //Ako je karta 2-9
                            char[] tempArray = karta.ToCharArray();
                            temp = temp + int.Parse(tempArray[0].ToString());
                            break;
                        case string As when karta.StartsWith("A"):
                            
                            if (temp > 10)
                            {
                                temp = temp + 1;
                            }
                            else
                            {
                                temp = temp + 11;
                                ProtivnikImaAsa = true;
                            }
                            break;
                        case string Deset when karta.StartsWith("10"):
                        case string Jukac when karta.StartsWith("J"):
                        case string Kraljica when karta.StartsWith("Q"):
                        case string Kralj when karta.StartsWith("K"):
                            temp = temp + 10;
                            break;
                    }
                }
                JakostRukeProtivnika = temp;
                Logger.Info("Karte protivnika: " + string.Join(",", RukaProtivnika));
                Logger.Info("Jakost ruke nakon brojanja: " + JakostRukeProtivnika);
            }
        }

        //Metoda pomoću koje proveravamo jakost ruke igrača, ako je veća od 21, igrač je izgubio
        private void ProvjeraRukeIgraca()
        {
            Logger.Info("Metoda ProvjeraRukeIgraca pokrenuta.");
            if (JakostRukeIgraca > 21)
            {
                if (!IgracImaAsa)
                {
                    Logger.Info("Korisnik je izgubio.");
                    MessageBox.Show("Izgubio si. Više sreće drugi put.");
                    IgrajBlackJack = false;
                }
                else
                {
                    if ((JakostRukeIgraca - 10) > 21)
                    {
                        Logger.Info("Korisnik je izgubio.");
                        MessageBox.Show("Izgubio si. Više sreće drugi put.");
                        IgrajBlackJack = false;
                    }
                }
            }
            else if (JakostRukeIgraca == 21 && RukaIgraca.Count == 2)
            {
                Logger.Info("Korisnik je dobio BlackJack.");
                MessageBox.Show("BlackJack. Dobio si.");
                trenutniChipovi = trenutniChipovi + ulozeniNovac + ulozeniNovac * 1.7;
                Stanje.Text = trenutniChipovi.ToString() + "€";
                IgrajBlackJack = false;
            }
            Logger.Info("Metoda ProvjeraJakostiRuke izvršena.");
        }

        //Metoda kojom program kontrolira protivnika
        private void ProtivnikIgra()
        {
            Logger.Info("Metoda ProtivnikIgra pokrenuta.");
            while (true)
            {
                VuciKartu("Protivnik");
                BrojacRuke("Protivnik");
                if (JakostRukeProtivnika >= JakostRukeIgraca)
                {
                    break;
                }
            }
            Logger.Info("Metoda ProtivnikIgra izvršena.");
            ProvjeraRuku();
        }

        //Metoda kojom provjeravamo protivnikovu ruku i uspoređujemo jakosti obje ruke
        private void ProvjeraRuku()
        {
            Logger.Info("Metoda ProvjeraRuku pokrenuta.");
            if (JakostRukeProtivnika > 21)
            {
                if (ProtivnikImaAsa)
                {
                    if ((JakostRukeProtivnika - 10) > 21)
                    {
                        Logger.Info("Korisnik je dobio.");
                        MessageBox.Show("Dobili ste.");
                        if (doubleDown)
                        {
                            trenutniChipovi = trenutniChipovi + ulozeniNovac * 4;
                        }
                        else
                        {
                            trenutniChipovi = trenutniChipovi + ulozeniNovac + ulozeniNovac * 1.5;
                        }
                        Stanje.Text = trenutniChipovi.ToString() + "€";
                        IgrajBlackJack = false;
                    }
                    else
                    {
                        if ((JakostRukeProtivnika - 10) > JakostRukeIgraca)
                        {
                            Logger.Info("Korisnik je izgubio.");
                            MessageBox.Show("Izgubio si. Više sreće drugi put.");
                            IgrajBlackJack = false;
                        }
                        else
                        {
                            Logger.Info("Korisnik je dobio.");
                            MessageBox.Show("Dobili ste.");
                            if (doubleDown)
                            {
                                trenutniChipovi = trenutniChipovi + ulozeniNovac * 4;
                            }
                            else
                            {
                                trenutniChipovi = trenutniChipovi + ulozeniNovac + ulozeniNovac * 1.5;
                            }
                            Stanje.Text = trenutniChipovi.ToString() + "€";
                            IgrajBlackJack = false;
                        }
                    }
                }
                else
                {
                    Logger.Info("Korisnik je dobio.");
                    MessageBox.Show("Dobili ste.");
                    if (doubleDown)
                    {
                        trenutniChipovi = trenutniChipovi + ulozeniNovac * 4;
                    }
                    else
                    {
                        trenutniChipovi = trenutniChipovi + ulozeniNovac + ulozeniNovac * 1.5;
                    }
                    Stanje.Text = trenutniChipovi.ToString() + "€";
                    IgrajBlackJack = false;
                }
            }
            else
            {
                if (JakostRukeProtivnika > JakostRukeIgraca)
                {
                    Logger.Info("Korisnik je izgubio.");
                    MessageBox.Show("Izgubio si. Više sreće drugi put.");
                    IgrajBlackJack = false;
                }
                else if (JakostRukeProtivnika == JakostRukeIgraca)
                {
                    MessageBox.Show("Neriješeno.");
                    if (doubleDown)
                    {
                        trenutniChipovi += ulozeniNovac * 2;
                    }
                    else
                    {
                        trenutniChipovi += ulozeniNovac;
                    }
                    Stanje.Text = trenutniChipovi.ToString() + "€";
                    IgrajBlackJack = false;
                }
                else
                {
                    Logger.Info("Korisnik je dobio.");
                    MessageBox.Show("Dobili ste.");
                    if (doubleDown)
                    {
                        trenutniChipovi = trenutniChipovi + ulozeniNovac * 4;
                    }
                    else
                    {
                        trenutniChipovi = trenutniChipovi + ulozeniNovac + ulozeniNovac * 1.5;
                    }
                    Stanje.Text = trenutniChipovi.ToString() + "€";
                    IgrajBlackJack = false;
                }
            }
            Logger.Info("Metoda ProvjeraRuku izvršena.");
        }

        //Događaji
        private void Igraj_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Dugme Igraj pokrenuta.");
            if (!IgrajBlackJack)
            {
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
                trenutniChipovi -= ulozeniNovac;
                Stanje.Text = trenutniChipovi.ToString() + "€";
                OcistiRuke();
                NapraviDeck();
                PocetnaRuka();
                IgrajBlackJack = true;
                ProvjeraRukeIgraca();
            }
            else
            {
                MessageBox.Show("Igra je u tijeku");
                return;
            }
            Logger.Info("Trenutni ulog: " + ulozeniNovac);
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Dugme Hit stisnuto.");
            if (IgrajBlackJack)
            {
                VuciKartu("Igrac");
                BrojacRuke("Igrac");
                ProvjeraRukeIgraca();
            }
            else
            {
                MessageBox.Show("Igra je gotova. Stisnite na dugme Započni Igru da pokrenete novu igru");
                return;
            }
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Dugme Stand stisnuto.");
            if (IgrajBlackJack)
            {
                doubleDown = true;
                trenutniChipovi -= ulozeniNovac;
                Stanje.Text = trenutniChipovi.ToString() + "€";
                BrojacRuke("Igrac");
                ProvjeraRukeIgraca();
                ProtivnikIgra();
                if (IgrajBlackJack)
                {
                    ProtivnikIgra();
                }
            }
            else
            {
                MessageBox.Show("Igra je gotova. Stisnite na dugme Započni Igru da pokrenete novu igru");
                return;
            }

        }

        private void DoubleDown_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Dugme DoubleDown stisnuto.");
            if (IgrajBlackJack)
            {
                VuciKartu("Igrac");
                BrojacRuke("Igrac");
                ProvjeraRukeIgraca();
                if (IgrajBlackJack)
                {
                    ProtivnikIgra();
                }
            }
            else
            {
                MessageBox.Show("Igra je gotova. Stisnite na dugme Započni Igru da pokrenete novu igru");
                return;
            }
        }
    }
    
}
