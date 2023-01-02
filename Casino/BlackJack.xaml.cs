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
        int JakostRukeIgraca, ulozeniNovac;
        bool IgrajBlackJack;
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
            Console.WriteLine("Metoda OcistiRuke");
            RukaProtivnika.Clear();
            RukaIgraca.Clear();
            ProtivnikovaRuka.Text = "";
            IgracevaRuka.Text = "";
            JakostRukeIgraca = 0;
            Console.WriteLine("Metoda OcistiRuke izvrsena");
        }

        //Metoda pomoću koje pravimo novi dek
        private void NapraviDeck()
        {
            Console.WriteLine("NapraviDeck Metoda");
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
            Console.WriteLine("Metoda PocetnaRuka");
            for (int i = 0; i < 3; i++)
            {
                List<string> tempDeck = Deck;
                string izvucenaKarta = tempDeck[r.Next(0, tempDeck.Count)];
                tempDeck.Remove(izvucenaKarta);
                if (i != 1)
                {
                    Console.WriteLine("Igraceva Karta: " + izvucenaKarta);
                    IgracevaRuka.Text += izvucenaKarta + "|";
                    RukaIgraca.Add(izvucenaKarta);
                }
                else
                {
                    Console.WriteLine("Protivnikova karta " + izvucenaKarta);
                    ProtivnikovaRuka.Text += izvucenaKarta + "|";
                    RukaProtivnika.Add(izvucenaKarta);
                }
                Deck = tempDeck;
                Console.WriteLine("Trenutno je u deku " + Deck.Count + " karata.");
            }
            Console.WriteLine("Metoda PocetnaRuka izvrsena.");
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
            }
            else
            {
                MessageBox.Show("Igra je u tijeku");
                return;
            }
        }
    }
    
}
