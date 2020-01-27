//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Terminal.cs
//Beschreibung: GUI für Nutzer
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//20.11.2019:   Umstrukturierung nach Besprechung im Unterricht
//23.01.2020:   Entwicklung der neuen Features (Pakete mit unterschiedlichen Größen und mehrere Stationen) abgeschlossen

using System;
using System.Collections.Generic;
using System.Threading;

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
            this.Text = "\n           Programmm:    Packstation Version 2.0" +
                "\n           Autor:        Kroll" +
                "\n           Beschreibung: Verwaltung von Paketen" +
                "\n\n\n";
            Console.Write(this.Text);
            Thread.Sleep(1000);
        }

        public void loginAufforderung(ref string benutzername, ref string passwort)
        {
            Console.Clear();
            Console.WriteLine("Authentifizierung");
            Console.WriteLine();
            Console.Write("Benutzername: ");
            benutzername = TextEinlesen();
            Console.Write("Passwort: ");
            passwort = TextEinlesen(); //TODO: Passwort nicht auf dem Bildschirm anzeigen
        }

        public void TextAusgeben(string Text)
        {
            Console.Clear();
            Console.WriteLine(Text);
        }

        public void WeiterESC()
        {
            while (this.Input.Key != ConsoleKey.Escape)
            {
                this.Input = Console.ReadKey();
            }
        }

        public string StationsMenueAnzeigen(int Stationsnummer)
        {
            Console.Clear();
            Console.WriteLine("Station " + Stationsnummer + ":");
            Console.WriteLine("Was möchten Sie tun: ");
            Console.WriteLine("");

            string StationsMenue = "\n 1. Einloggen" +
                "\n 2. Station verlassen";
            Console.WriteLine(StationsMenue);

            return StationsMenue;
        }

        public string KundenMenueAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("Was möchten Sie tun: ");
            Console.WriteLine("");

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
            
            string Mitarbeitermenue = "\n 1. Alle Pakete abholen" +
                "\n 2. Alle Pakete einlegen" +
                "\n 3. Defektes Fach auswechseln" +
                "\n 0. Abbrechen" +
                "\n\noder 'abschalten' eingeben, um die Station auszuschalten.";
            Console.WriteLine(Mitarbeitermenue);

            return Mitarbeitermenue;
        }

        public void StationHinzufuegenMenueAnzeigen()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie eine ID und eine Fachanzahl an (darf nicht unter 9 sein!) ");
            Console.WriteLine();
        }

        public void MetaMenueAnzeigen(int MaxAnzahl)
        {
            Console.Clear();
            string MetaMenue = "\n 1. Eine Station auswählen" +
                "\n 2. Als Geschäftsführer einloggen";
            Console.WriteLine(MetaMenue);
            Console.WriteLine();
        }

        public void MitarbeiterVerwaltenMenueAnzeigen()
        {
            Console.Clear();
            string MVMenue = "\n 1. Mitarbeiter hinzufügen" +
                "\n 2. Mitarbeiter entfernen" +
                "\n 0. Abbrechen";
            Console.WriteLine(MVMenue);
            Console.WriteLine();
        }

        public void KundenVerwaltenMenueAnzeigen()
        {
            Console.Clear();
            string KVMenue = "\n 1. Mitarbeiter hinzufügen" +
                "\n 2. Mitarbeiter entfernen" +
                "\n 0. Abbrechen";
            Console.WriteLine(KVMenue);
            Console.WriteLine();
        }

        public void GeschaeftsfuehrerMenueAnzeigen()
        {
            Console.Clear();
            string GeschMenue = "\n 1. Station hinzufügen" +
                "\n 2. Station entfernen" +
                "\n 3. Station um Faecher erweitern" +
                "\n 4. Mitarbeiter verwalten" +
                "\n 5. Kunden verwalten" +
                "\n 0. Abbrechen";
            Console.WriteLine(GeschMenue);
            Console.WriteLine();
        }

        public void PaketeZumAbholenAuflisten(List<Paket> Liste)
        {
            Console.WriteLine("Diese Pakete werden abgeholt");
            for (int i = 0; i < Liste.Count; i++)
            {
                Console.WriteLine(Liste[i]);
            }
        }

        public virtual void WeiterMitTaste()
        {
            while (!Console.KeyAvailable) ;
            Console.ReadKey();
        }

        public virtual string TextEinlesen()
        {
            string Eingabe = Console.ReadLine();
            return Eingabe;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
