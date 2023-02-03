using System;
using System.Text.RegularExpressions;

class Adres
{
    private string ulica;
    private string numerDomu;
    private string kodPocztowy;
    private string miasto;

    public Adres(string ulica, string numerDomu, string kodPocztowy, string miasto)
    {
        Ulica = ulica;
        NumerDomu = numerDomu;
        KodPocztowy = kodPocztowy;
        Miasto = miasto;
    }

    public string Ulica
    {
        get
        {
            return ulica;
        }
        set
        {
            if (value == null)
                throw new ArgumentNullException("Ulica nie może być nullem");
            ulica = value;
        }
    }

    public string NumerDomu
    {
        get
        {
            return numerDomu;
        }
        set
        {
            if (!Regex.IsMatch(value, @"^\d+[a-zA-Z]?$"))
                throw new ArgumentException("Numer domu musi się składać z liczby i ewentualnie litery");
            numerDomu = value;
        }
    }

    public string KodPocztowy
    {
        get
        {
            return kodPocztowy;
        }
        set
        {
            if (!Regex.IsMatch(value, @"^\d{2}-\d{3}$"))
                throw new ArgumentException("Kod pocztowy musi mieć format CC-CCC");
            kodPocztowy = value;
        }
    }

    public string Miasto
    {
        get
        {
            return miasto;
        }
        set
        {
            if (value == null)
                throw new ArgumentNullException("Miasto nie może być nullem");
            miasto = value;
        }
    }
}