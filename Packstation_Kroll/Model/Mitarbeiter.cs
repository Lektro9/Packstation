//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Mitarbeiter.cs
//Beschreibung: Klasse für Mitarbeiter
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//29.12.2019:   Entwicklung abgeschlossen

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    public class Mitarbeiter
    {
        #region Eigenschaften
        long _MitarbeiterID;
        string _Name;
        string _Benutzername;
        string _Passwort;
        List<Paket> _LieferPakete;
        List<Paket> _AbgeholtePakete;
        List<Fach> _ErsatzFaecher;
        #endregion

        #region Accessoren/Modifier
        public long MitarbeiterID { get => _MitarbeiterID; set => _MitarbeiterID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Benutzername { get => _Benutzername; set => _Benutzername = value; }
        public string Passwort { get => _Passwort; set => _Passwort = value; }
        public List<Paket> LieferPakete { get => _LieferPakete; set => _LieferPakete = value; } //Pakete die in aktuelle Station geliefert werden müssen
        public List<Paket> AbgeholtePakete { get => _AbgeholtePakete; set => _AbgeholtePakete = value; } //Pakete die aus aktueller Station entnommen wurden, müssen irgendwie irgendwann zu Lieferpaketen werden
        public List<Fach> ErsatzFaecher { get => _ErsatzFaecher; set => _ErsatzFaecher = value; }
        #endregion

        #region Konstruktoren
        public Mitarbeiter()
        {
            this.MitarbeiterID = 0L;
            this.Name = "noName";
            this.Benutzername = "noUser";
            this.Passwort = "0000";
            this.LieferPakete = null;
            this.AbgeholtePakete = null;
            this.ErsatzFaecher = new List<Fach>();
        }
        //Spezialkonstruktoren
        public Mitarbeiter(long MitarbeiterID, string Name, string Benutzername, string Passwort, List<Paket> LieferPakete, List<Paket> AbgeholtePakete)
        {
            this.MitarbeiterID = MitarbeiterID;
            this.Name = Name;
            this.Benutzername = Benutzername;
            this.Passwort = Passwort;
            this.LieferPakete = LieferPakete;
            this.AbgeholtePakete = AbgeholtePakete;
            this.ErsatzFaecher = new List<Fach>();
        }

        public Mitarbeiter(long MitarbeiterID, string Name, string Benutzername, string Passwort, List<Paket> LieferPakete, List<Paket> AbgeholtePakete, List<Fach> ErsatzFaecher)
        {
            this.MitarbeiterID = MitarbeiterID;
            this.Name = Name;
            this.Benutzername = Benutzername;
            this.Passwort = Passwort;
            this.LieferPakete = LieferPakete;
            this.AbgeholtePakete = AbgeholtePakete;
            this.ErsatzFaecher = ErsatzFaecher;
        }

        #endregion

        #region Worker
        public List<Paket> PaketeLiefern()
        {
            List<Paket> retPakete = new List<Paket>();
            for (int i = LieferPakete.Count - 1; i >= 0 ; i--)
            {
                if (LieferPakete[i].Status == "Transport")
                {
                    LieferPakete[i].Status = "Abholen";
                    retPakete.Add(LieferPakete[i]);
                    LieferPakete.RemoveAt(i);
                }
            }
            return retPakete;
        }

        public void PaketeAbholen(List<Paket> PaketListe)
        {
            for (int i = 0; i < PaketListe.Count; i++)
            {
                if (PaketListe[i].Status == "Abholen")
                {
                    PaketListe[i].Status = "Transport";
                    LieferPakete.Add(PaketListe[i]);
                    PaketListe.RemoveAt(i);
                }
            }
        }

        public bool Authentifizieren(string ben, string passwd)
        {
            bool retVal = false;
            if (this.Benutzername == ben && this.Passwort == passwd)
            {
                retVal = true;
            }
            return retVal;
        }

        public int getAuslieferndePakete()
        {
            int retVal = 0;
            retVal = LieferPakete.Count;
            return retVal;
        }

        public int getAbgeholtePakete()
        {
            int retVal = 0;
            retVal = AbgeholtePakete.Count;
            return retVal;
        }

        #endregion

        #region Schnittstellen
        #endregion
    }
}
