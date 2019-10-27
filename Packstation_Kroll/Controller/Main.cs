//Autor:        Kroll
//Datum:        26.10.2019
//Dateiname:    Main.cs
//Beschreibung: Startet Hauptprogramm
//Änderungen:
//26.10.2019:   Entwicklungsbeginn 

using System;

namespace Packstation_Kroll
{
    class main
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Controller Verwalter = new Contoller();

            Verwalter.run();
        }
    }
}
