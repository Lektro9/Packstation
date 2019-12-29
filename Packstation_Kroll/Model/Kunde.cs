//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Kunde.cs
//Beschreibung: Klasse für Kunde
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//29.12.2019:   Entwicklung abgeschlossen

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    public class Kunde
    {
        #region Eigenschaften
        long _Kundenummer;
        string _Name;
        string _Adresse;
        string _Benutzername;
        string _Passwort;
        List<Paket> _Pakete;
        #endregion

        #region Accessoren/Modifier
        public long Kundenummer { get => _Kundenummer; set => _Kundenummer = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Adresse { get => _Adresse; set => _Adresse = value; }
        public string Benutzername { get => _Benutzername; set => _Benutzername = value; }
        public string Passwort { get => _Passwort; set => _Passwort = value; }
        public List<Paket> Pakete { get => _Pakete; set => _Pakete = value; }
        #endregion

        #region Konstruktoren
        public Kunde()
        {
            this.Kundenummer = 0;
            this.Name = "NoName";
            this.Adresse = "NoAddress";
            this.Benutzername = "NoUser";
            this.Passwort = "0000";
            this.Pakete = null;
        }

        //Spezial Konstruktor
        public Kunde(long Kundenummer, string Name, string Adresse, string Benutzername, string Passwort, List<Paket> Pakete)
        {
            this.Kundenummer = Kundenummer;
            this.Name = Name;
            this.Adresse = Adresse;
            this.Benutzername = Benutzername;
            this.Passwort = Passwort;
            this.Pakete = Pakete;
        }

        #endregion

        #region Worker
        public Paket PaketEinliefern()
        {
            Paket retPaket = null;
            for (int i = 0; i < Pakete.Count; i++)
            {
                if (Pakete[i].Status == "Verschicken")
                {
                    Pakete[i].aendereStatus("abzuholen");
                    retPaket = Pakete[i];
                    Pakete.RemoveAt(i);
                    break;
                }
            }
            return retPaket;
        }

        public void PaketAbholen(Paket paket)
        {
            paket.aendereStatus("abgeholt");
            this.Pakete.Add(paket);
        }

        public bool Authentifizieren(string ben, string passwd)
        {
            bool retVal = false;
            if (this.Benutzername == ben && this.Passwort == passwd)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }

        public bool hatPaketabzugeben()
        {
            bool retVal = false;
            for (int i = 0; i < Pakete.Count; i++)
            {
                if (Pakete[i].Status == "Verschicken")
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }

        public bool hatPaketeabgeholt()
        {
            bool retVal = false;
            int counter = 0;
            for (int i = 0; i < Pakete.Count; i++)
            {
                if (Pakete[i].Status == "abgeholt")
                {
                    counter++;
                }
            }

            if (counter > 0)
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
