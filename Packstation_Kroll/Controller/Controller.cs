//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Controller.cs
//Beschreibung: Businesslogic für die Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Controller
    {
        #region Eigenschaften
        Terminal _Gui;
        Fach[] _Faecher;
        Benutzer[] _Nutzer;
        Mitarbeiter[] _Angestellte;
        #endregion

        #region Accessoren/Modifier
        public Benutzer[] Nutzer { get => _Nutzer; set => _Nutzer = value; }
        public Mitarbeiter[] Angestellte { get => _Angestellte; set => _Angestellte = value; }
        internal Terminal Gui { get => _Gui; set => _Gui = value; }
        internal Fach[] Faecher { get => _Faecher; set => _Faecher = value; }
        #endregion

        #region Konstruktoren
        public Controller()
        {
            this.Gui = new Terminal(this);
            this.Faecher = new Fach[9];
            for (int i = 0; i < Faecher.Length; i++)
            {
                this.Faecher[i] = new Fach(i);
            }
            this.Nutzer = new Benutzer[1];
            for (int i = 0; i < Nutzer.Length; i++)
            {
                this.Nutzer[i] = new Benutzer(2, "nutzer", "1234");
            }
            this.Angestellte = new Mitarbeiter[1];
            for (int i = 0; i < Angestellte.Length; i++)
            {
                this.Angestellte[i] = new Mitarbeiter(1, "admin", "admin");
            }
        }
        #endregion

        #region Worker
        public void run()
        {
            bool running = true;
            bool authenticated = false;
            while (running)
            {
                Gui.zeigeAuthMenu();
                if (Gui.Nutzer is Mitarbeiter)
                {
                    authenticated = true;
                    while (authenticated)
                    {
                        Gui.zeigeMitarbeiterMenu();
                        if (Gui.Aktion == "1") //Pakete abholen
                        {
                            for (int i = 0; i < Faecher.Length; i++)
                            {
                                //evtl. effizienter erst nach Paket != null zu suchen?
                                if (Faecher[i].Status == "versenden") //Achtung! evtl. Null Fehler wenn irgendwie ein Fach Status nicht richitg zurückgesetzt wurde und Paket = null ist
                                {
                                    Faecher[i].Packet = null;
                                    Faecher[i].Status = "frei";
                                    Gui.zeigeNachricht("Das Fach " + Faecher[i].ID + " wurde geöffnet, bitte Paket entnehmen.");
                                }
                            }
                            Gui.zeigeNachricht("Keine weiteren Pakete zum abholen bereit.");
                        }
                        else if (Gui.Aktion == "2") //Pakete einlegen
                        {
                            if (Gui.Nutzer.PaketAnzahl > 0)
                            {
                                for (int i = 0; i < Gui.Nutzer.PaketAnzahl; i++)
                                {
                                    int freieFachID = sucheFreieFachID();
                                    if (freieFachID >= 0)
                                    {
                                        Faecher[freieFachID].Status = "abholbereit";
                                        Faecher[freieFachID].Packet = new Paket();
                                        Gui.zeigeNachricht("Paket wurde in das Fach mit der ID " + Faecher[freieFachID].ID + " gelegt.");
                                        Gui.Nutzer.legePaketEin();
                                    }
                                    //für den Fall dass nicht genügend Faecher vorhanden sind
                                    else
                                    {
                                        Gui.zeigeNachricht("Kein freies Fach mehr vorhanden.");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Gui.zeigeNachricht("Sie haben kein Paket dabei, welches eingelegt werden kann.");
                            }
                        }
                        else if (Gui.Aktion == "0") //abbrechen
                        {
                            authenticated = false;
                        }
                    }
                }
                else if (Gui.Nutzer is Benutzer)
                {
                    authenticated = true;
                    while (authenticated)
                    {
                        Gui.zeigeKundenMenu();
                        if (Gui.Aktion == "1") //Paket abholen
                        {
                            //durchsuche Faecher nach Status "abholbereit" und Empfaengernamen des Pakets
                            int FachID = findeAbholbereiteFachID(Gui.Nutzer.Name);
                            //Fach leeren und Status zurücksetzen
                            if (FachID >= 0)
                            {
                                Faecher[FachID].Packet = null;
                                Faecher[FachID].Status = "frei";
                                Gui.zeigeNachricht("Paket wurde abgeholt.");
                            }
                            else
                            {
                                Gui.zeigeNachricht("Keine Pakete für Sie in dieser Packstation.");
                            }
                        }
                        if (Gui.Aktion == "2") //Paket einlegen
                        {
                            if (Gui.Nutzer.PaketAnzahl > 0)
                            {
                                //nach einem freien Fach suchen
                                int freieFachID = sucheFreieFachID();

                                //nutze freies Fach für das Paket
                                if (freieFachID >= 0)
                                {
                                    Faecher[freieFachID].Packet = new Paket();
                                    Faecher[freieFachID].Status = "versenden";
                                    Gui.Nutzer.legePaketEin();
                                    Gui.zeigeNachricht("Paket wurde eingelegt und wird von einem Mitarbeiter abgeholt.");
                                }
                                else
                                {
                                    Gui.zeigeNachricht("Keine freien Fächer vorhanden. Bitte später noch einmal versuchen.");
                                }
                            }
                            else
                            {
                                Gui.zeigeNachricht("Sie haben kein Paket dabei, welches eingelegt werden kann.");
                            }
                        }
                        if (Gui.Aktion == "0") //abbrechen
                        {
                            authenticated = false;
                        }
                    }
                }
            }

        }

        public bool pruefeUndSetzeNutzer(string Login, string Passwort)
        {
            bool retVal = false;
            for (int i = 0; i < Nutzer.Length; i++)
            {
                if (Nutzer[i].Name == Login && Nutzer[i].Passwort == Passwort)
                {
                    //wenn ein Nutzer gefunden, dann bitte direkt den Nutzer in der Gui setzen
                    Gui.Nutzer = Nutzer[i];
                    retVal = true;
                }
            }

            for (int i = 0; i < Angestellte.Length; i++)
            {
                if (Angestellte[i].Name == Login && Angestellte[i].Passwort == Passwort)
                {
                    //wenn ein Angestellter gefunden, dann bitte direkt den Nutzer in der Gui setzen
                    Gui.Nutzer = Angestellte[i];
                    retVal = true;
                }
            }
            return retVal;
        }

        public int sucheFreieFachID()
        {
            int retVal = -1;
            for (int i = 0; i < Faecher.Length; i++)
            {
                if (Faecher[i].Status == "frei")
                {
                    retVal = Faecher[i].ID;
                    break;
                }
            }
            return retVal;
        }

        public int findeAbholbereiteFachID(string Nutzername)
        {
            int retVal = -1;
            for (int i = 0; i < Faecher.Length; i++)
            {
                if (Faecher[i].Status == "abholbereit")
                {
                    if (Faecher[i].Packet.EmpfaengerName == Nutzername)
                    {
                        retVal = Faecher[i].ID;
                        break;
                    }
                }
            }
            return retVal;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
