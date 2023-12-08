// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

//Fra Json objekter til C# for hver nøkkel 
public class Delivery
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public List<OrderItem> Order { get; set; }
}

//Fra Json objekter til C# for Order 
public class OrderItem
{
    public string Menuitem { get; set; }
    public int Quantity { get; set; }
}

//Leser fil, deserialisere json objekter til c# objekter og går gjennom filen, skriver ut nøkkel-verdi 
class Program
{
    static void Main()
    {
        string jsonInput = System.IO.File.ReadAllText(@"input-delivery.json");
        List<Delivery> deliveries = JsonConvert.DeserializeObject<List<Delivery>>(jsonInput);

        foreach (var delivery in deliveries)
        {
            int floor = CalculateFloor(delivery.Description);
            Console.WriteLine($"Navn: {delivery.Name}");
            Console.WriteLine($"Adresse: {delivery.Address}, Etasje: {floor}");
            Console.WriteLine("Bestilling:");
            foreach (var item in delivery.Order)
            {
                Console.WriteLine($"- {item.Menuitem}, Antall: {item.Quantity}");
            }
            Console.WriteLine();
        }
    }

    // Metoden beregner hvilken etasje man skal levere orderen
    private static int CalculateFloor(string description)
    {
        int floor = 0;
        foreach (char c in description)
        {
            if (c == '*'){
                 floor++;
            }
            else if (c == ')'){
                floor--;
            } 
        }
        return floor;
    }
}