//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Fach.cs
//Beschreibung: Fach für die Pakete in der Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Packstation_Kroll;

namespace UnitTestPackstation
{
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
        }
    }
}
