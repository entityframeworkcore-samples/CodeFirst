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
