dotnet new webapi -o VehicleQuotes

dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-ef

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet remove package Microsoft.EntityFrameworkCore.Design

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
<!-- dotnet add package Microsoft.EntityFrameworkCore.SQLite -->
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
<!-- dotnet add package Microsoft.EntityFrameworkCore.Tools -->
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package EFCore.NamingConventions

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.Abstractions

dotnet ef...
dotnet aspnet-codegenerator...

docker run -d \
    --name vehicle-quote-postgres \
    -p 5432:5432 \
    --network host \
    -e POSTGRES_DB=vehicle_quote \
    -e POSTGRES_USER=vehicle_quote \
    -e POSTGRES_PASSWORD=password \
    postgres

docker run -d \
    --name vehicle-quote-postgres-2 \
    -p 5432:5432 \
    --network host \
    -e POSTGRES_DB=vehicle_quote \
    -e POSTGRES_USER=vehicle_quote \
    -e POSTGRES_PASSWORD=password \
    postgres


docker exec -it vehicle-quote-postgres psql -U vehicle_quote
psql -h localhost -U vehicle_quotes


---->>> TODO: learn more about configuration to figure out the appsettings.json and appsettings.Development.json. Also figuring out how to use env variables.

TODO: input validation
// this.TryValidateModel(model);
// var v = ModelState.IsValid;
// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#ivalidatableobject
// https://stackoverflow.com/questions/20072173/asp-net-mvc-validate-a-model-outside-of-the-controller
TODO: investigate cascade delete and non cascade delete to talk about it: https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete#configuring-cascading-behaviors


Features:

1. CRUD for vehicle models, makes, body types and sizes.
2. CRUD for specific vehicle price override.
3. CRUD for pricing rules. Each property and value of the vehicle that affects price. name: VehicleFeatureValue.
4. Submit vehicle configuration and send back quote.

dotnet aspnet-codegenerator controller -name MakesController -m Make -dc VehicleQuotesContext -async -api -outDir Controllers
dotnet aspnet-codegenerator controller -name BodyTypeController -m BodyType -dc VehicleQuotesContext -async -api -outDir Controllers
dotnet aspnet-codegenerator controller -name SizeController -m Size -dc VehicleQuotesContext -async -api -outDir Controllers

dotnet aspnet-codegenerator controller -name ModelController -m Model -dc VehicleQuotesContext -async -api -outDir Controllers

dotnet aspnet-codegenerator controller -name QuoteController -m Quote -dc VehicleQuotesContext -async -api -outDir Controllers
dotnet aspnet-codegenerator controller -name QuoteRuleController -m QuoteRule -dc VehicleQuotesContext -async -api -outDir Controllers
dotnet aspnet-codegenerator controller -name QuoteOverrideController -m QuoteOverride -dc VehicleQuotesContext -async -api -outDir Controllers


export Logging__LogLevel__Default=Warning; dotnet run


DefaultOffer=123 dotnet run
unset Logging__LogLevel__Default


dotnet test --logger "console;verbosity=detailed"


nuget packages location: /home/kevin/.nuget/packages/
this is where tools are installed alsio


```sh
cd VehicleQuotes.CreateUser/
dotnet run
dotnet pack
dotnet pack --version-suffix 1.0.1
```

```sh
dotnet nuget locals all --clear
dotnet nuget locals http-cache --clear
dotnet new tool-manifest
dotnet tool uninstall VehicleQuotes.CreateUser
dotnet tool install --add-source ./VehicleQuotes.CreateUser/nupkg VehicleQuotes.CreateUser
dotnet tool run create_user
dotnet tool run create_user name email secret
dotnet create_user name email secret
dotnet tool update --add-source ./VehicleQuotes.CreateUser/nupkg vehiclequotes.createuser
```