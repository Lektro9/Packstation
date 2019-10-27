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
        bool _IstFrei;
        #endregion

        #region Accessoren/Modifier
        public bool IstFrei { get => _IstFrei; set => _IstFrei = value; }
        #endregion

        #region Konstruktoren
        public Fach () {
            IstFrei = true;
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}
