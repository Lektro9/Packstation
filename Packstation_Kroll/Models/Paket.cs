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
        string _SourceAddress;
        string _DestAddress;

        #endregion

        #region Accessoren/Modifier
        public string SourceAddress { get => _SourceAddress; set => _SourceAddress = value; }
        public string DestAddress { get => _DestAddress; set => _DestAddress = value; }
        #endregion

        #region Konstruktoren
        public Paket () {
            SourceAddress = "TestSource 59945 ExampleStreet";
            DestAddress = "TestDest 59945 ExampleStreet";
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}