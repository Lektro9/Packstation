//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Terminal.cs
//Beschreibung: Benutzerinteraktion
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;
using System.Threading;

namespace Packstation_Kroll
{
    class Terminal
    {
        #region Eigenschaften
        string _Nutzer;
        #endregion

        #region Accessoren/Modifier
        public string Nutzer { get => _Nutzer; set => _Nutzer = value; }
        #endregion

        #region Konstruktoren
        public Terminal () {
            Nutzer = "Kunde";
        }
        #endregion

        #region Worker
        public void zeigeMenu(string Menu) {
            switch(Menu) {
                case "Main":
                Console.WriteLine("Hier ist das MainMenu!");
                break;
                
                case "standartMenu":
                Console.WriteLine("Hier ist das standartMenu!");
                break;

                default:
                Console.WriteLine("Irgendetwas ist schiefgelaufen");
                break;
            }
        }

        public void zeigeSplash() {
            Console.Clear();
            Console.WriteLine("\n           Programmm:    Bruchrechner Version 1.0" +
                "\n           Autor:        Kroll" +
                "\n           Beschreibung: Simples Rechnen mit Brüchen");
            Thread.Sleep(2000);
        }
        #endregion

        #region Schnittstellen
        #endregion
    }
}
