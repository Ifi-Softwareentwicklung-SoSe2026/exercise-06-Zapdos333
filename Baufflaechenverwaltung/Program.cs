using System;
using System.Collections.Generic;
using System.Linq;

namespace Baufflaechenverwaltung
{
    public enum Nutzung { Gewerbe, Landwirtschaft, Forst, Wohnnutzung, Brachfläche }
    public enum Bebaubarkeit { Ja, Nein, Auflagen }
    public enum FlaechenStatus { Frei, Reserviert, Bebaut }
    public enum BauvorhabenStatus { AntragEingereicht, Genehmigt, Abgelehnt, InBearbeitung, Abgeschlossen }
    public enum GeplanteNutzung { Wohngebäude, Gewerbe, Infrastruktur }

    public readonly record struct Antragsteller(string Name, string Kontakt, string Firma);
    public readonly record struct Bauflaeche(string Id, double Groesse, string Lage, Nutzung AktuelleNutzung, Bebaubarkeit Bebaubarkeit, string BPlanNummer, decimal Bodenrichtwert, string Eigentuemer, FlaechenStatus Status);
    public readonly record struct Grundstueck(string FlurstueckNummer, List<Bauflaeche> Flaechen);
    public record Bauvorhaben(string Id, Antragsteller Antragsteller, GeplanteNutzung Nutzung, DateTime Beginn, DateTime Fertigstellung, BauvorhabenStatus Status, List<string> ZugeordneteFlaechenIds);

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

        public void AddFlaeche(Bauflaeche flaeche) => _flaechen.Add(flaeche);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var verwaltung = new Bauverwaltung();
            
            var flaeche1 = new Bauflaeche("F1", 500.0, "Nord", Nutzung.Brachfläche, Bebaubarkeit.Ja, "BP-2022-089", 500.00m, "Max Mustermann", FlaechenStatus.Frei);
            verwaltung.AddFlaeche(flaeche1);

            var bv1 = new Bauvorhaben("BV-001", new Antragsteller("Erika Muster", "erika@mail.com", "Bau GmbH"), GeplanteNutzung.Wohngebäude, DateTime.Now, DateTime.Now.AddYears(1), BauvorhabenStatus.AntragEingereicht, new List<string> { "F1" });
            
            verwaltung.BauvorhabenAnlegen(bv1);
            verwaltung.BauvorhabenEntfernen("BV-001");
        }
    }
}