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
        int JakostRukeIgraca, JakostRukeProtivnika, ulozeniNovac;
        bool IgrajBlackJack = false;
        bool IgracImaAsa, ProtivnikImaAsa;
        double TrenutniChipovi;


        public BlackJack()
        {
            InitializeComponent();
            TrenutnoStanje();
        }

        //Metoda pomoću koje dohvaćamo trenutno stanje čipova
        private void TrenutnoStanje()
        {
            Stanje.Text = ((GlavniMeni)Application.Current.MainWindow).Chipovi.Text;
            TrenutniChipovi = double.Parse(((GlavniMeni)Application.Current.MainWindow).Chipovi.Text.Replace("€", ""));
        }
        //Metode

        //Metoda pomoću koje cistimo ruke igrača i protivnika
        private void OcistiRuke()
        {
            
            Console.WriteLine("Metoda OcistiRuke.");
            RukaProtivnika.Clear();
            RukaIgraca.Clear();
            ProtivnikovaRuka.Inlines.Clear();
            IgracevaRuka.Inlines.Clear();
            JakostRukeIgraca = 0;
            IgracImaAsa = false;
            ProtivnikImaAsa = false;
            Console.WriteLine("Metoda OcistiRuke izvrsena");
        }

        //Metoda pomoću koje pravimo novi dek
        private void NapraviDeck()
        {
            Console.WriteLine("NapraviDeck Metoda.");
            Deck.Clear();
            foreach (char boja in karte.Boje)
            {
                foreach (string broj in karte.Brojevi)
                {
                    Console.WriteLine(broj + boja);
                    Deck.Add(broj + boja);
                }
            }
            Console.WriteLine("Metoda NapraviDeck izvrsena.");
        }

        //Metoda pomoću koje dijelimo početne karte igraču i protivniku
        private void PocetnaRuka()
        {
            Console.WriteLine("Metoda PocetnaRuka.");
            for (int i = 0; i < 3; i++)
            {
                List<string> tempDeck = Deck;
                string izvucenaKarta = tempDeck[r.Next(0, tempDeck.Count)];
                tempDeck.Remove(izvucenaKarta);
                if (i != 1)
                {
                    Console.WriteLine("Igraceva Karta: " + izvucenaKarta);
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
                    Console.WriteLine("Protivnikova karta " + izvucenaKarta);
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
                Console.WriteLine("Trenutno je u deku " + Deck.Count + " karata.");
            }
            Console.WriteLine("Metoda PocetnaRuka izvrsena.");
        }

        //Metoda pomoću koje vučemo kartu
        private void VuciKartu(string osoba)
        {
            Console.WriteLine("Metoda VuciKartu.");
            List<string> tempDeck = Deck;
            string izvucenaKarta = tempDeck[r.Next(0, tempDeck.Count)];
            tempDeck.Remove(izvucenaKarta);
            if (osoba == "Igrac")
            {
                Console.WriteLine("Igraceva Karta: " + izvucenaKarta);
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
                Console.WriteLine("Protivnikova karta " + izvucenaKarta);
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
            Console.WriteLine("Trenutno je u deku " + Deck.Count + " karata.");
            Console.WriteLine("Metoda VuciKartu izvrsena.");
        }

        //Metoda pomoću koje brojimo jakost ruke igrača i protivnika
        private void BrojacRuke(string osoba)
        {
            Console.WriteLine("BrojacRuke.");
            int temp = 0;
            if (osoba == "Igrac")
            {
                Console.WriteLine("Brojanje ruke igrača.");
                foreach (string karta in RukaIgraca)
                {
                    Console.WriteLine("Karta: " + karta);
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
                    Console.WriteLine("Trenutna jakost ruke: " + temp);
                }
                JakostRukeIgraca = temp;
                Console.WriteLine("Jakost ruke nakon brojanja: " + JakostRukeIgraca);
            }
            else if (osoba == "Protivnik")
            {
                Console.WriteLine("Brojanje ruke protivnika");
                foreach (string karta in RukaProtivnika)
                {
                    Console.WriteLine("Karta: " + karta);
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
                    Console.WriteLine("Trenutna jakost ruke: " + temp);
                }
                JakostRukeProtivnika = temp;
                Console.WriteLine("Jakost ruke nakon brojanja: " + JakostRukeProtivnika);
            }
        }

        //Metoda pomoću koje proveravamo jakost ruke igrača, ako je veća od 21, igrač je izgubio
        //Metoda pomoću koje proveravamo jakost ruke igrača
        private void ProvjeraRukeIgraca()
        {
            if (JakostRukeIgraca > 21)
            {
                if (!IgracImaAsa)
                {
                    MessageBox.Show("Izgubio si. Više sreće drugi put.");
                    IgrajBlackJack = false;
                }
                else
                {
                    if ((JakostRukeIgraca - 10) > 21)
                    {
                        MessageBox.Show("Izgubio si. Više sreće drugi put.");
                        IgrajBlackJack = false;
                    }
                }
            }
            else if (JakostRukeIgraca == 21 && RukaIgraca.Count == 2)
            {
                MessageBox.Show("BlackJack. Dobio si.");
                IgrajBlackJack = false;
            }
        }

        //Metoda kojom program kontrolira protivnika
        private void ProtivnikIgra()
        {
            while (true)
            {
                VuciKartu("Protivnik");
                BrojacRuke("Protivnik");
                if (JakostRukeProtivnika >= JakostRukeIgraca)
                {
                    break;
                }
            }
            ProvjeraRuku();
        }

        //Metoda kojom provjeravamo protivnikovu ruku i uspoređujemo jakosti obje ruke
        private void ProvjeraRuku()
        {
            if (JakostRukeProtivnika > 21)
            {
                if (ProtivnikImaAsa)
                {
                    if ((JakostRukeProtivnika - 10) > 21)
                    {
                        MessageBox.Show("Dobili ste.");
                        IgrajBlackJack = false;
                    }
                    else
                    {
                        if ((JakostRukeProtivnika - 10) > JakostRukeIgraca)
                        {
                            MessageBox.Show("Izgubio si. Više sreće drugi put.");
                            IgrajBlackJack = false;
                        }
                        else
                        {
                            MessageBox.Show("Dobili ste.");
                            IgrajBlackJack = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Dobili ste.");
                    IgrajBlackJack = false;
                }
            }
            else
            {
                if (JakostRukeProtivnika > JakostRukeIgraca)
                {
                    MessageBox.Show("Izgubio si. Više sreće drugi put.");
                    IgrajBlackJack = false;
                }
                else if (JakostRukeProtivnika == JakostRukeIgraca)
                {
                    MessageBox.Show("Neriješeno.");
                    IgrajBlackJack = false;
                }
                else
                {
                    MessageBox.Show("Dobili ste.");
                    IgrajBlackJack = false;
                }
            }
        }

        //Događaji
        private void Igraj_Click(object sender, RoutedEventArgs e)
        {
            if (!IgrajBlackJack)
            {
                if (Ulog.Text.Length < 1)
                {
                    MessageBox.Show("Niste ništa uložili.");
                    return;
                }
                try
                {
                    ulozeniNovac = int.Parse(Ulog.Text);
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
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
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
            if (IgrajBlackJack)
            {
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
