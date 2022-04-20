# My First Source Generator

I have worked with Boo script in the past, but [Boo](https://github.com/boo-lang/boo) and [RhinoDSL](https://github.com/ayende/rhino-dsl) have not been updated for years and are not compatible with .netstandard or .net6 so this is an attempt at an alternative.

## Creating the outline of the solution

As I am trying to get more familiar with the dotnet commands so just for my memory these are the commands used to create the solution and project.

``` powershell

PS C:\Source\GitRepos\MyFirstSourceGenerator> dotnet new sln
The template "Solution File" was created successfully.

PS C:\Source\GitRepos\MyFirstSourceGenerator> dotnet new console --output src\MyFirstSourceGenerator.Console
The template "Console App" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\Source\GitRepos\MyFirstSourceGenerator\src\MyFirstSourceGenerator.Console\MyFirstSourceGenerator.Console.csproj...
  Determining projects to restore...
  Restored C:\Source\GitRepos\MyFirstSourceGenerator\src\MyFirstSourceGenerator.Console\MyFirstSourceGenerator.Console.csproj (in 285 ms).
Restore succeeded.

```

## Very first generated code

Starting extremely simple I followed the steps in this blog post https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview?msclkid=d4e406eec0db11eca190e3785e5bfdae.
