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
//23.01.2020:   Entwicklung der neuen Features (Pakete mit unterschiedlichen Größen und mehrere Stationen) abgeschlossen
//28.01.2020:   Entwicklung 2.0 abgeschlossen

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
        public void Erweiterungsaufgabe1_PruefePaketGroesse()
        {
            Paket p = new Paket(1L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.S);

            Assert.AreEqual(1, (int) p.Groesse);
        }

        [TestMethod]
        public void Erweiterungsaufgabe4_PruefeObFachHinzugefuegt()
        {
            TestUI testUI = new TestUI();
            Paketstation ps1 = new Paketstation(1, 50, testUI);
            Fach fXL = new Fach(10, true, null, false, Groesse.XL);
            ps1.FuegeFachHinzu(fXL);

            Assert.AreEqual(ps1.Paketfach.Count, 51);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Anzahl Stationen stimmt nicht")]
        public void Erweiterungsaufgabe5_PruefeAnzahlStationen()
        {
            Stationscontroller Verwalter = new Stationscontroller(0);
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
        //Standartfaecher für jede Station
        Fach f1;
        Fach f2;
        Fach f3;
        Fach f4;
        Fach f5;
        Fach f6;
        Fach f7;
        Fach f8;
        Fach f9;
        List<Fach> FaecherListe;
        List<Paketstation> StationenListe;
        List<Paket> KundenPakete1;
        Kunde k1;
        List<Kunde> kl1;
        Mitarbeiter m1;
        List<Mitarbeiter> ml1;
        List<Paket> MitarbeiterLieferPakete;
        List<Paket> MitarbeiterAbgeholtePakete;
        Geschaeftsfuehrer gf1;
        List<Geschaeftsfuehrer> geschFuehrList;
        TestUI ui1;
        Metacontroller Verwalter;

        [TestInitialize]
        public void TestInit()
        {
            //Pakete initialisieren
            p1 = new Paket(1L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);
            p2 = new Paket(2L, "Susi", "PaketAbsenderstr. 12", "Klaus", "Beispielstraße 22", "Transport", -1, -1, Groesse.XXL);
            p3 = new Paket(3L, "Daniela", "PaketAbsenderstr. 16", "Scharlotte", "EmpfaengerStr. 5", "Transport", -1, -1, Groesse.XXL);
            p4 = new Paket(4L, "BeispielAbsName", "BeispielAbsenderAddr. 16", "BeispielEmpfName", "EmpfaengerStr. 5", "Abholen", -1, -1, Groesse.XS);
            p5 = new Paket(5L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Abholen", -1, -1, Groesse.XS);
            p6 = new Paket(6L, "BeispielAbsName", "BeispielAbsenderAddr. 16", "BeispielEmpfName", "EmpfaengerStr. 5", "Transport", -1, -1, Groesse.XS);
            p7 = new Paket(7L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Transport", -1, -1, Groesse.XS);
            p8 = new Paket(8L, "BeispielAbsName2", "BeispielAbsenderAddr. 17", "BeispielEmpfName2", "EmpfaengerStr. 6", "Verschicken", -1, -1, Groesse.XL);

            f1= new Fach(1, true, null, false, Groesse.XS);
            f2 = new Fach(2, true, null, false, Groesse.XS);
            f3 = new Fach(3, true, null, false, Groesse.XS);
            f4 = new Fach(4, true, null, false, Groesse.M);
            f5 = new Fach(5, true, null, false, Groesse.M);
            f6 = new Fach(6, true, null, false, Groesse.M);
            f7 = new Fach(7, true, null, false, Groesse.XXL);
            f8 = new Fach(8, true, null, false, Groesse.XXL);
            f9 = new Fach(9, true, null, false, Groesse.XXL);

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

            //Geschäftsführer initialisieren
            gf1 = new Geschaeftsfuehrer(1, "Bernd", "BossBernd", "stark");
            geschFuehrList = new List<Geschaeftsfuehrer>() { gf1 };

            //Paketstation, UI und Controller initialisieren
            ui1 = new TestUI();
            FaecherListe = new List<Fach>()
            {
                f1, f2, f3, f4, f5, f6, f7, f8, f9
            };
            List<Fach> FaecherListe2 = new List<Fach>(FaecherListe.Count);
            FaecherListe.ForEach((item) =>
            {
                FaecherListe2.Add(new Fach(item));
            });

            StationenListe = new List<Paketstation>()
            {
                new Paketstation(1, FaecherListe, ui1),
                new Paketstation(1, FaecherListe2, ui1),
            };
            Verwalter = new Metacontroller(kl1, ml1, geschFuehrList, StationenListe, ui1);
            
        }

        [TestCleanup]
        public void Cleanup()
        {
            //nicht nötig, da alle Variablen neu instanziiert werden
        }

        [TestMethod]
        public void UnterschiedlicheStationenBesuchen()
        {
            //User simulieren
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("2");

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("2");

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");


            Verwalter.run();

            Assert.AreEqual(typeof(Mitarbeiter), Verwalter.Stationen[1].AktiverUser.GetType());
        }

        [TestMethod]
        public void AuthentifizierungsTest()
        {
            //User simulieren
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");

            Verwalter.Stationen[0].Authentifizieren();

            Assert.AreEqual(typeof(Kunde), Verwalter.Stationen[0].AktiverUser.GetType());

            //Admin simulieren
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");

            Verwalter.Stationen[0].Authentifizieren();

            Assert.AreEqual(typeof(Mitarbeiter), Verwalter.Stationen[0].AktiverUser.GetType());
        }

        [TestMethod]
        public void KundeGibtPaketAb()
        {
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //prüft ob es ein Paket zum Abholen in der Station gibt
            Assert.AreEqual(1, Verwalter.Stationen[0].AktuelleStation.MitarbeiterListeAbzuholenderPakete().Count);
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(0, k1.Pakete.Count);
        }
        [TestMethod]
        public void KundeHoltPaketAb()
        {
            //Paket in Paketstation hinzufügen welches abgeholt werden soll
            Verwalter.Stationen[0].AktuelleStation.Paketfach[2].PaketAnnehmen(p2); //der ersten Station ein Paket in das 3. Fach legen

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //Das zweite Paket bei k1 müsste auf abgeholt stehen
            Assert.AreEqual("abgeholt", k1.Pakete[1].Status);
        }
        [TestMethod]
        public void MitarbeiterHoltPaketeAb()
        {
            Verwalter.Stationen[0].AktuelleStation.Paketfach[2].PaketAnnehmen(p4);
            Verwalter.Stationen[0].AktuelleStation.Paketfach[3].PaketAnnehmen(p5);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("1");
            
            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
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

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //Paketstation muss nun 2 Pakete besitzen, welche auf dem Status "Abholen" stehen müssten
            int j = 0;
            for (int i = 0; i < StationenListe[0].Paketfach.Count; i++)
            {
                if (StationenListe[0].Paketfach[i].Packet != null)
                {
                    Assert.AreEqual("Abholen", StationenListe[0].Paketfach[i].Packet.Status);
                }
            }
        }
        [TestMethod]
        public void Erweiterungsaufgabe2_KundeGibtGroesseresPaketAb()
        {
            //erstes Paket mit kleinerer Größe entfernen
            k1.Pakete.Remove(p1);
            //groesseres Paket hinzufügen
            k1.Pakete.Add(p8);

            Fach fXL = new Fach(15, true, null, false, Groesse.XL);

            StationenListe[0].FuegeFachHinzu(fXL);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //prüft ob es ein Paket zum Abholen in der Station gibt
            Assert.AreEqual(1, Verwalter.Stationen[0].AktuelleStation.MitarbeiterListeAbzuholenderPakete().Count);
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(0, k1.Pakete.Count);
        }

        [TestMethod]
        public void KeineFreienFaecherVerfuegbar()
        {
            //alle Fächer belegen, damit Kunde kein Paket hineinlegen kann
            for (int i = 0; i < StationenListe[0].AnzahlFaecher; i++) 
            {
                StationenListe[0].Paketfach[i].Belegt = true;
            }

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();
            
            //prüft ob Kunde noch Paket besitzt
            Assert.AreEqual(1, k1.Pakete.Count);
        }

        [TestMethod]
        public void KleinePaketeInGroessereFaecher() 
        {
            //k1 Pakete geben

            Paket p9 = new Paket(9L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);
            Paket p10 = new Paket(10L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);
            Paket p11 = new Paket(11L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Verschicken", 1, 1, Groesse.XS);

            k1.Pakete.Add(p9);
            k1.Pakete.Add(p10);
            k1.Pakete.Add(p11);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("Klausi");
            ui1.LinesToRead.Add("1234");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");

            ui1.LinesToRead.Add("0");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //prüft ob Kunde noch Pakete besitzt
            Assert.AreEqual(0, k1.Pakete.Count);
            Assert.AreEqual(Groesse.XS, Verwalter.Stationen[0].AktuelleStation.Paketfach[3].Packet.Groesse);
            Assert.AreEqual(Groesse.M, Verwalter.Stationen[0].AktuelleStation.Paketfach[3].Groesse);

            //prüft ob es ein Paket mit Status "abzuholen" in der Station liegen (Achtung: Diese werden hier auch entfernt)
            Assert.AreEqual(4, Verwalter.Stationen[0].AktuelleStation.MitarbeiterListeAbzuholenderPakete().Count);
        }

        [TestMethod]
        public void MitarbeiterGibtZuvielePaketeAb()
        {
            //k1 Pakete geben

            Paket p9 = new Paket(9L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Transport", 1, 1, Groesse.XXL);
            Paket p10 = new Paket(10L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Transport", 1, 1, Groesse.XXL);
            Paket p11 = new Paket(11L, "Klaus", "Beispielstraße 22", "Bernd", "EmpfaengerStr. 22", "Transport", 1, 1, Groesse.XXL);

            m1.LieferPakete.Add(p9);
            m1.LieferPakete.Add(p10);
            m1.LieferPakete.Add(p11);

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("0");
            
            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //Mitarbeiter sollte noch 1 Paket besitzen mit dem Status "Transport"
            Assert.AreEqual(1, m1.LieferPakete.Count);
            //Kontrolle ob die 3 XXL Pakete tatsächlich in der Station angekommen sind (anhand der Größe)
            List<Paket> KontrollListe = new List<Paket>(Verwalter.Stationen[0].AktuelleStation.MitarbeiterListeAbzuholenderPakete());
            for (int i = 0; i < KontrollListe.Count; i++)
            {
                Assert.AreEqual(Groesse.XXL, KontrollListe[i].Groesse);
            }
        }

        [TestMethod]
        public void Erweiterungsaufgabe4_MitarbeiterWechseltDefektesFach()
        {
            //k1 Pakete geben

            Fach DefektesFach = new Fach(15, false, null, false, Groesse.XL);

            StationenListe[0].FuegeFachHinzu(DefektesFach);

            m1.ErsatzFaecher.Add(new Fach(10, true, null, false, Groesse.S));

            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("1");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("admin");
            ui1.LinesToRead.Add("3");
            ui1.LinesToRead.Add("9");
            ui1.LinesToRead.Add("0");


            ui1.LinesToRead.Add("2");
            ui1.LinesToRead.Add("BossBernd");
            ui1.LinesToRead.Add("stark");
            ui1.LinesToRead.Add("abschalten");
            ui1.LinesToRead.Add("abschalten");

            Verwalter.run();

            //Schauen ob Größe des neuen Faches übereinstimmt
            Assert.AreEqual(Groesse.S, Verwalter.Stationen[0].AktuelleStation.Paketfach[9].Groesse);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Anzahl der Fächer stimmt nicht")]
        public void Erweiterungsaufgabe3_PruefeAnzahlStationen()
        {
            //Ein Fach entfernen und prüfen ob Exception entsteht
            Verwalter.Stationen[0].AktuelleStation.EntferneFach(f1);
        }
    }
}
