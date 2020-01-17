//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Fach.cs
//Beschreibung: Fach für die Pakete in der Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//29.12.2019:   Entwicklung abgeschlossen

using System;

namespace Packstation_Kroll
{
    public class Fach
    {
        #region Eigenschaften
        bool _Status; //true = funktioniert, false = funktioniert nicht
        int _Nummer;
        Paket _Packet;
        bool _Belegt;
        Groesse _Groesse;
        #endregion

        #region Accessoren/Modifier
        public bool Status { get => _Status; set => _Status = value; }
        public int Nummer { get => _Nummer; set => _Nummer = value; }
        public Paket Packet { get => _Packet; set => _Packet = value; }
        public bool Belegt { get => _Belegt; set => _Belegt = value; }
        public Groesse Groesse { get => _Groesse; set => _Groesse = value; }
        #endregion

        #region Konstruktoren
        public Fach()
        {
            this.Nummer = -1;
            this.Status = false; //nicht funktionsfähig weil keine gültige Nummer eingegeben wurde
            this.Packet = null;
            this.Belegt = false;
            this.Groesse = Groesse.XS;
        }
        //Spezialkonstruktor
        public Fach(int ID)
        {
            this.Nummer = ID;
            this.Status = true; 
            this.Packet = null;
            this.Belegt = false;
            this.Groesse = Groesse.XS;
        }

        public Fach(int ID, Groesse groesse)
        {
            this.Nummer = ID;
            this.Status = true;
            this.Packet = null;
            this.Belegt = false;
            this.Groesse = groesse;
        }

        public Fach(int ID, bool Status, Paket Packet, bool Belegt, Groesse groesse)
        {
            this.Nummer = ID;
            this.Status = Status;
            this.Packet = Packet;
            this.Belegt = Belegt;
            this.Groesse = groesse;
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
            Paket retVal = new Paket(this.Packet);
            this.Belegt = false;
            this.Packet = null;
            return retVal;
        }

        public void PaketAnnehmen(Paket Packet)
        {
            this.Packet = Packet;
            this.Belegt = true;
            this.Packet.PaketfachNr = this.Nummer;
        }

        public bool IstGrossGenug(Groesse PaketGroesse)
        {
            bool retVal = false;
            if ((int) this.Groesse >= (int) PaketGroesse)
            {
                retVal = true;
            }
            return retVal;
        }
        
        #endregion

        #region Schnittstellen
        #endregion
    }
}
