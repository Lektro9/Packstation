//Autor:        Kroll
//Datum:        25.09.2019
//Dateiname:    Controller.cs
//Beschreibung: Verwaltet die Daten
//Änderungen:
//25.09.2019:   Entwicklungsbeginn 
//29.12.2019:   Testdaten hinzugefügt und Entwicklung abgeschlossen

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    class main
    {
        static void Main(string[] args)
        {
            Paket p1; //Paket für Kunden zum Abgeben
            Paket p2; //Paket für Mitarbeiter welches geliefert werden muss
            Paket p3; //Paket für Mitarbeiter welches abgeholt wurde
            Paket p4; //Paket in Station welches abgeholt werden soll ("abzuholen")
            Paket p5; //Paket in Station welches abgeholt werden soll ("abzuholen")
            Paket p6; //Paket für Mitarbeiter welches geliefert werden muss
            Paket p7; //Paket für Mitarbeiter welches geliefert werden muss 
            Paket p8; //Paket für Kunden zum Abgeben

            Fach f1;
            Fach f2;
            Fach f3;
            Fach f4;
            Fach f5;
            Fach f6;
            Fach f7;
            Fach f8;
            Fach f9;
            Fach f10;

            List<Fach> FaecherListe;
            List<Paketstation> StationenListe;

            List<Paket> KundenPakete1;
            Kunde k1;
            List<Kunde> kl1;
            Mitarbeiter m1;
            List<Mitarbeiter> ml1;
            List<Paket> MitarbeiterLieferPakete;
            List<Paket> MitarbeiterAbgeholtePakete;
            Userinterface ui1;
            Controller Verwalter;

            //Pakete initialisieren
            p1 = new Paket(1L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);
            p2 = new Paket(2L, "Susi", "PaketAbsenderstr. 12", "Klaus", "Beispielstraße 22", "Transport", -1, -1, Groesse.XS);
            p3 = new Paket(3L, "Daniela", "PaketAbsenderstr. 16", "Scharlotte", "EmpfaengerStr. 5", "Transport", -1, -1, Groesse.XS);
            p4 = new Paket(4L, "BeispielAbsName", "BeispielAbsenderAddr. 16", "BeispielEmpfName", "EmpfaengerStr. 5", "Abholen", -1, -1, Groesse.XS);
            p5 = new Paket(5L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Abholen", -1, -1, Groesse.XS);
            p6 = new Paket(6L, "BeispielAbsName", "BeispielAbsenderAddr. 16", "Klaus", "Beispielstraße 22", "Transport", -1, -1, Groesse.XS);
            p7 = new Paket(7L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "Klaus", "Beispielstraße 22", "Transport", -1, -1, Groesse.XS);
            p8 = new Paket(8L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);

            //StandartFächer für jede Station
            f1 = new Fach(1, true, null, false, Groesse.XS);
            f2 = new Fach(2, true, null, false, Groesse.XS);
            f3 = new Fach(3, true, null, false, Groesse.XS);
            f4 = new Fach(4, true, null, false, Groesse.M);
            f5 = new Fach(5, true, null, false, Groesse.M);
            f6 = new Fach(6, true, null, false, Groesse.M);
            f7 = new Fach(7, true, null, false, Groesse.XXL);
            f8 = new Fach(7, true, null, false, Groesse.XXL);
            f9 = new Fach(7, true, null, false, Groesse.XXL);
            
            f10 = new Fach(10, true, null, false, Groesse.S);

            //Pakete in entsprechende Listen hinzufügen
            KundenPakete1 = new List<Paket>();
            KundenPakete1.Add(p1);
            KundenPakete1.Add(p8);
            MitarbeiterLieferPakete = new List<Paket>();
            MitarbeiterLieferPakete.Add(p2);
            MitarbeiterLieferPakete.Add(p6);
            MitarbeiterLieferPakete.Add(p7);
            MitarbeiterAbgeholtePakete = new List<Paket>();
            MitarbeiterAbgeholtePakete.Add(p3);

            //Kunde mit Paket initialisieren (Login=Klausi, Passwort=1234)
            k1 = new Kunde(1L, "Klaus", "Beispielstraße 22", "Klausi", "1234", KundenPakete1);

            //Kundenliste initialisieren
            kl1 = new List<Kunde>();
            kl1.Add(k1);

            //Mitarbeiter mit Paketen initialisieren (Login=admin, Passwort=admin)
            m1 = new Mitarbeiter(1L, "John", "admin", "admin", MitarbeiterLieferPakete, MitarbeiterAbgeholtePakete);

            //Ersatzfach dem Mitarbeiter geben
            m1.ErsatzFaecher.Add(f10);

            //Mitarbeiterliste initialisieren
            ml1 = new List<Mitarbeiter>();
            ml1.Add(m1);

            //Paketstation, UI und Controller initialisieren
            ui1 = new Userinterface();
            FaecherListe = new List<Fach>(){
                f1, f2, f3, f4, f5, f6, f7, f8, f9
            };
            StationenListe = new List<Paketstation>()
            {
                new Paketstation(1, FaecherListe, ui1),
            };

            //Controller
            Verwalter = new Controller(kl1, ml1, ui1, null, false, StationenListe);
            Verwalter.run();
        }
    }
}
