﻿using System;
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
using System.IO;

namespace Casino
{
    /// <summary>
    /// Interaction logic for GlavniMeni.xaml
    /// </summary>
    public partial class GlavniMeni : Window
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public GlavniMeni()
        {
            InitializeComponent();
            OcistiLog();
        }

        //Metoda pomoću koje čistimo log nakon što datoteka naraste previše
        private void OcistiLog()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Log.txt"))
            {
                FileInfo Log = new FileInfo(Directory.GetCurrentDirectory() + @"\Log.txt");
                if (Log.Length > 10000000)
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\Log.txt");
                }
            }
        }

        //Događaji
        //Događaj kada se stisne dugme "Igraj BlackJack"
        private void IgrajBlackJack_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("BlackJack je pokrenut.");
            BlackJack BlackJack_Prozor = new BlackJack();
            this.Visibility = Visibility.Hidden;
            BlackJack_Prozor.ShowDialog();
            this.Visibility = Visibility.Visible;
            BlackJack_Prozor.Close();
            Chipovi.Text = BlackJack_Prozor.Stanje.Text;
            Logger.Info("BlackJack je ugašen.");
        }

        //Događaj kada se stisne dugme "Igraj Roulette"
        private void IgrajRoulette_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Roulette je pokrenut.");
            Roulette Roulette_Prozor = new Roulette();
            this.Visibility = Visibility.Hidden;
            Roulette_Prozor.ShowDialog();
            this.Visibility = Visibility.Visible;
            Roulette_Prozor.Close();
            Chipovi.Text = Roulette_Prozor.Stanje.Text;
            Logger.Info("Roulette je ugašen.");
        }

        //Događaj kada se stisne dugme "Kupi Čipove"
        private void KupiChipove_Click(object sender, RoutedEventArgs e)
        {
            KupiChipove KupiChipove_Prozor = new KupiChipove();
            KupiChipove_Prozor.ShowDialog();
            Chipovi.Text = KupiChipove_Prozor.TrenutniChipovi.ToString() + "€";
            KupiChipove_Prozor.Close();
        }

        //Događaj kada se stisne dugme "Prodaj Čipove"
        private void ProdajChipove_Click(object sender, RoutedEventArgs e)
        {
            ProdajaChipova ProdajChipove_Prozor = new ProdajaChipova();
            ProdajChipove_Prozor.ShowDialog();
            Chipovi.Text = ProdajChipove_Prozor.TrenutniChipovi.ToString() + "€";
            ProdajChipove_Prozor.Close();
        }
    }
}
