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

        #endregion

        #region Accessoren/Modifier
        public Terminal Gui { get => _Gui; set => _Gui = value; }
        #endregion

        #region Konstruktoren
        public Controller () {
            Gui = new Terminal();
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}
