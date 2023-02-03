using System.ComponentModel;

namespace PO_2
{
    public abstract class Pojazd
    {
        public string Id { get; }
        public string Location { get; set; }
        private List<Kontener> _containers { get; } = new();
        public IReadOnlyCollection<Kontener> Container => _containers;

        public static int TotalTravelCount { get; set; } = 0;

        public Pojazd(string id, string location)
        {
            Id = id;
            Location = location;
        }

        protected abstract int GetMaxCapacity();
        protected abstract string GetTravelDetails(string destination);
        public int GetAvailableCapacity() => GetMaxCapacity() - _containers.Count;

        public void Travel(Magazyn destination)
        {
            if (destination.Location.Equals(Location, StringComparison.CurrentCulture))
            {
                Console.WriteLine($"Vehicle {Id} is already at {destination.Location}");
                return;
            }

            Console.WriteLine($"[{Id}] {GetTravelDetails(destination.Location)}");
            Location = destination.Location;

            TotalTravelCount++;
        }

        public void Load(Kontener container)
        {
            if (_containers.Count >= GetMaxCapacity()) throw new Exception($"Cannot load {container} to vehicle {Id}, because it's full");

            Console.WriteLine($"Loading {container} to vehicle {Id}");

            container.Location = Id;
            _containers.Add(container);
        }

        public void LoadRange(List<Kontener> containers)
        {
            if (_containers.Count + containers.Count > GetMaxCapacity()) throw new Exception($"Canot load range of containers: {string.Join(',', containers)}, becuase maximum capacity will be exceeded");

            containers.ForEach(Load);
        }

        public int GetContainersCount()
        {
            return _containers.Count;
        }

        public void UnloadToWarehouse(Magazyn magazyn)
        {
            magazyn.LoadContainers(_containers);

            _containers.Clear();
        }
    }
}
