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
            zeigeSplash();
            while (running) {
                string aktuellerNutzer = authenticate();
                //depending on who and what he wants to do:
                while (aktuellerNutzer == "Kunde") {

                    string Aktion = legeBenutzerAktionFest();

                    if (Aktion == "versenden") {
                        int FachID = findeFach("verfuegbar");
                        Paket newPacket = registrierePakete(FachID);
                        pruefeObPaketEingegeben(); // for dramatic effects!
                        gebePaketIDaus(newPacket);
                        Aktion = "";
                    }
                    if (Aktion == "abholen") {
                        int PacketID = Gui.erfragePacketID();
                        int FachID = findeFach(PacketID);
                        oeffneFach(FachID);
                        Aktion = "";
                    }
                    if (Aktion == "abbrechen") {
                        aktuellerNutzer = "niemand";
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

        private void gebePaketIDaus(Paket newPacket)
        {
            Gui.zeigeNachricht(@"Packet wurde erfolgreich abgelegt.
Hier ist ihre Packet ID: " + newPacket.ID.ToString() + @"
Drücken Sie eine beliebige Taste...");
        }

        private Paket registrierePakete(int[] FachIDs) //TODO: iterieren um mehrere Fächer zu befüllen
        {
            return Gui.registrierePakete();
        }

        private Paket registrierePakete(int FachID)
        {
            // Verknüpfe Packet mit Fach
            Paket newPacket = Gui.registrierePakete();
            for (int i = 0; i < Faecher.Length; i++) {
                if (Faecher[i].ID == FachID) {
                    Faecher[i].Packet = newPacket;
                    Faecher[i].Status = "versenden";
                }
                else {
                    // nothing?
                }
            }
            return newPacket;
        }

        private void pruefeObPaketEingegeben()
        {
            Gui.gebePaketEin();
        }

        private int[] oeffneFaecher(string Status, int Anzahl)
        {
            // Faecher durchiterieren bis ein Vergübares Fach gefunden wurde und die Anzahl an benötigten Fächern erreicht wurde
            int [] geoeffneteFaecher = new int[Anzahl];
            int j = 0;
            for (int i = 0; i < Faecher.Length; i++) {
                if (Faecher[i].Status == Status && j < Anzahl) {
                    geoeffneteFaecher[i] = Faecher[i].ID;
                    j++;
                }
                else {
                    Gui.zeigeNachricht("Keine Fächer mit dem Status '" + Status + "' vorhanden.");
                }
            }
            return geoeffneteFaecher;
        }

        private int findeFach(string Status)
        {
            // Faecher durchiterieren bis ein Vergübares Fach gefunden wurde und die Anzahl an benötigten Fächern erreicht wurde
            int i = 0;
            while (Faecher[i].Status != Status) {
                i++;
                if (i >= Faecher.Length) {
                    Gui.zeigeNachricht("Keine Fächer mit dem Status '" + Status + "' vorhanden.");
                    break;
                }
            }
            return Faecher[i].ID;
        }

        private void oeffneFach(int FachID)
        {
            // Fach oeffnen um Packet herauszunehmen
            Gui.zeigePacketInhalt(Faecher[FachID].Packet.Inhalt);
            Faecher[FachID].Status = "verfuegbar";
            Faecher[FachID].Packet = null;
        }
        private void zeigeSplash() {
            Gui.zeigeSplash();
        }

        private string authenticate() {
            string Nutzer = Gui.authenticate();
            return Nutzer;
        }

        public string legeBenutzerAktionFest() {
                return Gui.legeBenutzerAktionFest();
        }

        public string legeMitarbeiterAktionFest() {
                return Gui.legeMitarbeiterAktionFest();
        }

        public int findeFach(int PacketID) {
            int FachID = -1;
            for (int i = 0; i < Faecher.Length; i++) {
                if (Faecher[i].Packet != null) {
                    if (Faecher[i].Packet.ID == PacketID) {
                    FachID = Faecher[i].ID;
                    }
                }
            }
            return FachID;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
