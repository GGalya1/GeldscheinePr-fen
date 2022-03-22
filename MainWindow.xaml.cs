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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace GeldscheinePrüfen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool Eingabeprüfung(string seriennummer)
        {

            if (seriennummer.Length != 12)
            {
                Ergebnis_Box.Text = "Die Seriennummer muss folgendes Format haben: XX000000000";
                return false;
            }
            else if (!char.IsLetter(seriennummer[0]) || !char.IsLetter(seriennummer[1]))
            {
                Ergebnis_Box.Text = "Die beiden ersten Zeichen müssen Buchstaben sein.";
                return false;
            }
            else if (!Regex.IsMatch(seriennummer.Substring(2, seriennummer.Length - 2), "^[0-9]+$"))
            {
                Ergebnis_Box.Text = "Die letzen 10 Stellen müssen Ziffer sein!";
                return false;
            }
            else
                return true;
        }

        private void Prüfung(string seriennumer)
        {
            //Umwandlung des Eingabestrings
            int Ländercode1, Ländercode2;

            Ländercode1 = seriennumer.ToUpper()[0] - 64;
            Ländercode2 = seriennumer.ToUpper()[1] - 64;

            string zahl = Ländercode1.ToString() + Ländercode2.ToString() + seriennumer.Substring(2, seriennumer.Length - 3);
            int quersumme = 0;

            foreach (char c in zahl)
            {
                quersumme += Convert.ToInt32(c.ToString());
            }

            float r = quersumme % 9;
            int kontrollnummer = Convert.ToInt32(seriennumer.Last().ToString());
            if (7 - r == kontrollnummer || 7-r == 0 && kontrollnummer == 9 || 7-r == -1 && kontrollnummer == 8)
                Ergebnis_Box.Text = "Die Serien ist gültig";
            else
                Ergebnis_Box.Text = "Die Serien ist ungültig";
            //Beispiel: wa8930983573
        }

        private void Pruf_Knopf_Click(object sender, RoutedEventArgs e)
        {
            if (Eingabeprüfung(Eingabe_Box.Text))
            {
                Prüfung(Eingabe_Box.Text);
            }
            
        }

        private void Eingabe_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Eingabeprüfung(Eingabe_Box.Text))
                {
                    Prüfung(Eingabe_Box.Text);
                }
            }
        }
    }
}
