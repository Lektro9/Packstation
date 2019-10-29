//Autor:        Kroll
//Datum:        29.10.2019
//Dateiname:    Controller.cs
//Beschreibung: Verwaltet die Daten
//Änderungen:
//29.10.2019:   Entwicklungsbeginn 

using System;
using System.Collections.Generic;

namespace Packstation_Kroll
{
    class Menu
    {
        #region Eigenschaften
        string _Ueberschrift;
        List <string> _AuswahlOptionen;
        string _Eingabe;

        
        #endregion

        #region Accessoren/Modifier
        public string Ueberschrift { get => _Ueberschrift; set => _Ueberschrift = value; }
        public string Eingabe { get => _Eingabe; set => _Eingabe = value; }
        public List<string> AuswahlOptionen { get => _AuswahlOptionen; set => _AuswahlOptionen = value; }
        #endregion

        #region Konstruktoren
        public Menu(string Ueberschrift, List<string> AuswahlOptionen) {
            this.Ueberschrift = Ueberschrift;
            this.AuswahlOptionen = AuswahlOptionen;
        }
        #endregion

        #region Worker
        public string anzeigen() {
            string alleOptionen =""; //string.Join(Environment.NewLine,AuswahlOptionen.ToArray());
            int j;
            for (int i = 0; i < AuswahlOptionen.Count; i++) {
                j = i + 1;
                alleOptionen += j.ToString() + ". " + AuswahlOptionen[i] + "\n";
            }
            return Ueberschrift + "\n\n" + alleOptionen + "\n\n";
        }

        public bool pruefeEingabe(string Eingabe) {
            for (int i = 0; i < AuswahlOptionen.Count;i ++) {
                if (i.ToString() == Eingabe) {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
