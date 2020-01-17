//Autor:        Kroll
//Datum:        10.12.2019
//Dateiname:    Paketstation.cs
//Beschreibung: Paketstation welches die Fächer beinhaltet und managed
//Änderungen:
//10.12.2019:   Entwicklungsbeginn 
//29.12.2019:   Entwicklung abgeschlossen

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packstation_Kroll
{
    public class Paketstation
    {
        #region Eigenschaften
        List<Fach> _Paketfach;
        Userinterface _Terminal;
        int _AnzahlFaecher;
        int _ID;
        #endregion

        #region Accessoren/Modifier
        public List<Fach> Paketfach { get => _Paketfach; set => _Paketfach = value; }
        public Userinterface Terminal { get => _Terminal; set => _Terminal = value; }
        public int AnzahlFaecher { get => _AnzahlFaecher; set => _AnzahlFaecher = value; }
        public int ID { get => _ID; set => _ID = value; }
        #endregion

        #region Konstruktoren
        public Paketstation()
        {
            this.Paketfach = new List<Fach>();
            for (int i = 0; i < 9; i++)
            {
                this.Paketfach.Add(new Fach(i));
            }
            this.Terminal = null;
            this.AnzahlFaecher = 9;
            this.ID = ID;
        }
        //Spezialkonstruktor
        public Paketstation(int AnzahlFaecher)
        {
            this.Paketfach = new List<Fach>();
            for (int i = 0; i < AnzahlFaecher; i++)
            {
                this.Paketfach.Add(new Fach(i));
            }
            this.Terminal = null;
            this.AnzahlFaecher = AnzahlFaecher;
            pruefeAnzahlFaecher();
        }

        public Paketstation(int ID, List<Fach> Faecher)
        {
            this.Paketfach = new List<Fach>();
            for (int i = 0; i < AnzahlFaecher; i++)
            {
                this.Paketfach.Add(new Fach(i));
            }
            this.Terminal = null;
            this.AnzahlFaecher = AnzahlFaecher;
            pruefeAnzahlFaecher();
            this.ID = ID;
        }

        public Paketstation(List<Fach> Paketfach, Userinterface Terminal)
        {
            this.Paketfach = Paketfach;
            this.Terminal = Terminal;
            this.AnzahlFaecher = Paketfach.Count();
            pruefeAnzahlFaecher();
        }

        public Paketstation(int AnzahlFaecher, Userinterface Terminal)
        {
            this.Paketfach = new List<Fach>();
            for (int i = 0; i < AnzahlFaecher; i++)
            {
                this.Paketfach.Add(new Fach(i));
            }
            this.Terminal = Terminal;
            pruefeAnzahlFaecher();
        }
        #endregion

        #region Worker
        //Konnte keinen Gebrauch für die Methode finden
        public Paket KundeholtPaketab()
        {
            Paket retVal = new Paket();
            return retVal;
        }

        public void KundeLiefertPaket(Paket p)
        {
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (!Paketfach[i].IstBelegt() && Paketfach[i].IstGrossGenug(p.Groesse))
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
                if (Paketfach[i].IstBelegt())
                {
                    if (Paketfach[i].Packet.Status == "abzuholen")
                    {
                        retVal.Add(Paketfach[i].getPaket());
                    }
                    else
                    {
                        // nichts tun
                    }
                }
                else
                {
                    // nichts tun
                }
            }
            return retVal;
        }

        public bool IsteinPaketvorhanden(Kunde k)
        {
            bool retVal = false;
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (Paketfach[i].IstBelegt())
                {
                    if (k.Name == Paketfach[i].Packet.EmpfaengerName && k.Adresse == Paketfach[i].Packet.EmpfaengerAdresse)
                    {
                        retVal = true;
                    }
                    else
                    {
                        //nichts tun
                    }
                }
                else
                {
                    //nichts tun
                }
            }
            return retVal;
        }

        public int getPaketFachnummer(Kunde k)
        {
            int retVal = 0;
            for (int i = 0; i < Paketfach.Count; i++)
            {
                if (Paketfach[i].IstBelegt())
                {
                    if (k.Name == Paketfach[i].Packet.EmpfaengerName && k.Adresse == Paketfach[i].Packet.EmpfaengerAdresse)
                    {
                        retVal = Paketfach[i].Nummer;
                    } 
                    else
                    {
                        //nichts tun
                    }
                }
                else
                {
                    //nichts tun
                }
            }
            return retVal;
        }

        public void pruefeAnzahlFaecher()
        {
            if (this.AnzahlFaecher < 9 || this.AnzahlFaecher > 100)
            {
                throw new ArgumentException("Anzahl der Faecher darf nicht kleiner als 9 und nicht höher als 100 sein.");
            }
            else
            {
                //nichts tun
            }
        }

        //Konnte keinen Gebrauch für die Methode finden
        public void MitarbeiterWechseltFach()
        {
            //TODO: noch nicht verstanden was diese Methode machen könnte
        }

        public void FuegeFachHinzu(Fach f)
        {
            this.Paketfach.Add(f);
            this.AnzahlFaecher += 1;
            pruefeAnzahlFaecher();
        }

        public Fach EntferneFach(Fach f)
        {
            Fach retVal = f;
            this.Paketfach.Remove(f);
            this.AnzahlFaecher -= 1;
            pruefeAnzahlFaecher();
            return retVal;
        }

        //TODO: neues Fach erstellen und hinzufügen
        #endregion

        #region Schnittstellen
        #endregion
    }
}
