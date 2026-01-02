# .NET Templates

## Project Overview
This repository is a collection of .NET templates for building Clean Architecture solutions. The current focus is an ASP.NET Core Web API template designed for microservices. More templates (console app, background job) will be added over time.

## Templates
- WebApplicationTemplate: Clean Architecture ASP.NET Core Web API template for microservices.


## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (latest version)
- [Node.js](https://nodejs.org/) (latest LTS, only required if you are using Angular or React)

```bash
dotnet new install .
```

Check your .NET templates

```bash
dotnet new -l
```

or

```bash
dotnet new --list
```

Uninstall the templates

```bash
dotnet new uninstall . 
```

## Create a project

After installing, create a Web API solution with:

```bash
dotnet new web-api-template -n MyService
```

See `WebApplicationTemplate/README.md` for template-specific details.
