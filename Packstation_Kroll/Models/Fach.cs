//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Fach.cs
//Beschreibung: Zur Aufbewahrung der Pakete
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Fach
    {
        #region Eigenschaften
        string _Status;
        int _FachID;
        Paket _Packet;
        #endregion

        #region Accessoren/Modifier
        public string Status { get => _Status; set => _Status = value; } //kann "verfuegbar", "versenden" oder "abholen" sein
        public int FachID { get => _FachID; set => _FachID = value; }
        internal Paket Packet { get => _Packet; set => _Packet = value; }
        #endregion

        #region Konstruktoren
        public Fach (int FachID) {
            this.Status = "verfuegbar";
            this.FachID = FachID;
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}
