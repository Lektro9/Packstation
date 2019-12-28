//Autor:        Kroll
//Datum:        25.09.2019
//Dateiname:    Controller.cs
//Beschreibung: Verwaltet die Daten
//Änderungen:
//25.09.2019:   Entwicklungsbeginn 

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    class main
    {
        static void Main(string[] args)
        {
            //Kunden
            Paket p1 = new Paket(1L, "Klaus", "Beispielstraße 22", "Bernd", "AbsenderStr. 22", "Verschicken", 1, 1);
            List<Paket> KundenPakete1 = new List<Paket>();
            KundenPakete1.Add(p1);
            Kunde k1 = new Kunde(1L, "Klaus", "Beispielstraße 22", "Klausi", "1234", KundenPakete1);

            //Mitarbeiter
            Paket p2 = new Paket(2L, "Susi", "PaketAbsenderstr. 12", "Klaus", "Beispielstraße 22", "Transport", -1, -1);
            List<Paket> MitarbeiterLieferPakete = new List<Paket>();
            MitarbeiterLieferPakete.Add(p2);
            Paket p3 = new Paket(2L, "Daniela", "PaketAbsenderstr. 16", "Scharlotte", "EmpfaengerStr. 5", "Transport", -1, -1);
            List<Paket> MitarbeiterAbgeholtePakete = new List<Paket>();
            MitarbeiterAbgeholtePakete.Add(p3);

            Mitarbeiter m1 = new Mitarbeiter(1L, "John", "admin", "admin", MitarbeiterLieferPakete, MitarbeiterAbgeholtePakete);

            //UserInterface
            Userinterface ui1 = new Userinterface();

            //Paketstation
            Paketstation ps1 = new Paketstation();

            //Kunden und Mitarbeiterliste
            List<Kunde> kl1 = new List<Kunde>();
            kl1.Add(k1);
            List<Mitarbeiter> ml1 = new List<Mitarbeiter>();
            ml1.Add(m1);

            //Controller
            Controller Verwalter = new Controller(kl1, ml1, ui1, null, false, null, ps1);
            Verwalter.run();
        }
    }
}
