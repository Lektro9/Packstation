//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Terminal.cs
//Beschreibung: Benutzerinteraktion
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class Terminal
    {
        #region Eigenschaften
        string _Nutzer;
        #endregion

        #region Accessoren/Modifier
        public string Nutzer { get => _Nutzer; set => _Nutzer = value; }
        #endregion

        #region Konstruktoren
        public Terminal () {
            Nutzer = "Kunde";
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}
