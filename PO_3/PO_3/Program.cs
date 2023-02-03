using PO_3;

Console.WriteLine(new string('#', 20));
Console.WriteLine("Task #1");

Console.WriteLine("Creating pairs and displaying max values\n");

var stringPair = new Para<string>("left-value", "right-value");
var intPair = new Para<int>(-10, 10);
var doublePair = new Para<double>(-10.5, 10.5);

Console.WriteLine(stringPair + ", Max value: " + stringPair.Max());
Console.WriteLine(intPair + ", Max value: " + intPair.Max());
Console.WriteLine(doublePair + ", Max value: " + doublePair.Max());

Console.WriteLine("\nSwapping and displaying pairs with max values");
stringPair.Swap();
intPair.Swap();
doublePair.Swap();
Console.WriteLine(stringPair + ", Max value: " + stringPair.Max());
Console.WriteLine(intPair + ", Max value: " + intPair.Max());
Console.WriteLine(doublePair + ", Max value: " + doublePair.Max());

Console.WriteLine(new string('#', 20));
Console.WriteLine("Task #2");
Console.WriteLine(new string('#', 20));
Console.WriteLine("Creating bird enclosure");
var birdsEnclosure = new Wybieg<Ptak>();
Console.WriteLine("Adding birds to enclosure");
birdsEnclosure.Add(new Ptak("Bird_A"));
birdsEnclosure.Add(new Ptak("Bird_B"));
birdsEnclosure.Add(new Ptak("Bird_C"));
Console.WriteLine($"Current animal count in enclosure: {birdsEnclosure.Count}");
Console.WriteLine("Removing birds from enclosure");
birdsEnclosure.RemoveAll();
Console.WriteLine($"Current animal count in enclosure: {birdsEnclosure.Count}");

Console.Write("Press any key to continue");
Console.ReadKey();