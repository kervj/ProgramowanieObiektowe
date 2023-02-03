namespace PO_3
{
    internal abstract class Zwierze
    {
        private string Gatunek { get; set; }

        public Zwierze(string gatunek)
        {
            Gatunek = gatunek;
        }
    }
}
