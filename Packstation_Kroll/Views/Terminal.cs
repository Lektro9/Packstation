//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Terminal.cs
//Beschreibung: Benutzerinteraktion
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;
using System.Collections.Generic;
using System.Threading;

namespace Packstation_Kroll
{
    class Terminal
    {
        #region Eigenschaften
        string _Nutzer;
        string _MenuEingabe;
        IDictionary<string, Menu> _Menus;
        #endregion

        #region Accessoren/Modifier
        public string Nutzer { get => _Nutzer; set => _Nutzer = value; }
        internal IDictionary<string, Menu> Menus { get => _Menus; set => _Menus = value; }
        #endregion

        #region Konstruktoren
        public Terminal() {
            Nutzer = "Kunde";
            Menus = new Dictionary<string, Menu>();
            erstelleMenus();
        }
        #endregion

        #region Worker
        public void zeigeMenu(string Menu) {
            switch(Menu) {
                case "KundenMenu":
                Console.Clear();
                Console.WriteLine("Was möchten Sie gerne tun?");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Ein Paket versenden");
                break;

                case "Authentifizierung":
                Console.Clear();
                Console.WriteLine("Als was möchten Sie fortfahren?");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Normaler Nutzer");
                break;


                default:
                Console.Clear();
                Console.WriteLine("Menu exisitiert nicht");
                break;
            }
        }
        
        public void erstelleMenus() {
            Menus["KundenMenu"] = new Menu("Wählen Sie eine Option: ", new List<string> {"versenden", "abholen", "abbrechen"});
            Menus["AuthentifizierungsMenu"] = new Menu("Bitte wählen Sie aus, wer Sie sind: ", new List<string> {"Kunde", "Mitarbeiter", "Beenden"});
        }

        public void zeigeSplash() {
            Console.Clear();
            Console.WriteLine("\n           Programmm:    Packstation Version 0.1" +
                "\n           Autor:        Kroll" +
                "\n           Beschreibung: Packete ein und ausgeben! Spannend!");
            Thread.Sleep(2000);
        }

        public string authenticate() {

            Console.Clear();
            Console.WriteLine(Menus["AuthentifizierungsMenu"].anzeigen());

            Nutzer = Console.ReadLine();
            switch(Nutzer) {
                case "1":
                Nutzer = "Kunde";
                break;

                default:
                Console.WriteLine("Kein gültiger Nutzer");
                break;
            }
            return Nutzer;
        }

        public string legeBenutzerAktionFest() {
            bool validInput = false;
            string Aktion = "";

            Console.Clear();
            Console.WriteLine(Menus["KundenMenu"].anzeigen());
            renderPackstation();

            while(validInput == false) {
                int j;
                Aktion = Console.ReadLine();
                for (int i = 0; i < Menus["KundenMenu"].AuswahlOptionen.Count; i++) {
                    j = i + 1;
                    if (Aktion == j.ToString()){
                        Aktion = Menus["KundenMenu"].AuswahlOptionen[i];
                        validInput = true;
                    }
                }
            }
            return Aktion;
        }

        public string legeMitarbeiterAktionFest() {
            bool validInput = false;
            string Aktion = "";
            Console.WriteLine("Möchten Sie ein Paket 'versenden' oder 'abholen' (noch nicht implementiert)?");
            while(validInput == false) {
                Aktion = Console.ReadLine();
                switch (Aktion) {
                    case "versenden":
                    validInput = true;
                    break;

                    default:
                    Console.WriteLine("Falsche Eingabe, bitte noch einmal versuchen.");
                    Console.ReadKey();
                    continue;
                }
            }
            return Aktion;
        }

        public void gebePaketEin()
        {
            Console.WriteLine("Paket wird eingelegt");
            Console.Write(".");
            Thread.Sleep(250);
            Console.Write(".");
            Thread.Sleep(250);
            Console.Write(". \n");
            Thread.Sleep(250);
            Console.WriteLine("Fertig!");
        }

        public Paket registrierePakete()
        {
            Console.WriteLine("Geben Sie eine Nachricht ein, die in das Paket hineinkommt: ");
            string Nachricht = Console.ReadLine();
            Paket newPacket = new Paket(Nachricht, generierePaketID(1000,9999));
            return newPacket;
        }
        
        private int generierePaketID(int min, int max)  
        {  
            Random random = new Random();  
            return random.Next(min, max);  
        }

        public void zeigeNachricht(string Nachricht)
        {
            Console.WriteLine(Nachricht);
            Console.ReadKey();
        }

        public int erfragePacketID() {
            Console.WriteLine("Wie lautet ihre Packet ID?");
            int PacketID = Int32.Parse(Console.ReadLine()); //TODO: Fehler abfangen
            return PacketID;
        }
        
        public void zeigePacketInhalt(string Inhalt) {
            Console.Clear();
            Console.WriteLine("Der Inhalt des Packetes lautet: \n");
            Console.WriteLine(Inhalt + "\n");
            Console.WriteLine("weiter mit beliebiger Taste...");
            Console.ReadKey();
        }

        private void renderPackstation() {
            Console.SetCursorPosition(50, 1);
            Console.Write("1 | 2 | 3");
            Console.SetCursorPosition(50, 2);
            Console.Write("---------");
            Console.SetCursorPosition(50, 3);
            Console.Write("4 | 5 | 6");
            Console.SetCursorPosition(50, 4);
            Console.Write("---------");
            Console.SetCursorPosition(50, 5);
            Console.Write("7 | 8 | 9");
            Console.WriteLine();
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
