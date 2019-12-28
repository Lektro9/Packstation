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
    public class Controller
    {
        #region Eigenschaften
        List<Kunde> _Kunden;
        List<Mitarbeiter> _Mitarbeiter;
        List<Paketstation> _Stationen;
        Userinterface _Terminal;
        object _AktiverUser;
        Paketstation _AktuelleStation;
        bool _Authentifiziert;
        #endregion

        #region Accessoren/Modifier
        public List<Kunde> Kunden { get => _Kunden; set => _Kunden = value; }
        public List<Mitarbeiter> Mitarbeiter { get => _Mitarbeiter; set => _Mitarbeiter = value; }
        public Userinterface Terminal { get => _Terminal; set => _Terminal = value; }
        public object AktiverUser { get => _AktiverUser; set => _AktiverUser = value; }
        public bool Authentifiziert { get => _Authentifiziert; set => _Authentifiziert = value; }
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
            this.Authentifiziert = false;
        }

        //Spezialkonstruktor
        public Controller(List<Kunde> Kunden, List<Mitarbeiter> Mitarbeiter, Userinterface Terminal, object AktiverUser, bool Authentifiziert, List<Paketstation> Stationen, Paketstation AktuelleStation)
        {
            this.Kunden = Kunden;
            this.Mitarbeiter = Mitarbeiter;
            this.Terminal = Terminal;
            this.AktiverUser = AktiverUser;
            this.Authentifiziert = Authentifiziert;
            this.Stationen = Stationen;
            this.AktuelleStation = AktuelleStation;
        }
        #endregion

        #region Worker
        public void run()
        {
            bool running = true;
            string Eingabe;
            SplashinfoAnzeigen();

            while (running)
            {
                while (Authentifiziert == false) //TODO: vielleicht option zum Abschalten geben?
                {
                    Authentifizieren();
                }
                if (AktiverUser.GetType() == typeof(Mitarbeiter))
                {
                    Terminal.MitarbeiterMenueAnzeigen();
                    Eingabe = Terminal.TextEinlesen();
                    if (Eingabe == "1")
                    {
                        MitarbeiterHoltPakete();
                    }
                    else if (Eingabe == "2")
                    {
                        MitarbeiterLiefertPakete();
                    }
                    else if(Eingabe == "0")
                    {
                        Authentifiziert = false;
                    }
                }
                else
                {
                    Terminal.KundenMenueAnzeigen();
                    Eingabe = Terminal.TextEinlesen();
                    if (Eingabe == "1")
                    {
                        KundeHoltPaket();
                    }
                    else if (Eingabe == "2")
                    {
                        KundeLiefertPaket();
                    }
                    else if (Eingabe == "0")
                    {
                        Authentifiziert = false;
                    }
                }
                
            }
        }

        public void PaketeListen() // von wem die Pakete listen?
        {
            for (int i = 0; i < AktuelleStation.Paketfach.Count; i++)
            {

            }
        }

        public void MitarbeiterHoltPakete()
        {
            Mitarbeiter AktiverMitarbeiter = (Mitarbeiter)AktiverUser;
            List<Paket> AbzuholendePakete = AktuelleStation.MitarbeiterListeAbzuholenderPakete();

            for (int i = 0; i < AbzuholendePakete.Count; i++)
            {
                AbzuholendePakete[i].aendereStatus("Transport");
                AktiverMitarbeiter.AbgeholtePakete.Add(AbzuholendePakete[i]);
            }

            if (AbzuholendePakete.Count > 0)
            {
                Terminal.TextAusgeben("Sie haben " + AbzuholendePakete.Count + " Pakete abgeholt.");
            }

            /*           for (int i = 0; i < AktuelleStation.Paketfach.Count; i++)
                       {
                           if (AktuelleStation.Paketfach[i].IstBelegt())
                           {
                               if (AktuelleStation.Paketfach[i].Packet.Status == "abzuholen")
                               {
                                   if (AktiverUser.GetType() == typeof(Mitarbeiter))
                                   {

                                       AktiverMitarbeiter.AbgeholtePakete.Add(AktuelleStation.Paketfach[i].getPaket());
                                   }
                               }
                           }
                       }*/
        }

        public void MitarbeiterLiefertPakete()
        {
            if (AktiverUser.GetType() == typeof(Mitarbeiter))
            {
                Mitarbeiter AktiverMitarbeiter = (Mitarbeiter)AktiverUser;
                List<Paket> EinzubindendePakete = new List<Paket>(AktiverMitarbeiter.PaketeLiefern());
                int EinzubindendePaketeAnzahl = EinzubindendePakete.Count;
                for (int i = 0; i < EinzubindendePakete.Count; i++)
                {
                    if (!AktuelleStation.Paketfach[i].IstBelegt())
                    {
                        AktuelleStation.Paketfach[i].PaketAnnehmen(EinzubindendePakete[i]);
                        EinzubindendePakete.RemoveAt(i);
                        
                    }
                    else
                    {
                        // do nothing
                    }
                }

                //Sichergehen dass alle Pakete ein Fach erhalten haben
                if (EinzubindendePakete.Count != 0)
                {
                    Terminal.TextAusgeben("Keine freien Fächer mehr verfügbar");
                    // Pakete dem Mitarbeiter zurück geben
                    for (int i = 0; i < EinzubindendePakete.Count; i++)
                    {
                        EinzubindendePakete[i].aendereStatus("Transport");
                        AktiverMitarbeiter.LieferPakete.Add(EinzubindendePakete[i]);
                    }
                }
                else
                {
                    // nichts tun, alles verläuft nach Plan
                }
            }
        }

        public void KundeHoltPaket()
        {
            Kunde AktuellerKunde = (Kunde)AktiverUser;
            if (AktuelleStation.IsteinPaketvorhanden(AktuellerKunde))
            {
                for (int i = 0; i < AktuelleStation.Paketfach.Count; i++)
                {
                    if (AktuelleStation.Paketfach[i].IstBelegt())
                    {
                        Terminal.TextAusgeben("Das Paket (" + AktuelleStation.Paketfach[i].Packet.PaketNummer + ") wurde aus dem Fach " + AktuelleStation.getPaketFachnummer(AktuellerKunde) + " entnommen.");
                        AktuellerKunde.PaketAbholen(AktuelleStation.Paketfach[i].getPaket());
                        break;
                    }
                    else
                    {
                        //nichts tun
                    }
                }
                    
            }
            else
            {
                Terminal.TextAusgeben("Keine Pakete in dieser Station für Sie verfügbar.");
            }
            
        }

        public void KundeLiefertPaket()
        {
            Kunde AktuellerKunde = (Kunde)AktiverUser;
            if (AktuellerKunde.hatPaketabzugeben())
            {
                AktuelleStation.KundeLiefertPaket(AktuellerKunde.PaketEinliefern());
            }
            else
            {
                Terminal.TextAusgeben("Sie besitzen keine Pakete zum Abgeben.");
                Terminal.WeiterMitTaste();
            }
        }

        public void SplashinfoAnzeigen()
        {
            Terminal.SplashAnzeigen();
        }

        public void Authentifizieren()
        {
            string Benutzername = "";
            string Passwort = "";
            Terminal.loginAufforderung(ref Benutzername, ref Passwort);

            for (int i = 0; i < Mitarbeiter.Count; i++)
            {
                if (Benutzername == Mitarbeiter[i].Benutzername && Passwort == Mitarbeiter[i].Passwort)
                {
                    AktiverUser = Mitarbeiter[i];
                    Authentifiziert = true;
                    break;
                }
                else
                {
                    // nichts tun
                }
            }

            for (int i = 0; i < Kunden.Count; i++)
            {
                if (Benutzername == Kunden[i].Benutzername && Passwort == Kunden[i].Passwort)
                {
                    AktiverUser = Kunden[i];
                    Authentifiziert = true;
                    break;
                }
                else
                {
                    // nichts tun
                }
            }

            if (Authentifiziert == false)
            {
                Terminal.TextAusgeben("Falscher Login, bitte prüfen Sie Ihren Benutzernamen und Ihr Passwort.");
            }
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
