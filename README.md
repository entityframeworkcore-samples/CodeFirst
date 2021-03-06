# Code first Entity Framework Dotnet Core 2x simple guide

## 1. Creating the solution projects for this guide
Open a new Terminal window and then type the bellow commands or run the powershell script "1.SetupGuide.ps1" which it will execute the bellow commands:

Create new empty solution "EFCodeFirst.JecaestevezApp"
 > dotnet new sln -n EFCodeFirst.JecaestevezApp

Create empty console application "ConsoleApp.Jecaestevez"
 > dotnet new console -n ConsoleApp.Jecaestevez -o ConsoleApp

Create empty library application "DAL.JecaestevezApp"
 > dotnet new classlib -n DAL.JecaestevezApp -o DAL

 Add the created console application to the solution
  > dotnet sln EFCodeFirst.JecaestevezApp.sln add ConsoleApp/ConsoleApp.Jecaestevez.csproj  

Add the console application to the solution
  > dotnet sln EFCodeFirst.JecaestevezApp.sln add DAL/DAL.JecaestevezApp.csproj  

Add a refrence from ConsoleApp to DAL.JecaestevezApp
  >dotnet add ConsoleApp/ConsoleApp.Jecaestevez.csproj reference DAL/DAL.JecaestevezApp.csproj

Build the solution
 > dotnet build

# 2 Add Entity Framework Core packages to 
Add the necesary packages for this example executing the powershell script "2.AddNugetPackages.ps1"

You can also add manual the package opening  terminal and navigate to CodeFirst\DAL add to "DAL.JecaestevezApp.csproj"  EntityFrameworkCore.SqlServer and EntityFrameworkCore.Tools

> dotnet add .\DAL\DAL.JecaestevezApp.csproj package Microsoft.EntityFrameworkCore.SqlServer

> dotnet add .\DAL\DAL.JecaestevezApp.csproj package Microsoft.EntityFrameworkCore.Tools 

> dotnet add .\DAL\DAL.JecaestevezApp.csproj package Microsoft.EntityFrameworkCore.Design 

# 3 Add a simple class to be used in a new  DBContext
Add a simple class "Item" like this:
```
    public class Item
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
```
Add DBContext
```
    public class EfDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO Extract connection string to a secret
            optionsBuilder.UseSqlServer(@"Server=.\;Database=EFCodeFirstDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Item> Items { get; set; }
    }
```
# 4 Create the first migration
Open powershell console and execute the powershell script  "4.CrateMigration.ps1" which will execute the bellow command in "\DAL\DAL.Jecaestevez.csproj"
> dotnet ef  migrations add CreateDatabase --startup-project ..\ConsoleApp

It's possible do the same step using the Package Manager Console in Visual Studio, selecting the DAL.JecaestevezApp.csproj and execute 
> PM > add-migration CreateDatabase

It will be create a folder "Migrations" and the following files:
* CreateDatabase.cs
* CreateDatabase.Designer.cs
* EfDbContextModelSnapshot.cs

# 5 Update Database
Execute the powershell script  "5.UpdateDatabase.ps1" which will execute the bellow command in "\DAL\DAL.Jecaestevez.csproj"
> dotnet ef database update --startup-project ..\ConsoleApp

Using Package Manager Console select the DAL.JecaestevezApp.csproj and execute 
> PM> update-database –verbose



# 6 Use DBContext in the console App
```
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
```
## 7 Modify the Item table adding new column / field
Add a simple new field "Expiration" DateTime
```
    public class Item
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Expiration { get; set; }
    }
```
## 8 Add new migration
Open the Package Manager Console in Visual Studio, selecting the DAL.JecaestevezApp.csproj and execute 
> PM > add-migration AddNewColumn

It will be create a folder "Migrations" and the following files:
* 20190328182329_AddNewColumn.cs
* 20190328182329_AddNewColumn.Designer.cs

It will automatially update
* EfDbContextModelSnapshot.cs

# 9 Update Database
Execute the powershell script  "5.UpdateDatabase.ps1" which will execute the bellow command in "\DAL\DAL.Jecaestevez.csproj"
> dotnet ef database update --startup-project ..\ConsoleApp

Using Package Manager Console select the DAL.JecaestevezApp.csproj and execute 
> PM> update-database –verbose

# 10 Use the new field
```
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
                    Description = "Drink",
                    Expiration = DateTime.Now.AddYears(1)

                };
                Console.WriteLine($"Item NOT saved -> Id {Item.id} {Item.Name} {Item.Expiration}");

                context.Add(Item);
                context.SaveChanges();

                Console.WriteLine($"Item saved -> Id {Item.id} {Item.Name} {Item.Expiration}");
                Console.ReadKey();
            }
        }
    }
```