#r "VehicleQuotes/bin/Debug/net6.0/VehicleQuotes.dll"
#r "VehicleQuotes/VehicleQuotes.csproj"

#r "nuget:EFCore.NamingConventions, 6.0.0"
#r "nuget:Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore, 6.0.1"
#r "nuget:Microsoft.EntityFrameworkCore.Design, 6.0.1"
#r "nuget:Microsoft.EntityFrameworkCore.SqlServer, 6.0.1"
#r "nuget:Microsoft.VisualStudio.Web.CodeGeneration.Design, 6.0.1"
#r "nuget:Npgsql.EntityFrameworkCore.PostgreSQL, 6.0.2"

#r "nuget:EFCore.NamingConventions"
#r "nuget:Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore"
#r "nuget:Microsoft.EntityFrameworkCore.Design"
#r "nuget:Microsoft.EntityFrameworkCore.SqlServer"
#r "nuget:Microsoft.VisualStudio.Web.CodeGeneration.Design"
#r "nuget:Npgsql.EntityFrameworkCore.PostgreSQL"


```cs
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleQuotes;

var options = new DbContextOptionsBuilder<VehicleQuotesContext>()
    .UseNpgsql("Host=db;Database=vehicle_quotes_test;Username=vehicle_quotes;Password=password")
    .UseSnakeCaseNamingConvention()
    .Options;
```