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
        bool _Status;
        int _Nummer;
        Paket _Packet;
        bool _Belegt;
        #endregion

        #region Accessoren/Modifier
        public bool Status { get => _Status; set => _Status = value; }
        public int Nummer { get => _Nummer; set => _Nummer = value; }
        internal Paket Packet { get => _Packet; set => _Packet = value; }
        public bool Belegt { get => _Belegt; set => _Belegt = value; }
        #endregion

        #region Konstruktoren
        public Fach()
        {
            this.Nummer = -1;
            this.Status = true; //true = funktioniert, false = funktioniert nicht
            this.Packet = null;
            this.Belegt = false;
        }
        //Spezialkonstruktor
        public Fach(int ID)
        {
            this.Nummer = ID;
            this.Status = true; //true = funktioniert, false = funktioniert nicht
            this.Packet = null;
            this.Belegt = false;
        }
        #endregion

        #region Worker
        public bool IstBelegt()
        {
            bool retVal = this.Belegt;
            return retVal;
        }

        public Paket getPaket()
        {
            Paket retVal = this.Packet;
            this.Packet = null;
            return retVal;
        }

        public void PaketAnnehmen(Paket Packet)
        {
            this.Packet = Packet;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
