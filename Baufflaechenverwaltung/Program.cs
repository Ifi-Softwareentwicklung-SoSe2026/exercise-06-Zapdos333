using System;
using System.Collections.Generic;

namespace Baufflaechenverwaltung;

class Program
{
    static void Main(string[] args)
    {
        var verwaltung = new Bauverwaltung();
        var flaeche1 = new Bauflaeche("F1", 500.0, "Nord", Nutzung.Wohnnutzung, Bebaubarkeit.Ja, "BP-2022-089", 500.00m, "Max Mustermann");
        verwaltung.FlaechenHinzufuegen(flaeche1);

        if (args.Length == 0)
        {
            Console.WriteLine("Nutzung: 'create' oder 'remove <id>'");
            return;
        }

        switch (args[0].ToLower())
        {
            case "create":
                var bv = new Bauvorhaben("BV-001", new Antragsteller("Erika Muster", "erika@mail.com", "Bau GmbH"), "Wohngebäude", DateTime.Now, DateTime.Now.AddYears(1), BauvorhabenStatus.AntragEingereicht, new List<Bauflaeche> { flaeche1 });
                verwaltung.BauvorhabenAnlegen(bv);
                break;
            case "remove":
                if (args.Length > 1)
                {
                    verwaltung.BauvorhabenEntfernen(args[1]);
                }
                else
                {
                    Console.WriteLine("Bitte ID angeben.");
                }
                break;
            default:
                Console.WriteLine("Unbekannter Befehl.");
                break;
        }
    }
}