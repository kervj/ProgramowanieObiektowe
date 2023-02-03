using System;

class SilniaException : Exception
{
    public SilniaException(string message) : base(message)
    {
    }
}

class Program
{
    static long Silnia(int n)
    {
        if (n < 0)
        {
            throw new SilniaException("Nie można obliczyć silni dla liczby ujemnej");
        }

        long wynik = 1;
        for (int i = 2; i <= n; i++)
        {
            wynik *= i;
        }

        return wynik;
    }

    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Podaj cyfrę której chcesz obliczyć silnie");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Silnia(n));
        }
        catch (SilniaException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}