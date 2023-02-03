namespace PO_3
{
    internal class Para<T> where T : IComparable
    {
        private T Left { get; set; }
        private T Right { get; set; }

        public Para(T left, T right)
        {
            Left = left;
            Right = right;
        }

        public void Swap()
        {
            (Left, Right) = (Right, Left);
        }

        public override string ToString()
        {
            return $"Left: {Left}".PadRight(20, ' ') + $"Right: {Right}";
        }

        public T Max()
        {
            return Left.CompareTo(Right) < 0 ? Right : Left;
        }
    }
}
