namespace PO_2
{
    public class Pociag : Pojazd
    {
        private static int _idCounter = 1;

        public Pociag(string location) : base($"TRA_{_idCounter.ToString().PadLeft(2, '0')}", location)
        {
            _idCounter++;
        }

        protected override int GetMaxCapacity() => 20;

        protected override string GetTravelDetails(string destination) =>
            $"Performing travel with train from {Location} to {destination}";
    }
}
