//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Fach.cs
//Beschreibung: Fach für die Pakete in der Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Packstation_Kroll;

namespace UnitTestPackstation
{
    public class TestController : Controller
    {
        public TestController(List<Kunde> Kunden, List<Mitarbeiter> Mitarbeiter, Userinterface Terminal, object AktiverUser, bool Authentifiziert, List<Paketstation> Stationen, Paketstation AktuelleStation)
        {
            this.Kunden = Kunden;
            this.Mitarbeiter = Mitarbeiter;
            this.Terminal = Terminal;
            this.AktiverUser = AktiverUser;
            this.Authentifiziert = Authentifiziert;
            this.Stationen = Stationen;
            this.AktuelleStation = AktuelleStation;
        }
        public void Authentifizieren(string Benutzername, string Passwort)
        {
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
                Console.WriteLine("Falscher Login, bitte prüfen Sie Ihren Benutzernamen und Ihr Passwort.");
            }
        }
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PaketVerschicken()
        {
            Paket testPaket = new Paket();
            testPaket.aendereStatus("Verschicken");
            Assert.AreEqual(testPaket.Status, "Verschicken");
        }

        [TestMethod]
        public void PaketTransport()
        {
            Paket testPaket = new Paket();
            testPaket.aendereStatus("Transport");
            Assert.AreEqual(testPaket.Status, "Transport");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "You must supply an argument")]
        public void PaketFalscherStatus()
        {
            Paket testPaket = new Paket();
            testPaket.aendereStatus("nein");
        }

        [TestMethod]
        public void FachAggregationFunction()
        {
            Paket testPaket = new Paket();
            Fach fach = new Fach(12, true, testPaket, true);
            fach.getPaket();
            Assert.AreEqual(fach.Packet, null);
            Assert.AreEqual(fach.IstBelegt(), false);
        }

        [TestMethod]
        public void KundenPaketListe_hinzufuegen()
        {
            Paket testPaket = new Paket();
            List<Paket> PaketListe = new List<Paket>();
            PaketListe.Add(testPaket);
            Kunde TestKlaus = new Kunde(1L, "Klaus", "Berndstr. 54", "Klausi", "0000", PaketListe);
            Paket shouldBeInStation = TestKlaus.PaketEinliefern();

            Assert.AreEqual(shouldBeInStation.Status, "abzuholen");
        }

        [TestMethod]
        public void KundenAuthentifizierung()
        {
            List<Paket> PaketListe = new List<Paket>();
            Kunde TestKlaus = new Kunde(1L, "Klaus", "Berndstr. 54", "Klausi", "123456", PaketListe);
            
            Assert.AreEqual(TestKlaus.Authentifizieren("Klausi", "123456"), true);
        }

        [TestMethod]
        public void KundenHatPaketAbzugeben()
        {
            List<Paket> PaketListe = new List<Paket>();
            Paket p = new Paket("Verschicken");
            PaketListe.Add(p);
            Kunde TestKlaus = new Kunde(1L, "Klaus", "Berndstr. 54", "Klausi", "123456", PaketListe);
            
            Assert.AreEqual(TestKlaus.hatPaketabzugeben(), true);
        }

        [TestMethod]
        public void KundenHatPaketAbgeholt()
        {
            List<Paket> PaketListe = new List<Paket>();
            Paket p = new Paket("abgeholt");
            PaketListe.Add(p);
            Kunde TestKlaus = new Kunde(1L, "Klaus", "Berndstr. 54", "Klausi", "123456", PaketListe);

            Assert.AreEqual(TestKlaus.hatPaketeabgeholt(), true);
        }
        
        [TestMethod]
        
        public void AuthentifizierungsTest()
        {
            Kunde k1 = new Kunde(1L, "Klaus", "Beispielstraße 22", "Klausi", "1234", null);
            Mitarbeiter m1 = new Mitarbeiter(1L, "John", "admin", "admin", null, null);
        
            List<Kunde> kl1 = new List<Kunde>();
            kl1.Add(k1);
            List<Mitarbeiter> ml1 = new List<Mitarbeiter>();
            ml1.Add(m1);

            //Controller
            TestController Verwalter = new TestController(kl1, ml1, null, null, false, null, null);
            Verwalter.Authentifizieren(k1.Benutzername, k1.Passwort);

            Assert.AreEqual(Verwalter.AktiverUser.GetType(), typeof(Kunde));
        }

        //TODO: Controller.MitarbeiterLiefertPakete() testen! (ob fehler abgefangen werden)
    }
}
