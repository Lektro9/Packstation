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
        public void TestVerschicken()
        {
            Paket testPaket = new Paket();
            testPaket.aendereStatus("Verschicken");
            Assert.AreEqual(testPaket.Status, "Verschicken");
        }

        [TestMethod]
        public void TestTransport()
        {
            Paket testPaket = new Paket();
            testPaket.aendereStatus("Transport");
            Assert.AreEqual(testPaket.Status, "Transport");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "You must supply an argument")]
        public void TestMethod2()
        {
            Paket testPaket = new Paket();
            testPaket.aendereStatus("nein");
        }
    }
}
