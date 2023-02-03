namespace PO_2
{
    public class Towar
    {
        public TypTowaru TypTowaru { get; }

        public Towar(TypTowaru goodsType)
        {
            TypTowaru = goodsType;
        }

        public override string ToString()
        {
            return TypTowaru.ToString();
        }

        public static Towar None() => new(TypTowaru.None);
    }
}
