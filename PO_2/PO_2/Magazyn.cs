using System.ComponentModel;

namespace PO_2
{
    public class Magazyn
    {
        public string Location { get; }
        public List<Kontener> Kontener { get; } = new();
        public Dictionary<string, List<Kontener>> ReservedContainers { get; } = new();

        public Magazyn(string location)
        {
            Location = location;
        }

        public void LoadContainers(List<Kontener> containers)
        {
            containers.ForEach(c =>
            {
                c.Location = Location;
                Kontener.Add(c);
            });

            Console.WriteLine($"[{Location}] Loaded {containers.Count} containers to warehouse");
        }

        public bool ReserveContainers(Func<Kontener, bool> query, Pojazd vehicle)
        {
            if (ReservedContainers.TryGetValue(vehicle.Id, out _))
            {
                Console.WriteLine($"[{Location}] Vehicle {vehicle.Id} has already reserved containers in this warehouse");
                return false;
            }

            var containers = Kontener.Where(query).Take(vehicle.GetAvailableCapacity()).ToList();
            if (!containers.Any())
            {
                return false;
            }

            ReservedContainers.Add(vehicle.Id, containers);

            containers.ForEach(c => Kontener.Remove(c));

            Console.WriteLine($"[{Location}] {containers.Count} containers reserved for vehicle {vehicle.Id}");

            return true;
        }

        public void LoadToVehicle(Pojazd vehicle)
        {
            if (ReservedContainers.TryGetValue(vehicle.Id, out var Kontener))
            {
                vehicle.LoadRange(Kontener);
                Console.WriteLine($"[{Location}] Loaded {Kontener.Count} containers to vehicle {vehicle.Id}");
                ReservedContainers.Remove(vehicle.Id);
                return;
            }

            Console.WriteLine($"[{Location}] Could not load containers to vehicle {vehicle.Id}, because no containers were reserved");
        }

        public void DisplayStock()
        {
            Console.WriteLine($"[{Location}] Current stock:");
            Kontener.ForEach(c =>
            {
                Console.WriteLine($"[{Location}] \t- {c}");
            });
        }
    }
}
