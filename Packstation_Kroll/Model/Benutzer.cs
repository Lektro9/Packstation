//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Kunde.cs
//Beschreibung: Klasse für Kunde
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    public class Benutzer
    {
        #region Eigenschaften
        int _PaketAnzahl;
        string _Name;
        string _Passwort;
        #endregion

        #region Accessoren/Modifier
        public int PaketAnzahl { get => _PaketAnzahl; set => _PaketAnzahl = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Passwort { get => _Passwort; set => _Passwort = value; }
        #endregion

        #region Konstruktoren
        public Benutzer()
        {
            this.PaketAnzahl = 0;
            this.Name = "Benutzer1";
            this.Passwort = "1234";
        }

        //Spezial Konstruktor
        public Benutzer(int PaketAnzahl, string Name, string Passwort)
        {
            this.PaketAnzahl = PaketAnzahl;
            this.Name = Name;
            this.Passwort = Passwort;
        }
        #endregion

        #region Worker
        public void legePaketEin()
        {
            this.PaketAnzahl = this.PaketAnzahl - 1;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
