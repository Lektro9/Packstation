﻿//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Controller.cs
//Beschreibung: Businesslogik für die Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//20.11.2019:   Controller funktioniert zur Zeit nicht richtig, da Modelle überarbeitet wurden
//29.12.2019:   Businesslogik hinzugefügt und Entwicklung abgeschlossen

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
        int _AnzahlStationen;
        #endregion

        #region Accessoren/Modifier
        public List<Kunde> Kunden { get => _Kunden; set => _Kunden = value; }
        public List<Mitarbeiter> Mitarbeiter { get => _Mitarbeiter; set => _Mitarbeiter = value; }
        public Userinterface Terminal { get => _Terminal; set => _Terminal = value; }
        public object AktiverUser { get => _AktiverUser; set => _AktiverUser = value; }
        public bool Authentifiziert { get => _Authentifiziert; set => _Authentifiziert = value; }
        public List<Paketstation> Stationen { get => _Stationen; set => _Stationen = value; }
        public Paketstation AktuelleStation { get => _AktuelleStation; set => _AktuelleStation = value; }
        public int AnzahlStationen { get => _AnzahlStationen; set => _AnzahlStationen = value; }
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
            this.AnzahlStationen = 0;
        }

        public Controller(int AnzahlStationen)
        {
            this.Kunden = null;
            this.Mitarbeiter = null;
            this.Stationen = null;
            this.Terminal = null;
            this.AktiverUser = null;
            this.AktuelleStation = null;
            this.Authentifiziert = false;
            this.AnzahlStationen = AnzahlStationen;
            PruefeAnzahlPaketstationen(this.AnzahlStationen);
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

        public Controller(List<Kunde> Kunden, List<Mitarbeiter> Mitarbeiter, Userinterface Terminal, object AktiverUser, bool Authentifiziert, List<Paketstation> Stationen)
        {
            this.Kunden = Kunden;
            this.Mitarbeiter = Mitarbeiter;
            this.Terminal = Terminal;
            this.AktiverUser = AktiverUser;
            this.Authentifiziert = Authentifiziert;
            this.Stationen = Stationen;
            this.AktuelleStation = null;
            this.AnzahlStationen = this.Stationen.Count;
        }

        //hier werden automatisch Stationen erstellt, die aber alle nur XS Fächer nutzen
        public Controller(List<Kunde> Kunden, List<Mitarbeiter> Mitarbeiter, Userinterface Terminal, object AktiverUser, bool Authentifiziert, int AnzahlStationen) 
        {
            this.Kunden = Kunden;
            this.Mitarbeiter = Mitarbeiter;
            this.Terminal = Terminal;
            this.AktiverUser = AktiverUser;
            this.Authentifiziert = Authentifiziert;
            this.AnzahlStationen = AnzahlStationen;
            List<Paketstation> Stationen = new List<Paketstation>();
            for (int i = 0; i < this.AnzahlStationen; i++)
            {
                Stationen.Add(new Paketstation(i));
            }
            this.Stationen = Stationen;
            this.AktuelleStation = null;
        }
        #endregion

        #region Worker
        public void run()
        {
            bool running = true;
            string Eingabe;
            int ZahlenEingabe;
            bool ExistiertStation = false;

            SplashinfoAnzeigen();

            while (running)
            {
                while (ExistiertStation == false)
                {
                    Terminal.StationsMenueAnzeigen(this.AnzahlStationen);
                    ZahlenEingabe = Terminal.ZahlEinlesen();
                    if (ZahlenEingabe > 0 && ZahlenEingabe <= this.AnzahlStationen)
                    {
                        ExistiertStation = true;
                        this.AktuelleStation = Stationen[ZahlenEingabe - 1];
                    }
                    else
                    {
                        Terminal.TextAusgeben("Paketstation existiert nicht.");
                    }
                }
                while (Authentifiziert == false)
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
                    else if (Eingabe == "0")
                    {
                        Authentifiziert = false;
                    }
                    else if (Eingabe == "abschalten")
                    {
                        running = false;
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

        public void PaketeListen()
        {
            List<Paket> AbzuholendePakete = AktuelleStation.MitarbeiterListeAbzuholenderPakete();
            for (int i = 0; i < AbzuholendePakete.Count; i++)
            {
                Terminal.TextAusgeben("Paket " + AbzuholendePakete[i].PaketNummer + " liegt in Fach " + AbzuholendePakete[i].PaketfachNr + ".");
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
                string faecherNummern = "";
                for (int i = 0; i < AbzuholendePakete.Count; i++)
                {
                    faecherNummern += AbzuholendePakete[i].PaketfachNr + " ";
                }
                Terminal.TextAusgeben("Sie haben " + AbzuholendePakete.Count + " Pakete aus den Fächern " + faecherNummern + "abgeholt.");
            }
            else
            {
                Terminal.TextAusgeben("Keine Pakete zum Abholen in der Station.");
            }
        }

        public void MitarbeiterLiefertPakete()
        {
            if (AktiverUser.GetType() == typeof(Mitarbeiter))
            {
                Mitarbeiter AktiverMitarbeiter = (Mitarbeiter)AktiverUser;
                List<Paket> EinzubindendePakete = new List<Paket>(AktiverMitarbeiter.PaketeLiefern());
                int EinzubindendePaketeAnzahl = EinzubindendePakete.Count;
                for (int i = EinzubindendePakete.Count - 1; i >= 0; i--)
                {
                    if (EinzubindendePakete.Count > 0)
                    {
                        for (int j = 0; j < AktuelleStation.Paketfach.Count; j++)
                        {
                            if (!AktuelleStation.Paketfach[j].IstBelegt())
                            {
                                AktuelleStation.Paketfach[j].PaketAnnehmen(EinzubindendePakete[i]);
                                Terminal.TextAusgeben("Paket " + EinzubindendePakete[i].PaketNummer + " wurde in das Fach " + EinzubindendePakete[i].PaketfachNr + " eingelegt.");
                                EinzubindendePakete.RemoveAt(i);
                                break;
                            }
                            else
                            {
                                // do nothing
                            }
                        }
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
                    Terminal.TextAusgeben("Keine weiteren Pakete zum hineinlegen verfügbar.");
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
                Paket p = AktuellerKunde.PaketEinliefern();
                AktuelleStation.KundeLiefertPaket(p);
                Terminal.TextAusgeben("Paket " + p.PaketNummer + " wurde in das Fach " + p.PaketfachNr + " eingelegt.");
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

        public void PruefeAnzahlPaketstationen(int Anzahl)
        {
            if (this.AnzahlStationen > 0 && this.AnzahlStationen < 101)
            {
                // nichts tun
            }
            else
            {
                throw new ArgumentException("Anzahl der Stationen darf nicht kleiner als 1 und nicht höher als 100 sein.");
            }
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
