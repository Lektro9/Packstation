//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Metacontroller.cs
//Beschreibung: ein zentraler Controller
//Änderungen:
//27.01.2020:   Entwicklungsbeginn
//28.01.2020:   Entwicklung 2.0 abgeschlossen

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Packstation_Kroll
{
    public class Metacontroller
    {
        #region Eigenschaften
        List<Kunde> _Kunden;
        List<Mitarbeiter> _Mitarbeiter;
        List<Geschaeftsfuehrer> _Geschaeftsfuehrer;
        List<Stationscontroller> _Stationen;
        Userinterface _Terminal;
        bool _Authentifiziert;
        int _AnzahlStationen;
        object _AktiverUser;
        #endregion

        #region Accessoren/Modifier
        public List<Kunde> Kunden { get => _Kunden; set => _Kunden = value; }
        public List<Mitarbeiter> Mitarbeiter { get => _Mitarbeiter; set => _Mitarbeiter = value; }
        public List<Geschaeftsfuehrer> Geschaeftsfuehrer { get => _Geschaeftsfuehrer; set => _Geschaeftsfuehrer = value; }
        public List<Stationscontroller> Stationen { get => _Stationen; set => _Stationen = value; }
        public Userinterface Terminal { get => _Terminal; set => _Terminal = value; }
        public bool Authentifiziert { get => _Authentifiziert; set => _Authentifiziert = value; }
        public int AnzahlStationen { get => _AnzahlStationen; set => _AnzahlStationen = value; }
        public object AktiverUser { get => _AktiverUser; set => _AktiverUser = value; }
        #endregion

        #region Konstruktoren
        public Metacontroller()
        {
            this.Kunden = null;
            this.Mitarbeiter = null;
            this.Geschaeftsfuehrer = null;
            this.Stationen = null;
            this.Terminal = null;
            this.Authentifiziert = false;
            this.AnzahlStationen = 0;
            this.AktiverUser = null;
        }

        //Spezialcontroller
        //Konstruktor mit Stationenliste um Controllern Stationen zuzuweisen

        public Metacontroller(List<Kunde> Kunden, List<Mitarbeiter> Mitarbeiter, List<Geschaeftsfuehrer> Geschaeftsfuehrer, List<Paketstation> Paketstationen, Userinterface Terminal)
        {
            this.Kunden = Kunden;
            this.Mitarbeiter = Mitarbeiter;
            this.Geschaeftsfuehrer = Geschaeftsfuehrer;
            this.Stationen = new List<Stationscontroller>();
            this.Terminal = Terminal;
            this.Authentifiziert = false;
            this.AktiverUser = null;

            for (int i = 0; i < Paketstationen.Count; i++)
            {
                this.Stationen.Add(new Stationscontroller(this.Kunden, this.Mitarbeiter, this.Terminal, null, false, Paketstationen[i]));
            }
            this.AnzahlStationen = this.Stationen.Count;
            PruefeAnzahlPaketstationen();
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
                Terminal.MetaMenueAnzeigen(this.AnzahlStationen);
                Eingabe = Terminal.TextEinlesen();
                if (Eingabe == "1")
                {
                    while (ExistiertStation == false)
                    {
                        Terminal.TextAusgeben("Geben Sie eine Stationsnummer ein.");
                        Eingabe = Terminal.TextEinlesen();

                        while (!Int32.TryParse(Eingabe, out ZahlenEingabe))
                        {
                            Terminal.TextAusgeben("Sie haben keine Zahl eingegeben. Weiter mit einer beliebigen Taste...");
                            Terminal.WeiterMitTaste();
                            Terminal.MetaMenueAnzeigen(this.AnzahlStationen);
                            Eingabe = Terminal.TextEinlesen();
                        }

                        if (ZahlenEingabe > 0 && ZahlenEingabe <= this.AnzahlStationen)
                        {
                            ExistiertStation = true;
                            this.Stationen[ZahlenEingabe - 1].run();
                        }
                        else
                        {
                            Terminal.TextAusgeben("Paketstation existiert nicht.");
                            Terminal.WeiterMitTaste();
                        }
                    }
                    ExistiertStation = false;
                }
                else if (Eingabe == "2")
                {
                    while (this.Authentifiziert == false)
                    {
                        AuthentifiziereGF();
                    }

                    while (this.Authentifiziert)
                    {
                        Terminal.GeschaeftsfuehrerMenueAnzeigen();
                        Eingabe = Terminal.TextEinlesen();
                        if (Eingabe == "1")
                        {
                            Terminal.StationHinzufuegenMenueAnzeigen();
                            Eingabe = Terminal.TextEinlesen();
                            StationHinzufuegen(Eingabe);
                        }
                        else if(Eingabe == "2")
                        {
                            Terminal.StationEntfernenMenueAnzeigen(Stationen);
                            Eingabe = Terminal.TextEinlesen();
                            StationEntfernen(Eingabe);
                        }
                        else if(Eingabe == "3")
                        {
                            Terminal.StationErweiterungsMenueAnzeigen(Stationen);
                            Eingabe = Terminal.TextEinlesen();
                            int scNummer = StationErweitern(Eingabe);
                            if (scNummer > -1)
                            {
                                Terminal.StationErweiterungsMenueAnzeigen();
                                Eingabe = Terminal.TextEinlesen();
                                if (Eingabe == "1")
                                {
                                    Terminal.StationFachEntfernenMenueAnzeigen(scNummer, Stationen[scNummer-1]);
                                    Eingabe = Terminal.TextEinlesen();
                                    FachEntfernen(Eingabe, Stationen[scNummer - 1]);
                                }
                                else if (Eingabe == "2")
                                {
                                    Terminal.StationFachHinzufuegenMenueAnzeigen(scNummer, Stationen[scNummer - 1]);
                                    Eingabe = Terminal.TextEinlesen();
                                    FachHinzufuegen(Eingabe, Stationen[scNummer - 1]);
                                }
                            }
                        }
                        else if(Eingabe == "4") //Mitarbeiter Verwalten
                        {
                            bool MitarbeiterMenuRunning = true;

                            while (MitarbeiterMenuRunning)
                            {
                                Terminal.MitarbeiterVerwaltenMenueAnzeigen();
                                Eingabe = Terminal.TextEinlesen();

                                if (Eingabe == "1")
                                {
                                    Terminal.MitarbeiterHinzufuegenMenueAnzeigen();
                                    Eingabe = Terminal.TextEinlesen();
                                    MitarbeiterHinzufuegen(Eingabe);
                                }
                                else if (Eingabe == "2")
                                {
                                    Terminal.MitarbeiterEntfernenMenueAnzeigen(this.Mitarbeiter);
                                    Eingabe = Terminal.TextEinlesen();
                                    MitarbeiterEntfernen(Eingabe);
                                }
                                else if (Eingabe == "0")
                                {
                                    MitarbeiterMenuRunning = false;
                                }
                                else
                                {
                                    Terminal.TextAusgeben("Falsche Eingabe! Weiter mit beliebiger Taste.");
                                    Terminal.WeiterMitTaste();
                                }
                            }
                        }
                        else if(Eingabe == "5") //Kunden Verwalten
                        {
                            bool KundenMenuRunning = true;

                            while (KundenMenuRunning)
                            {
                                Terminal.KundenVerwaltenMenueAnzeigen();
                                Eingabe = Terminal.TextEinlesen();

                                if (Eingabe == "1")
                                {
                                    Terminal.KundeHinzufuegenMenueAnzeigen();
                                    Eingabe = Terminal.TextEinlesen();
                                    KundeHinzufuegen(Eingabe);
                                }
                                else if (Eingabe == "2")
                                {
                                    Terminal.KundeEntfernenMenueAnzeigen(this.Kunden);
                                    Eingabe = Terminal.TextEinlesen();
                                    KundeEntfernen(Eingabe);
                                }
                                else if (Eingabe == "0")
                                {
                                    KundenMenuRunning = false;
                                }
                                else
                                {
                                    Terminal.TextAusgeben("Falsche Eingabe! Weiter mit beliebiger Taste.");
                                    Terminal.WeiterMitTaste();
                                }
                            }
                        }
                        else if(Eingabe == "0")
                        {
                            this.Authentifiziert = false;
                        }
                    }
                }
                else if (Eingabe == "abschalten")
                {
                    running = false;
                }
            }
        }

        public void SplashinfoAnzeigen()
        {
            Terminal.SplashAnzeigen();
        }

        public void PruefeAnzahlPaketstationen()
        {
            if (this.AnzahlStationen > 0 && this.AnzahlStationen < 101)
            {
                //nichts tun
            }
            else
            {
                throw new ArgumentException("Anzahl der Stationen darf nicht kleiner als 1 und nicht höher als 100 sein.");
            }
        }

        public void StationHinzufuegen(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\d+)\s(\d+)$");

            List<string> matches = MatchRegexRules(Eingabe, regEx);

            if (matches.Count == 3 && Int32.Parse(matches[2]) > 8)
            {
                //TODO: auf IDs prüfen

                Paketstation p = new Paketstation(Int32.Parse(matches[1]), Int32.Parse(matches[2]), this.Terminal);
                Stationscontroller sc = new Stationscontroller(this.Kunden, this.Mitarbeiter, this.Terminal, null, false, p);
                this.Stationen.Add(sc);
                this.AnzahlStationen += 1;
                PruefeAnzahlPaketstationen();
                Terminal.TextAusgeben("Eine neue Station wurde hinzugefügt. Die Station ist mit der Nummer " + Stationen.Count + " zu erreichen.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Keine valide Eingabe. Weiter mit beliebiger Taste...");
                Terminal.WeiterMitTaste();
            }
            
        }

        public List<string> MatchRegexRules(string Eingabe, Regex regEx)
        {
            Match match = regEx.Match(Eingabe);
            GroupCollection data = match.Groups;
            List<string> matches = new List<string>();

            if (match.Success)
            {
                foreach (string groupName in regEx.GetGroupNames())
                {
                    matches.Add(match.Groups[groupName].Value);
                }
            }
            else
            {
                //nichts tun und leere Liste zurückgeben
            }

            return matches;
        }

        public Stationscontroller StationEntfernen(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\d+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);
            Stationscontroller retVal = null;

            if (matches.Count == 2)
            {
                retVal = Stationen[Int32.Parse(matches[0]) - 1];
                Stationen.Remove(Stationen[Int32.Parse(matches[0]) - 1]);
                Terminal.TextAusgeben("Station wurde erfolgreich entfernt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Station konnte nicht gefunden werden. Halten Sie sich bitte an die Liste.");
                Terminal.WeiterMitTaste();
            }
            return retVal;
        }

        public void AuthentifiziereGF()
        {
            string Benutzername = "";
            string Passwort = "";
            Terminal.loginAufforderung(ref Benutzername, ref Passwort);

            for (int i = 0; i < Mitarbeiter.Count; i++)
            {
                if (Benutzername == Geschaeftsfuehrer[i].Benutzername && Passwort == Geschaeftsfuehrer[i].Passwort)
                {
                    AktiverUser = Geschaeftsfuehrer[i];
                    Authentifiziert = true;
                    break;
                }
                else
                {
                    //nichts tun
                }
            }

            if (Authentifiziert == false)
            {
                Terminal.TextAusgeben("Falscher Login, bitte prüfen Sie Ihren Benutzernamen und Ihr Passwort.");
                Terminal.WeiterMitTaste();
            }
        }

        public void MitarbeiterHinzufuegen(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\w+)\s(\w+)\s(\w+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);

            if (matches.Count == 4)
            {
                Mitarbeiter m = new Mitarbeiter(this.Mitarbeiter.Count + 1, matches[1], matches[2], matches[3], new List <Paket>(), new List<Paket>());
                this.Mitarbeiter.Add(m);
                Terminal.TextAusgeben("Mitarbeiter wurde hinzugefügt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Format nicht richtig beachtet, bitte noch einmal versuchen.");
                Terminal.WeiterMitTaste();
            }
        }

        public Mitarbeiter MitarbeiterEntfernen(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\d+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);
            Mitarbeiter retVal = null;

            if (matches.Count == 2 && Int32.Parse(matches[0]) > 0 && Int32.Parse(matches[0]) <= this.Mitarbeiter.Count)
            {
                retVal = this.Mitarbeiter[Int32.Parse(matches[0]) - 1];
                this.Mitarbeiter.Remove(this.Mitarbeiter[Int32.Parse(matches[0]) - 1]);
                Terminal.TextAusgeben("Mitarbeiter wurde erfolgreich entfernt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Mitarbeiter konnte nicht gefunden werden. Halten Sie sich bitte an die Liste.");
                Terminal.WeiterMitTaste();
            }
            return retVal;
        }

        public void KundeHinzufuegen(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\w+)\s(\w+)\s(\w+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);

            if (matches.Count == 4)
            {
                Kunde k = new Kunde(this.Kunden.Count + 1, matches[1], matches[2], matches[3], new List<Paket>());
                this.Kunden.Add(k);
                Terminal.TextAusgeben("Kunde wurde hinzugefügt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Format nicht richtig beachtet, bitte noch einmal versuchen.");
                Terminal.WeiterMitTaste();
            }
        }

        public Kunde KundeEntfernen(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\d+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);
            Kunde retVal = null;

            if (matches.Count == 2 && Int32.Parse(matches[0]) > 0 && Int32.Parse(matches[0]) <= this.Kunden.Count)
            {
                retVal = this.Kunden[Int32.Parse(matches[0]) - 1];
                this.Kunden.Remove(this.Kunden[Int32.Parse(matches[0]) - 1]);
                Terminal.TextAusgeben("Kunde wurde erfolgreich entfernt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Kunde konnte nicht gefunden werden. Halten Sie sich bitte an die Liste.");
                Terminal.WeiterMitTaste();
            }
            return retVal;
        }

        public int StationErweitern(string Eingabe)
        {
            Regex regEx = new Regex(@"^(\d+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);
            int retVal = -1;

            if (matches.Count == 2 && Int32.Parse(matches[1])-1 < Stationen.Count)
            {
                retVal = Int32.Parse(matches[1]);
                Terminal.TextAusgeben("Station '" + (Int32.Parse(matches[1])) + "' wurde ausgewählt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Station konnte nicht gefunden werden. Halten Sie sich bitte an die Liste.");
                Terminal.WeiterMitTaste();
            }
            return retVal;
        }
        
        public Fach FachEntfernen(string Eingabe, Stationscontroller sc)
        {
            Regex regEx = new Regex(@"^(\d+)$");
            List<string> matches = MatchRegexRules(Eingabe, regEx);
            Fach retVal = null;

            if (matches.Count == 2 && Int32.Parse(matches[1]) - 1 < sc.AktuelleStation.Paketfach.Count)
            {
                retVal = sc.AktuelleStation.Paketfach[Int32.Parse(matches[1]) - 1];
                sc.AktuelleStation.Paketfach.Remove(retVal);
                sc.AktuelleStation.pruefeAnzahlFaecher();
                Terminal.TextAusgeben("Fach '" + (Int32.Parse(matches[1])) + "' wurde entfernt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Fach konnte nicht gefunden werden. Halten Sie sich bitte an die Liste.");
                Terminal.WeiterMitTaste();
            }
            return retVal;
        }

        public void FachHinzufuegen(string Eingabe, Stationscontroller sc)
        {
            Groesse groesse;
            if (Enum.TryParse<Groesse>(Eingabe, out groesse))
            {
                Fach f = new Fach(sc.AktuelleStation.Paketfach.Count + 1, groesse);
                sc.AktuelleStation.FuegeFachHinzu(f);
                Terminal.TextAusgeben("Fach '" + f.Nummer + "' mit der Größe " + groesse + " wurde erstellt.");
                Terminal.WeiterMitTaste();
            }
            else
            {
                Terminal.TextAusgeben("Fachgröße exisitiert nicht, bitte die Liste der möglichen Größen prüfen.");
                Terminal.WeiterMitTaste();
            }
        }

        #endregion

        #region Schnittstellen
        #endregion
    }
}
