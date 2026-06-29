namespace Baufflaechenverwaltung;

public class Bauverwaltung
{
    private readonly List<Bauvorhaben> _bauvorhaben = new();
    private readonly List<Bauflaeche> _flaechen = new();

    public void BauvorhabenAnlegen(Bauvorhaben bv)
    {
        _bauvorhaben.Add(bv);
        Console.WriteLine($"Bauvorhaben {bv.Id} wurde angelegt.");
    }

    public void BauvorhabenEntfernen(string id)
    {
        var bv = _bauvorhaben.FirstOrDefault(x => x.Id == id);
        if (bv != null)
        {
            _bauvorhaben.Remove(bv);
            Console.WriteLine($"Bauvorhaben {id} wurde entfernt.");
        }
    }

    public void FlaechenHinzufuegen(Bauflaeche flaeche) => _flaechen.Add(flaeche);
}

public readonly record struct Antragsteller(string Name, string Kontakt, string Firma);
public readonly record struct Bauflaeche(string Id, double Groesse, string Lage, Nutzung AktuelleNutzung, Bebaubarkeit Bebaubarkeit, string BPlanNummer, decimal Bodenrichtwert, string Eigentuemer, FlaechenStatus Status);
public readonly record struct Grundstueck(string FlurstueckNummer, List<Bauflaeche> Flaechen);
public record Bauvorhaben(string Id, Antragsteller Antragsteller, GeplanteNutzung Nutzung, DateTime Beginn, DateTime Fertigstellung, BauvorhabenStatus Status, List<string> ZugeordneteFlaechenIds);
