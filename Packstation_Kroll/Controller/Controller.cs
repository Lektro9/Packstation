//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Controller.cs
//Beschreibung: Verwaltet die Daten
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Controller
    {
        #region Eigenschaften
        Terminal _Gui;
        Fach[] _Faecher;

        #endregion

        #region Accessoren/Modifier
        public Terminal Gui { get => _Gui; set => _Gui = value; }
        public Fach[] Faecher { get => _Faecher; set => _Faecher = value; }
        #endregion

        #region Konstruktoren
        public Controller () {
            Gui = new Terminal();
            Faecher = new Fach[9];
            for (int i = 0; i < Faecher.Length; i++) {
                Faecher[i] = new Fach(i);
            }
        }
        #endregion

        #region Worker
        public void run()
        {
            bool running = true;
            Console.WriteLine("Here should start the Packstation logic!");
            zeigeSplash();
            while (running) {
                zeigeMenu("Main");
                //Menu logic here
                string aktuellerNutzer = authenticate();
                //depending on who and what he wants to do:
                while (aktuellerNutzer == "Kunde") {

                    zeigeMenu("standartMenu");

                    string Aktion = legeAktionFest();
                    if (Aktion == "versenden") {
                        oeffneFaecher("verfuebar");
                        pruefeObPaketEingegeben(); // will just output some graphics
                        gebePaketIDaus();
                        Aktion = "";
                    }
                    if (Aktion == "abholen") {
                        int PaketID = erfragePaketID();
                        int Fachnummer = findeFachnummer(PaketID);
                        oeffneFaecher(Fachnummer);
                        Paket Packet = gebePacketAus(Fachnummer);
                        gebeInhaltAus(Packet);
                        Aktion = "";
                    }
                    if (Aktion == "abbrechen") {
                        Nutzer = "niemand";
                        Aktion = "";
                    }
                }
                // while (Gui.Nutzer == "mitarbeiter") {
                //     frageNachPasswort();
                //     zeigeMitarbeiterMenu();

                //     Gui.Aktion = legeAktionFest();

                //     if (Gui.Aktion == "ablegen") {
                //         int Fachnummern = gebeVerfuegbaresFach("all");
                //         oeffneFaecher(Fachnummern);
                //         pruefeObPaketEingegeben();
                //         gebePaketIDaus();
                //     }
                //     if (Gui.Aktion == "abholen") {
                //         int Fachnummern = gebeAbzuholendeFaecherAus();
                //         oeffneFaecher(Fachnummern);

                //     }
                // }
            }
        }
        public void zeigeSplash() {
            Gui.zeigeSplash();
        }
        public void zeigeMenu(String Menu) {
            Gui.zeigeMenu(Menu);
        }

        public string authenticate() {
            System.Console.WriteLine("Sind Sie ein 'Kunde' oder ein 'Mitarbeiter'?");
            string Nutzer = Console.ReadLine();
            return Nutzer;
        }

        public string legeAktionFest() {

        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
