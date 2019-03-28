using DAL.JecaestevezApp;
using System;

namespace ConsoleApp.Jecaestevez
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var context = new EfDbContext())
            {
                var Item = new Item()
                {
                    Name = "Ron Palido",
                    Description = "Drink"

                };
                Console.WriteLine($"Item NOT saved -> Id {Item.id} {Item.Name}");

                context.Add(Item);
                context.SaveChanges();

                Console.WriteLine($"Item saved -> Id {Item.id} {Item.Name}");
                Console.ReadKey();
            }

        }
    }
}
