//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Controller.cs
//Beschreibung: Businesslogic für die Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//20.11.2019:   Controller funktioniert zur Zeit nicht richtig, da Modelle überarbeitet wurden

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    class Controller
    {
        #region Eigenschaften
        List<Kunde> _Kunden;
        List<Mitarbeiter> _Mitarbeiter;
        List<Paketstation> _Stationen;
        Userinterface _Terminal;
        Object _AktiverUser;
        Paketstation _AktuelleStation;
        bool Authentifizieren;
        #endregion

        #region Accessoren/Modifier
        public List<Kunde> Kunden { get => _Kunden; set => _Kunden = value; }
        public List<Mitarbeiter> Mitarbeiter { get => _Mitarbeiter; set => _Mitarbeiter = value; }
        public Userinterface Terminal { get => _Terminal; set => _Terminal = value; }
        public object AktiverUser { get => _AktiverUser; set => _AktiverUser = value; }
        public bool Authentifizieren1 { get => Authentifizieren; set => Authentifizieren = value; }
        public List<Paketstation> Stationen { get => _Stationen; set => _Stationen = value; }
        public Paketstation AktuelleStation { get => _AktuelleStation; set => _AktuelleStation = value; }
        #endregion

        #region Konstruktoren
        public Controller()
        {
            this.Kunden = null;
            this.Mitarbeiter = null;
            this.Stationen = null;
            this.Terminal = null;
            this.AktiverUser = null;
            this.AktuelleStation = null;
            this.Authentifizieren = false;
        }
        //Spezialkonstruktor
        
        //TODO: mit Parametern
        #endregion

        #region Worker
        public void run()
        {
            //TODO: businesslogic
        }

        public void PaketeListen()
        {

        }

        public void MitarbeiterHoltPakete()
        {

        }

        public void MitarbeiterLiefertPakete()
        {

        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
