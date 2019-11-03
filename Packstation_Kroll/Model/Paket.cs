//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Packet.cs
//Beschreibung: Fach für die Pakete in der Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Paket
    {
        #region Eigenschaften
        string _Inhalt;
        int _ID;
        string _Absender;
        string _Empfaenger;
        #endregion

        #region Accessoren/Modifier
        public string Inhalt { get => _Inhalt; set => _Inhalt = value; }
        public int ID { get => _ID; set => _ID = value; }
        public string Absender { get => _Absender; set => _Absender = value; }
        public string Empfaenger { get => _Empfaenger; set => _Empfaenger = value; }
        #endregion

        #region Konstruktoren
        public Paket()
        {
            this.Inhalt = "Standartpaket Inhalt";
            this.ID = 1234;
            this.Absender = "einAbsender";
            this.Empfaenger = "nutzer";
        }
        //Spezialkonstruktor
        public Paket(string Inhalt, int ID, string Absender, string Empfaenger)
        {
            this.Inhalt = Inhalt;
            this.ID = ID;
            this.Absender = Absender;
            this.Empfaenger = Empfaenger;
        }
        #endregion

        #region Worker

        #endregion

        #region Schnittstellen
        #endregion
    }
}
