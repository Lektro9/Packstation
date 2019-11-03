//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Mitarbeiter.cs
//Beschreibung: Klasse für Mitarbeiter
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    public class Mitarbeiter : Benutzer
    {
        #region Eigenschaften
        #endregion

        #region Accessoren/Modifier
        #endregion

        #region Konstruktoren
        public Mitarbeiter(int PaketAnzahl, string Name, string Passwort)
        {
            this.PaketAnzahl = PaketAnzahl;
            this.Name = Name;
            this.Passwort = Passwort;
        }
        #endregion

        #region Worker
        #endregion

        #region Schnittstellen
        #endregion
    }
}
