//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Geschaeftsfuehrer.cs
//Beschreibung: Klasse für Geschaeftsfuehrer
//Änderungen:
//28.01.2020:   Entwicklung 2.0 abgeschlossen   

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    public class Geschaeftsfuehrer
    {
        #region Eigenschaften
        long _ID;
        string _Name;
        string _Benutzername;
        string _Passwort;
        #endregion

        #region Accessoren/Modifier
        public long ID { get => _ID; set => _ID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Benutzername { get => _Benutzername; set => _Benutzername = value; }
        public string Passwort { get => _Passwort; set => _Passwort = value; }
        #endregion

        #region Konstruktoren
        public Geschaeftsfuehrer()
        {
            this.ID = -1;
            this.Name = "Niemand";
            this.Benutzername = "nichtVorhanden";
            this.Passwort = "0000";
        }

        //Spezialkonstruktor

        public Geschaeftsfuehrer(int ID, string Name, string Benutzername, string Passwort)
        {
            this.ID = ID;
            this.Name = Name;
            this.Benutzername = Benutzername;
            this.Passwort = Passwort;
        }
        #endregion

        #region Worker
        public bool Authentifizieren(string ben, string passwd)
        {
            bool retVal = false;
            if (this.Benutzername == ben && this.Passwort == passwd)
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
