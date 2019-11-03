//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Terminal.cs
//Beschreibung: GUI für Nutzer
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Terminal
    {
        #region Eigenschaften
        Benutzer _Nutzer;
        bool _ValideEingabe;
        string _Aktion;
        int _AnzahlAuswahl;
        Controller _Verwalter;
        #endregion

        #region Accessoren/Modifier
        public Benutzer Nutzer { get => _Nutzer; set => _Nutzer = value; }
        public bool ValideEingabe { get => _ValideEingabe; set => _ValideEingabe = value; }
        public string Aktion { get => _Aktion; set => _Aktion = value; }
        public int AnzahlAuswahl { get => _AnzahlAuswahl; set => _AnzahlAuswahl = value; }
        internal Controller Verwalter { get => _Verwalter; set => _Verwalter = value; }
        #endregion

        #region Konstruktoren
        public Terminal(Controller Verwalter)
        {
            this.Nutzer = null;
            this.ValideEingabe = false;
            this.Aktion = "nichts";
            this.AnzahlAuswahl = 0;
            this.Verwalter = Verwalter;
        }

        //Spezialkonstruktor
        #endregion

        #region Worker
        public void zeigeAuthMenu()
        {
            this.ValideEingabe = false;
            while (ValideEingabe == false)
            {
                Console.Clear();
                Console.WriteLine("Authentifizieren Sie sich: ");
                Console.WriteLine();
                Console.WriteLine();

                Console.Write("Login: ");
                string Login = Console.ReadLine();

                Console.WriteLine();

                Console.Write("Passwort: ");
                string Passwort = Console.ReadLine();

                if (Verwalter.pruefeUndSetzeNutzer(Login, Passwort) == true)
                {
                    ValideEingabe = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Ungültige Einlogdaten, bitte prüfen Sie Ihre Eingabe");
                    Console.ReadKey();
                }
            }
        }

        public void zeigeKundenMenu()
        {
            this.ValideEingabe = false;
            while (ValideEingabe == false)
            {
                Console.Clear();
                Console.WriteLine("Was möchten Sie tun: ");
                Console.WriteLine();
                Console.WriteLine();

                //Anzahl an Auswahlmöglichkeiten werden hier bestimmt
                AnzahlAuswahl = 2;
                Console.WriteLine("1. Paket abholen");
                Console.WriteLine("2. Paket einlegen");
                Console.WriteLine("0. Abbrechen");

                string Auswahl = Console.ReadLine();

                if (pruefeEingabe(AnzahlAuswahl, Auswahl) == true)
                {
                    this.Aktion = Auswahl;
                    ValideEingabe = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Ungültige Eingabe, bitte wählen Sie eine andere Option.");
                    Console.ReadKey();
                }
            }
        }

        public void zeigeMitarbeiterMenu()
        {
            this.ValideEingabe = false;
            while (ValideEingabe == false)
            {
                Console.Clear();
                Console.WriteLine("Was möchten Sie tun: ");
                Console.WriteLine();
                Console.WriteLine();

                //Anzahl an Auswahlmöglichkeiten werden hier bestimmt
                AnzahlAuswahl = 2;
                Console.WriteLine("1. Alle Pakete abholen");
                Console.WriteLine("2. Alle Pakete einlegen");
                Console.WriteLine("0. Abbrechen");

                string Auswahl = Console.ReadLine();

                if (pruefeEingabe(AnzahlAuswahl, Auswahl) == true)
                {
                    this.Aktion = Auswahl;
                    ValideEingabe = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Ungültige Eingabe, bitte wählen Sie eine andere Option.");
                    Console.ReadKey();
                }
            }
        }

        public bool pruefeEingabe(int AnzahlAuswahl, string Auswahl)
        {
            //TODO: negative Zahlen können eingegeben werden
            bool retVal = false;
            for (int i = 0; i <= AnzahlAuswahl; i++)
            {
                if (i.ToString() == Auswahl)
                {
                    retVal = true;
                }
            }
            return retVal;
        }

        public void zeigeNachricht(string Nachricht)
        {
            Console.Clear();
            Console.WriteLine(Nachricht);
            Console.ReadKey();
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
