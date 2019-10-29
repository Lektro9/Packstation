//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Paket.cs
//Beschreibung: Paket Modell
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Paket
    {


        #region Eigenschaften
        string _Inhalt;
        int _ID;
        string _SourceAddress;
        string _DestAddress;

        #endregion

        #region Accessoren/Modifier
        public string SourceAddress { get => _SourceAddress; set => _SourceAddress = value; }
        public string DestAddress { get => _DestAddress; set => _DestAddress = value; }
        public int ID { get => _ID; set => _ID = value; }
        public string Inhalt { get => _Inhalt; set => _Inhalt = value; }
        #endregion

        #region Konstruktoren
        public Paket (string Inhalt, int ID) {
            SourceAddress = "TestSource 59945 ExampleStreet";
            DestAddress = "TestDest 59945 ExampleStreet";
            this.Inhalt = Inhalt;
            this.ID = ID;
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}