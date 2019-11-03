//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Fach.cs
//Beschreibung: Fach für die Pakete in der Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Fach
    {
        #region Eigenschaften
        string _Status;
        int _ID;
        Paket _Packet;
        #endregion

        #region Accessoren/Modifier
        public string Status { get => _Status; set => _Status = value; }
        public int ID { get => _ID; set => _ID = value; }
        internal Paket Packet { get => _Packet; set => _Packet = value; }
        #endregion

        #region Konstruktoren
        public Fach(int ID)
        {
            this.ID = ID;
            this.Status = "frei"; //3 Mögl.: frei, abholbereit, versenden
            this.Packet = null;
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}
