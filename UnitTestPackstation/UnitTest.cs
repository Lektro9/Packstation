//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    UnitTest.cs
//Beschreibung: Unit tests um einzelne Klassen und später alles gemeinsam zu testen
//              unittest fix (visual studio 2017)
//              - right click UnitTestPackstation
//              - Properties
//              - choose "Build"
//              - change "Platform target" to "any CPU" (or try out other settings)
//
//              - maybe you also have to change:
//              - "Test"
//              - "Test Settings"
//              - "Default Processor Architecture"
//              - change to x86 or x64
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 
//29.12.2019:   Entwicklung abgeschlossen 

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Packstation_Kroll;

namespace UnitTestPackstation
{
    //Erstellen einer eigenen Userinterfaces für Tests, um Readline() und Readkey() zu simulieren
    public class TestUI : Userinterface
    {
        public List<String> LinesToRead = new List<String>();

        public override void WeiterMitTaste()
        {
            // nichts um weiter zu kommen
        }

        public override string TextEinlesen()
        {
            string Eingabe = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return Eingabe;
        }
    }
    [TestClass]
    public class Einzelobjekt_tests
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
            Fach fach = new Fach(12, true, testPaket, true, Groesse.XS);
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
        [ExpectedException(typeof(ArgumentException), "Anzahl Faecher stimmt nicht.")]
        public void PaketstationFalscheAnzahlAnFaechern()
        {
            TestUI testUI = new TestUI();
            Paketstation ps1 = new Paketstation(1, 101, testUI);
        }

        [TestMethod]
        public void PruefePaketGroesse()
        {
            Paket p = new Paket(1L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.S);

            Assert.AreEqual(1, (int) p.Groesse);
        }

        [TestMethod]
        public void PruefeObFachHinzugefuegt()
        {
            TestUI testUI = new TestUI();
            Paketstation ps1 = new Paketstation(1, 50, testUI);
            Fach fXL = new Fach(10, true, null, false, Groesse.XL);
            ps1.FuegeFachHinzu(fXL);

            Assert.AreEqual(ps1.Paketfach.Count, 51);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Anzahl Stationen stimmt nicht")]
        public void PruefeAnzahlStationen()
        {
            Controller Verwalter = new Controller(0);
        }
    }
    [TestClass]
    public class Zusammenhaengene_tests
    {
        Paket p1; //Paket für Kunden zum Abgeben
        Paket p2; //Paket für Mitarbeiter welches geliefert werden muss
        Paket p3; //Paket für Mitarbeiter welches abgeholt wurde
        Paket p4; //Paket in Station welches abgeholt werden soll ("abzuholen")
        Paket p5; //Paket in Station welches abgeholt werden soll ("abzuholen")
        Paket p6; //Paket für Mitarbeiter welches geliefert werden muss
        Paket p7; //Paket für Mitarbeiter welches geliefert werden muss 
        Paket p8; //zum Testen anderer Paketgroeßen 
        List<Paket> KundenPakete1;
        Kunde k1;
        List<Kunde> kl1;
        Mitarbeiter m1;
        List<Mitarbeiter> ml1;
        List<Paket> MitarbeiterLieferPakete;
        List<Paket> MitarbeiterAbgeholtePakete;
        Paketstation ps1;
        TestUI ui1;
        Controller Verwalter;

