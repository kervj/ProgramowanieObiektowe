namespace PO_2
{
    public class Symulacja
    {
        public Dictionary<string, List<Pojazd>> Pojazd { get; } = new();
        public Dictionary<TypTowaru, Towar> Towar { get; } = new();
        public Dictionary<string, Magazyn> Magazyn { get; } = new();
        public List<Kontener> Kontener { get; } = new();

        public void DisplaySimulationInfo()
        {
            Console.WriteLine("Vehicles: ");
            foreach (var pair in Pojazd)
            {
                Console.WriteLine($"\t- {pair.Key}");
                foreach (var vehicle in pair.Value)
                {
                    Console.WriteLine($"\t\t -{vehicle.Id} at {vehicle.Location}");
                }
            }
        }

        public void RunSimulation()
        {
            Console.WriteLine("Starting simulation");
            InitializeSimulation();

            Console.WriteLine("Transporting containers from New York to Gdansk");

            TransportContainers(x => true, Magazyn["New York"], Magazyn["Gdansk"], Pojazd[nameof(Statek)]);

            Console.WriteLine("Transporting containers from Gdansk to Wroclaw");

            TransportContainers(x => true, Magazyn["Gdansk"], Magazyn["Wroclaw"], Pojazd[nameof(Pociag)]);

            Console.WriteLine("Transporting shoes and clothes from Wroclaw to Poznan");

            TransportContainers(x => x.Content.TypTowaru is TypTowaru.Clothes or TypTowaru.Shoes, Magazyn["Wroclaw"],
                Magazyn["Poznan"], Pojazd[nameof(Ciezarowka)]);

            Console.WriteLine("Transporting electronic parts from Wroclaw to Krakow");
            TransportContainers(x => x.Content.TypTowaru is TypTowaru.ElectronicParts, Magazyn["Wroclaw"],
                Magazyn["Krakow"], Pojazd[nameof(Ciezarowka)]);

            Console.WriteLine(new string('#', 25));

            foreach (var warehouse in Magazyn)
            {
                warehouse.Value.DisplayStock();
            }

        }

        private void InitializeSimulation()
        {
            // dodajemy towary
            var goods = new List<Towar>
        {
            new(TypTowaru.ElectronicParts),
            new(TypTowaru.Cellphones),
            new(TypTowaru.Clothes),
            new(TypTowaru.Shoes)
        };
            goods.ForEach(g => Towar.Add(g.TypTowaru, g));

            // dodajemy magazyny
            var warehouses = new List<Magazyn>
        {
            new("New York"),
            new("Gdansk"),
            new("Wroclaw"),
            new("Krakow"),
            new("Poznan")
        };
            warehouses.ForEach(w => Magazyn.Add(w.Location, w));

            // dodajemy pojazdy
            Pojazd.Add(nameof(Statek), new List<Pojazd>
            {
                new Statek("Gdansk")
            });

            Pojazd.Add(nameof(Pociag), new List<Pojazd>
        {
            new Pociag("Gdansk"),
                new Pociag("Gdansk"),
        });

            Pojazd.Add(nameof(Ciezarowka), new List<Pojazd>
            {
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
            new Ciezarowka("Gdansk"),
        });

            // dodajemy kontenery
            Kontener.AddRange(Enumerable.Range(0, 10).Select(x => new Kontener(Towar[TypTowaru.ElectronicParts])));
            Kontener.AddRange(Enumerable.Range(0, 20).Select(x => new Kontener(Towar[TypTowaru.Cellphones])));
            Kontener.AddRange(Enumerable.Range(0, 5).Select(x => new Kontener(Towar[TypTowaru.Shoes])));
            Kontener.AddRange(Enumerable.Range(0, 5).Select(x => new Kontener(Towar[TypTowaru.Clothes])));

            // dodajemy kontenery do miejsca poczatkowego
            Magazyn["New York"].LoadContainers(Kontener);
        }

        private void ImportContainersFromAbroad()
        {
            var ships = Pojazd[nameof(Statek)];

            var source = Magazyn["New York"];

            var target = Magazyn["Gdansk"];

            TransportContainers((c => true), source, target, ships);
        }

        private void MoveAllContainersToWroclaw()
        {
            var trains = Pojazd[nameof(Pociag)];

            var source = Magazyn["Gdansk"];

            var target = Magazyn["Wroclaw"];

            TransportContainers(c => true, source, target, trains);
        }

        private bool TransportContainers(Func<Kontener, bool> containerQuery, Magazyn source, Magazyn target,
            Pojazd transport)
        {
            // kontenery musza byc zarezerwowane przed startem
            if (!source.ReserveContainers(containerQuery, transport))
                return false;

            // jezeli pojazd nie jest w danym magazynie to robimy dodatkowy przejazd na pusto
            if (transport.Location != source.Location)
                transport.Travel(source);

            // wczytujemy poprzednie zapamietane transporty
            source.LoadToVehicle(transport);

            // podrozujemy do wybranego magayznu
            transport.Travel(target);

            // wyładowujemy w magazynie
            transport.UnloadToWarehouse(target);
            return true;
        }

        private void TransportContainers(Func<Kontener, bool> containerQuery, Magazyn source, Magazyn target,
            List<Pojazd> transport)
        {
            // jezeli jest pojazd w magazynie to najpierw go rozladuje a potem kolejny
            transport = transport.OrderBy(v => v.Location.Equals(source.Location) ? 0 : 1).ToList();

            bool containersTransported = false;

            while (!containersTransported)
            {
                // tak dlugo bedzie dostarczac produkty az nie bedzie pustki w magazyniea
                if (transport.Any(vehicle => !TransportContainers(containerQuery, source, target, vehicle)))
                {
                    containersTransported = true;
                }
            }
        }
    }
}
