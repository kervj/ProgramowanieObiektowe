namespace PO_2
{
    public class Kontener
    {
        private static int _idCounter = 1;

        public string Id { get; }
        public string Location { get; set; } = "None";
        public Towar Content { get; private set; } = Towar.None();

        public Kontener()
        {
            Id = $"C_{_idCounter}";
            _idCounter++;
        }

        public Kontener(Towar goods) : this()
        {
            Load(goods);
        }

        public override string ToString()
        {
            return $"Container with id: {Id} containing {Content}";
        }

        public void Load(Towar goods)
        {
            if (Content is not { TypTowaru: TypTowaru.None })
                throw new Exception($"Cannot load {goods} because container {Id} already contains {Content}");

            Content = goods;

            Console.WriteLine($"[{Id}] {goods} was loaded to container");
        }

        public Towar Unload()
        {
            if (Content is { TypTowaru: TypTowaru.None })
                throw new Exception($"Cannot container {Id}, because it's empty");

            var result = Content;
            Content = Towar.None();
            return result;
        }
    }
}
