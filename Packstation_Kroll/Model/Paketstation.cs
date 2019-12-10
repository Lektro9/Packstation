//Autor:        Kroll
//Datum:        10.12.2019
//Dateiname:    Paketstation.cs
//Beschreibung: Paketstation welches die Fächer beinhaltet und managed
//Änderungen:
//10.12.2019:   Entwicklungsbeginn 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packstation_Kroll.Model
{
    class Paketstation
    {
        #region Eigenschaften
        List<Fach> _Paketfach;
        Userinterface _Terminal;
        #endregion

        #region Accessoren/Modifier
        public List<Fach> Paketfach { get => _Paketfach; set => _Paketfach = value; }
        public Userinterface Terminal { get => _Terminal; set => _Terminal = value; }
        #endregion

        #region Konstruktoren
        public Paketstation()
        {
            this.Paketfach = new List<Fach>();
            for (int i = 0; i < 9; i++)
            {
                Paketfach.Add(new Fach());
            }
            this.Terminal = null;
        }
        //Spezialkonstruktor
        public Paketstation(List<Fach> Paketfach, Userinterface Terminal)
        {
            this.Paketfach = Paketfach;
            this.Terminal = Terminal;
        }

        public Paketstation(int AnzahlFaecher, Userinterface Terminal)
        {
            this.Paketfach = new List<Fach>();
            for (int i = 0; i < AnzahlFaecher; i++)
            {
                Paketfach.Add(new Fach());
            }
            this.Terminal = Terminal;
        }
        #endregion

        #region Worker
        public Paket KundeholtPaketab()
        {
            Paket retVal = new Paket();
            //TODO: logic
            return retVal;
        }

        public void KundeLiefertPaket(Paket p)
        {
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (!Paketfach[i].IstBelegt())
                {
                    Paketfach[i].PaketAnnehmen(p);
                    break;
                } //TODO: was ist wenn kein Fach frei ist?
            }
        }

        public List<Paket> MitarbeiterListeAbzuholenderPakete()
        {
            List<Paket> retVal = new List<Paket>();
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (Paketfach[i].Packet.Status == "abzuholen") //Achtung, vielleicht doch "Abholen"?
                {
                    retVal.Add(Paketfach[i].getPaket());
                }
            }
            return retVal;
        }

        public bool IsteinPaketvorhanden(Kunde k)
        {
            bool retVal = false;
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (k.Name == Paketfach[i].Packet.EmpfaengerName)
                {
                    retVal = true;
                }
            }
            return retVal;
        }

        public int getPaketFachnummer(Kunde k)
        {
            int retVal = 0;
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (k.Name == Paketfach[i].Packet.EmpfaengerName)
                {
                    retVal = Paketfach[i].Nummer;
                } //was passiert wenn keine Fachnummer verfügbar ist?
            }
            return retVal;
        }

        public void MitarbeiterWechseltFach()
        {
            //TODO: noch nicht verstanden was diese Methode machen könnte
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
