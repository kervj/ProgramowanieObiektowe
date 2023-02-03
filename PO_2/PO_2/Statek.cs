namespace PO_2
{
    public class Statek : Pojazd
    {
        private static int _idCounter = 1;

        public Statek(string location) : base($"SHP_{_idCounter.ToString().PadLeft(2, '0')}", location)
        {
            _idCounter++;
        }

        protected override int GetMaxCapacity() => 90;

        protected override string GetTravelDetails(string destination) =>
            $"Performing travel with ship from {Location} to {destination}";
    }
}
