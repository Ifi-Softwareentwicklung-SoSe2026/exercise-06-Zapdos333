using System;
using System.Collections.Generic;
using System.Linq;

namespace Baufflaechenverwaltung;

public enum Nutzung
{
    Gewerbe, Landwirtschaft, Forst, Wohnnutzung, Brachfläche
}

public enum Bebaubarkeit
{
    Ja, Nein, Auflagen
}

public enum FlaechenStatus
{
    Frei, Reserviert, Bebaut
}

public enum BauvorhabenStatus
{
    AntragEingereicht, Genehmigt, Abgelehnt, InBearbeitung, Abgeschlossen
}

public record Antragsteller(string Name, string Kontakt, string Firma);

public record Bauflaeche(string Id, double Groesse, string Lage, Nutzung Nutzung, Bebaubarkeit Bebaubarkeit, string BPlanNummer, decimal Bodenrichtwert, string Eigentuemer) 
{
    public FlaechenStatus Status { get; set; } = FlaechenStatus.Frei;
}

public record Grundstueck(string FlurstueckNummer, List<Bauflaeche> Flaechen);

public record Bauvorhaben(string Id, Antragsteller Antragsteller, string GeplanteNutzung, DateTime Beginn, DateTime Fertigstellung, BauvorhabenStatus Status, List<Bauflaeche> ZugeordneteFlaechen);

public class Bauverwaltung
{
    private readonly List<Bauvorhaben> _bauvorhaben = new();
    private readonly List<Bauflaeche> _alleFlaechen = new();

    public void BauvorhabenAnlegen(Bauvorhaben bv)
    {
        _bauvorhaben.Add(bv);
        foreach (var flaeche in bv.ZugeordneteFlaechen)
        {
            flaeche.Status = FlaechenStatus.Reserviert;
        }
        Console.WriteLine($"Bauvorhaben {bv.Id} wurde angelegt.");
    }

    public void BauvorhabenEntfernen(string id)
    {
        var bv = _bauvorhaben.FirstOrDefault(x => x.Id == id);
        if (bv != null)
        {
            foreach (var flaeche in bv.ZugeordneteFlaechen)
            {
                flaeche.Status = FlaechenStatus.Frei;
            }
            _bauvorhaben.Remove(bv);
            Console.WriteLine($"Bauvorhaben {id} wurde entfernt.");
        }
    }

    public void FlaechenHinzufuegen(Bauflaeche flaeche) => _alleFlaechen.Add(flaeche);
    public List<Bauflaeche> GetFlaechen() => _alleFlaechen;
}

class Program
{
    static void Main(string[] args)
    {
        var verwaltung = new Bauverwaltung();
        var flaeche1 = new Bauflaeche("F1", 500.0, "Nord", Nutzung.Wohnnutzung, Bebaubarkeit.Ja, "BP-2022-089", 500.00m, "Max Mustermann");
        verwaltung.FlaechenHinzufuegen(flaeche1);

        Console.WriteLine("--- Bauflächenverwaltung CLI ---");
        Console.WriteLine("1: Bauvorhaben erstellen");
        Console.WriteLine("2: Bauvorhaben entfernen");
        Console.WriteLine("3: Beenden");

        bool running = true;
        while (running)
        {
            Console.Write("Auswahl: ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    var bv = new Bauvorhaben("BV-001", new Antragsteller("Erika Muster", "erika@mail.com", "Bau GmbH"), "Wohngebäude", DateTime.Now, DateTime.Now.AddYears(1), BauvorhabenStatus.AntragEingereicht, new List<Bauflaeche> { flaeche1 });
                    verwaltung.BauvorhabenAnlegen(bv);
                    break;
                case "2":
                    verwaltung.BauvorhabenEntfernen("BV-001");
                    break;
                case "3":
                    running = false;
                    break;
            }
        }
    }
}