        [TestInitialize]
        public void TestInit()
        {
            //Pakete initialisieren
            p1 = new Paket(1L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);
            p2 = new Paket(2L, "Susi", "PaketAbsenderstr. 12", "Klaus", "Beispielstraße 22", "Transport", -1, -1, Groesse.XS);
            p3 = new Paket(3L, "Daniela", "PaketAbsenderstr. 16", "Scharlotte", "EmpfaengerStr. 5", "Transport", -1, -1, Groesse.XS);
            p4 = new Paket(4L, "BeispielAbsName", "BeispielAbsenderAddr. 16", "BeispielEmpfName", "EmpfaengerStr. 5", "Abholen", -1, -1, Groesse.XS);
            p5 = new Paket(5L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Abholen", -1, -1, Groesse.XS);
            p6 = new Paket(6L, "BeispielAbsName", "BeispielAbsenderAddr. 16", "BeispielEmpfName", "EmpfaengerStr. 5", "Transport", -1, -1, Groesse.XS);
            p7 = new Paket(7L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Transport", -1, -1, Groesse.XS);
            p8 = new Paket(8L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Verschicken", -1, -1, Groesse.XL);

            //Pakete in entsprechende Listen hinzufügen
            KundenPakete1 = new List<Paket>();
            KundenPakete1.Add(p1);
            MitarbeiterLieferPakete = new List<Paket>();
            MitarbeiterLieferPakete.Add(p2);
            MitarbeiterAbgeholtePakete = new List<Paket>();
            MitarbeiterAbgeholtePakete.Add(p3);

            //Kunde mit Paket initialisieren
            k1 = new Kunde(1L, "Klaus", "Beispielstraße 22", "Klausi", "1234", KundenPakete1);

            //Kundenliste initialisieren
            kl1 = new List<Kunde>();
            kl1.Add(k1);

            //Mitarbeiter mit Paketen initialisieren
            m1 = new Mitarbeiter(1L, "John", "admin", "admin", MitarbeiterLieferPakete, MitarbeiterAbgeholtePakete);

            //Mitarbeiterliste initialisieren
            ml1 = new List<Mitarbeiter>();
            ml1.Add(m1);

            //Paketstation, UI und Controller initialisieren
            List<Paketstation> psList = new List<Paketstation>();
            ui1 = new TestUI();
            ps1 = new Paketstation(1, 9, ui1);
            psList.Add(ps1);
            Verwalter = new Controller(kl1, ml1, ui1, null, false, psList);
        }

        [TestCleanup]
        public void Cleanup()
        {
            p1 = null;
            KundenPakete1 = null;
            k1 = null;
            kl1 = null;
            m1 = null;
            ml1 = null;
            ps1 = null;
            ui1 = null;
        }
        [TestMethod]
        public void AuthentifizierungsTest()
        {
            //User simulieren
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");

            Verwalter.Authentifizieren();

            Assert.AreEqual(typeof(Kunde), Verwalter.AktiverUser.GetType());

            //Admin simulieren
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");

            Verwalter.Authentifizieren();

            Assert.AreEqual(typeof(Mitarbeiter), Verwalter.AktiverUser.GetType());
        }

        [TestMethod]
        public void KundeGibtPaketAb()
        {
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //prüft ob es ein Paket zum Abholen in der Station gibt
            Assert.AreEqual(1, Verwalter.AktuelleStation.MitarbeiterListeAbzuholenderPakete().Count);
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(0, k1.Pakete.Count);
        }
        [TestMethod]
        public void KundeHoltPaketAb()
        {
            //Paket in Paketstation hinzufügen welches abgeholt werden soll
            Verwalter.Stationen[0].Paketfach[2].PaketAnnehmen(p2); //der ersten Station ein Paket in das 3. Fach legen

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //Das zweite Paket bei k1 müsste auf abgeholt stehen
            Assert.AreEqual("abgeholt", k1.Pakete[1].Status);
        }
        [TestMethod]
        public void MitarbeiterHoltPaketeAb()
        {
            Verwalter.Stationen[0].Paketfach[2].PaketAnnehmen(p4);
            Verwalter.Stationen[0].Paketfach[3].PaketAnnehmen(p5);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            for (int i = 0; i < m1.AbgeholtePakete.Count; i++)
            {
                Assert.AreEqual("Transport", m1.AbgeholtePakete[i].Status);
            }
        }
        [TestMethod]
        public void MitarbeiterBringtPakete()
        {

            //Pakete Mitarbeiter geben
            m1.LieferPakete.Add(p6);
            m1.LieferPakete.Add(p7);

            //Pakete in Paketstation einlegen
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //Paketstation muss nun 2 Pakete besitzen, welche auf dem Status "Abholen" stehen müssten
            int j = 0;
            for (int i = 0; i < ps1.Paketfach.Count; i++)
            {
                if (ps1.Paketfach[i].Packet != null)
                {
                    Assert.AreEqual("Abholen", ps1.Paketfach[i].Packet.Status);
                }
            }
        }
        [TestMethod]
        public void KundeGibtGroesseresPaketAb()
        {
            //erstes Paket mit kleinerer Größe entfernen
            k1.Pakete.Remove(p1);
            //groesseres Paket hinzufügen
            k1.Pakete.Add(p8);

            Fach fXL = new Fach(15, true, null, false, Groesse.XL);

            ps1.FuegeFachHinzu(fXL);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //prüft ob es ein Paket zum Abholen in der Station gibt
            Assert.AreEqual(1, Verwalter.AktuelleStation.MitarbeiterListeAbzuholenderPakete().Count);
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(0, k1.Pakete.Count);
        }

        [TestMethod]
        public void KeineFreienFaecherVerfuegbar() //TODO mehrere Pakete gleicher Größe in unterschiedliche Fächer
        {
            for (int i = 0; i < ps1.AnzahlFaecher; i++) //alle Fächer belegen, damit Kunde kein Paket hineinlegen kann
            {
                ps1.Paketfach[i].Belegt = true;
            }

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();
            
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(1, k1.Pakete.Count);
        }

        [TestMethod]
        public void KundeGibtMehrerePaketeHinein() //TODO mehrere Pakete gleicher Größe in unterschiedliche Fächer
        {
            //erstes Paket mit kleinerer Größe entfernen
            k1.Pakete.Remove(p1);
            //groesseres Paket hinzufügen
            k1.Pakete.Add(p8);

            Fach fXL = new Fach(15, true, null, false, Groesse.XL);

            ps1.FuegeFachHinzu(fXL);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //prüft ob es ein Paket zum Abholen in der Station gibt
            Assert.AreEqual(1, Verwalter.AktuelleStation.MitarbeiterListeAbzuholenderPakete().Count);
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(0, k1.Pakete.Count);
        }
        //TODO: Controller.MitarbeiterLiefertPakete() testen! (ob fehler abgefangen werden)
    }
}
