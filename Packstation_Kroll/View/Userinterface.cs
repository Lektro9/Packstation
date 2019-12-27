//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Terminal.cs
//Beschreibung: GUI für Nutzer
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//20.11.2019:   Umstrukturierung nach Besprechung im Unterricht

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    public class Userinterface
    {
        #region Eigenschaften
        string _Text;
        List<Paket> _Liste;
        ConsoleKeyInfo _Input;
        #endregion

        #region Accessoren/Modifier
        public string Text { get => _Text; set => _Text = value; }
        public List<Paket> Liste { get => _Liste; set => _Liste = value; }
        public ConsoleKeyInfo Input { get => _Input; set => _Input = value; }
        #endregion

        #region Konstruktoren
        public Userinterface()
        {
            this.Text = null;
            this.Liste = null;
            this.Input = new ConsoleKeyInfo();
        }
        public Userinterface(string Text)
        {
            this.Text = Text;
            this.Liste = null;
            this.Input = new ConsoleKeyInfo();
        }

        public Userinterface(string Text, List<Paket> Liste)
        {
            this.Text = Text;
            this.Liste = Liste;
            this.Input = new ConsoleKeyInfo();
        }

        public Userinterface(List<Paket> Liste)
        {
            this.Text = null;
            this.Liste = Liste;
            this.Input = new ConsoleKeyInfo();
        }

        //Spezialkonstruktor
        #endregion

        #region Worker
        public void SplashAnzeigen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            this.Text = "\n           Programmm:    Packstation Version 1.0" +
                "\n           Autor:        Kroll" +
                "\n           Beschreibung: Verwaltung von Paketen";
            Console.Write(this.Text);
        }

        public void loginAufforderung(ref string benutzername, ref string passwort)
        {
            Console.WriteLine("Benutzername: ");
            benutzername = Console.ReadLine();
            Console.WriteLine("Passwort: ");
            passwort = Console.ReadLine(); //TODO: Passwort nicht auf dem Bildschirm anzeigen
        }

        public void TextAusgeben(string Text)
        {
            Console.Clear();
            Console.WriteLine(Text);
            Console.ReadKey();
        }

        public void WeiterESC()
        {
            while (this.Input.Key != ConsoleKey.Escape)
            {
                this.Input = Console.ReadKey();
            }
        }

        public string KundenMenueAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("Was möchten Sie tun: ");
            Console.WriteLine();
            Console.WriteLine();

            string KundenMenue = "\n 1. Paket abholen" +
                "\n 2. Paket einlegen" +
                "\n 0. Abbrechen";
            Console.WriteLine(KundenMenue);
            
            return KundenMenue;
        }

        public string MitarbeiterMenueAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("Was möchten Sie tun: ");
            Console.WriteLine();
            Console.WriteLine();
            
            string Mitarbeitermenue = "\n 1. Alle Pakete abholen" +
                "\n 2. Alle Pakete einlegen" +
                "\n 0. Abbrechen";
            Console.WriteLine(Mitarbeitermenue);

            return Mitarbeitermenue;
        }

        public void PaketeZumAbholenAuflisten(List<Paket> Liste)
        {
            Console.WriteLine("Diese Pakete werden abgeholt");
            for (int i = 0; i < Liste.Count; i++)
            {
                Console.WriteLine(Liste[i]);
            }
        }

        public void WeiterMitTaste()
        {
            while (!Console.KeyAvailable) ;
            Console.ReadKey();
        }

        public string TextEinlesen()
        {
            string Eingabe = Console.ReadLine();
            return Eingabe;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
