var burger = new BurgerBuilder()
    .SetBun("Sesame Seed")
    .SetMeat("Beef")
    .AddTopping("Cheese")
    .AddTopping("Lettuce")
    .AddTopping("Tomato")
    .Build();

Console.WriteLine(burger);

var oakLeaf1 = Leaf.GetLeaf("Oak", 10);
var oakLeaf2 = Leaf.GetLeaf("OAK", 10);
var mapleLeaf = Leaf.GetLeaf("MAPLE", 12);

Console.WriteLine(oakLeaf1);
Console.WriteLine(oakLeaf2);
Console.WriteLine(mapleLeaf);

var google = new Stock("GOOG", 612.50m);

var investor1 = new Investor("Investor 1");
var investor2 = new Investor("Investor 2");

google.Attach(investor1);
google.Attach(investor2);

google.Price = 625.00m;
google.Price = 630.50m;

google.Detach(investor2);

google.Price = 635.75m;

public class Burger
{
    public string Bun;
    public string Meat;
    public List<string> Toppings;

    public Burger(string bun, string meat, List<string> toppings)
    {
        Bun = bun;
        Meat = meat;
        Toppings = toppings;
    }

    public override string ToString()
    {
        return $"Bun: {Bun}, Meat: {Meat}, Toppings: {string.Join(", ", Toppings)}";
    }
}

public class BurgerBuilder
{
    private string bun;
    private string meat;
    private List<string> toppings;

    public BurgerBuilder()
    {
        toppings = new List<string>();
    }

    public BurgerBuilder SetBun(string bun)
    {
        this.bun = bun;
        return this;
    }

    public BurgerBuilder SetMeat(string meat)
    {
        this.meat = meat;
        return this;
    }

    public BurgerBuilder AddTopping(string topping)
    {
        toppings.Add(topping);
        return this;
    }

    public Burger Build()
    {
        return new Burger(bun, meat, toppings);
    }
}


public class Leaf
{
    private static Dictionary<string, Leaf> cache = new Dictionary<string, Leaf>();

    private readonly string type;
    private readonly int size;

    private Leaf(string type, int size)
    {
        this.type = type;
        this.size = size;
    }

    public static Leaf GetLeaf(string type, int size)
    {
        if (!cache.ContainsKey(type))
        {
            cache[type] = new Leaf(type, size);
        }
        return cache[type];
    }

    public override string ToString()
    {
        return $"Type: {type}, Size: {size}";
    }
}

public class Stock
{
    public readonly string symbol;
    public decimal price;
    public List<IInvestor> investors = new List<IInvestor>();

    public Stock(string symbol, decimal price)
    {
        this.symbol = symbol;
        this.price = price;
    }

    public decimal Price
    {
        get { return price; }
        set
        {
            if (price != value)
            {
                price = value;
                NotifyInvestors();
            }
        }
    }

    public void Attach(IInvestor investor)
    {
        investors.Add(investor);
    }

    public void Detach(IInvestor investor)
    {
        investors.Remove(investor);
    }

    public void NotifyInvestors()
    {
        foreach (IInvestor investor in investors)
        {
            investor.Update(this);
        }
    }
}

public interface IInvestor
{
    void Update(Stock stock);
}

public class Investor : IInvestor
{
    private readonly string name;

    public Investor(string name)
    {
        this.name = name;
    }

    public void Update(Stock stock)
    {
        Console.WriteLine("Notified {0} of {1}'s change to {2:C}", name, stock.symbol, stock.Price);
    }
}