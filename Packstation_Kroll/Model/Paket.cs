//Autor:        Kroll
//Datum:        02.11.2019
//Dateiname:    Packet.cs
//Beschreibung: Fach für die Pakete in der Packstation
//Änderungen:
//02.11.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    public class Paket
    {
        #region Eigenschaften
        long _PaketNummer;
        string _AbsenderName;
        string _AbsenderAdresse;
        string _EmpfaengerName;
        string _EmpfaengerAdresse;
        string _Status; //5 Mögl.: Verschicken, Transport, Abholen, abgeholt, abzuholen
        int _PaketfachNr;
        int _PaketstationsNr;
        #endregion

        #region Accessoren/Modifier
        public long PaketNummer { get => _PaketNummer; set => _PaketNummer = value; }
        public string AbsenderName { get => _AbsenderName; set => _AbsenderName = value; }
        public string EmpfaengerName { get => _EmpfaengerName; set => _EmpfaengerName = value; }
        public string EmpfaengerAdresse { get => _EmpfaengerAdresse; set => _EmpfaengerAdresse = value; }
        public string Status { get => _Status; set => _Status = value; }
        public int PaketfachNr { get => _PaketfachNr; set => _PaketfachNr = value; }
        public int PaketstationsNr { get => _PaketstationsNr; set => _PaketstationsNr = value; }
        public string AbsenderAdresse { get => _AbsenderAdresse; set => _AbsenderAdresse = value; }
        #endregion

        #region Konstruktoren
        public Paket()
        {
            this.PaketNummer = -1;
            this.AbsenderName = "einAbsender";
            this.AbsenderAdresse = "";
            this.EmpfaengerName = "nutzer";
            this.EmpfaengerAdresse = "";
            this.Status = "Verschicken";
            this.PaketfachNr = -1;
            this.PaketstationsNr = -1;
        }
        //Spezialkonstruktor
        public Paket(long PaketNummer, string AbsenderName, string AbsenderAdresse, string EmpfaengerName, string EmpfaengerAdresse, string Status, int PaketfachNr, int PaketstationsNr)
        {
            this.PaketNummer = PaketNummer;
            this.AbsenderName = AbsenderName;
            this.AbsenderAdresse = AbsenderAdresse;
            this.EmpfaengerName = EmpfaengerName;
            this.EmpfaengerAdresse = EmpfaengerAdresse;
            this.Status = Status;
            this.PaketfachNr = PaketfachNr;
            this.PaketstationsNr = PaketstationsNr;
        }

        public Paket(Paket p)
        {
            this.PaketNummer = p.PaketNummer;
            this.AbsenderName = p.AbsenderName;
            this.AbsenderAdresse = p.AbsenderAdresse;
            this.EmpfaengerName = p.EmpfaengerName;
            this.EmpfaengerAdresse = p.EmpfaengerAdresse;
            this.Status = p.Status;
            this.PaketfachNr = p.PaketfachNr;
            this.PaketstationsNr = p.PaketstationsNr;
        }

        public Paket(string Status)
        {
            this.PaketNummer = -1;
            this.AbsenderName = "einAbsender";
            this.AbsenderAdresse = "";
            this.EmpfaengerName = "nutzer";
            this.EmpfaengerAdresse = "";
            this.Status = Status;
            this.PaketfachNr = -1;
            this.PaketstationsNr = -1;
        }
        #endregion

        #region Worker
        public void aendereStatus(string Status)
        {
            switch (Status)
            {
                case "Verschicken":
                    this.Status = Status;
                    break;
                case "Transport":
                    this.Status = Status;
                    break;
                case "Abholen":
                    this.Status = Status;
                    break;
                case "abgeholt":
                    this.Status = Status;
                    break;
                case "abzuholen":
                    this.Status = Status;
                    break;
                default:
                    throw new ArgumentException("You must supply an argument");
            }
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
