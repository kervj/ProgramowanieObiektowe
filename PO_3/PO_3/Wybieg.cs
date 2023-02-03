namespace PO_3
{
    internal class Wybieg<T> where T : Zwierze
    {
        private List<T> Zwierze { get; set; } = new();
        public int Count => Zwierze.Count;
        
        public void Add(T zwierze)
        {
            if (zwierze is not null) Zwierze.Add(zwierze);
        }

        public void RemoveAll()
        {
            Zwierze = new List<T>();
        }

    }
}
